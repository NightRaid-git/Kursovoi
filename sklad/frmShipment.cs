using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmShipment : Form
    {
        private int employeeId;
        private DataTable detailsTable;

        public frmShipment(int empId)
        {
            InitializeComponent();
            employeeId = empId;
            LoadProducts();
            InitializeDetailsGrid();
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Product, ProductName FROM Products", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbProduct.DataSource = dt;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ID_Product";
            }
        }

        private void InitializeDetailsGrid()
        {
            detailsTable = new DataTable();
            detailsTable.Columns.Add("ProductID", typeof(int));
            detailsTable.Columns.Add("ProductName", typeof(string));
            detailsTable.Columns.Add("Quantity", typeof(decimal));
            detailsTable.Columns.Add("Price", typeof(decimal));
            dataGridViewDetails.DataSource = detailsTable;
            dataGridViewDetails.AutoResizeColumns();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedValue == null) return;
            int productId = (int)cmbProduct.SelectedValue;
            string productName = cmbProduct.Text;
            decimal qty = nudQuantity.Value;
            decimal price = nudPrice.Value;

            if (qty <= 0)
            {
                MessageBox.Show("Количество должно быть больше 0");
                return;
            }
            // проверка наличия достаточного количества на складе (суммарно)
            decimal available = GetAvailableQuantity(productId);
            if (available < qty)
            {
                MessageBox.Show($"Недостаточно товара на складе. Доступно: {available}");
                return;
            }

            DataRow row = detailsTable.NewRow();
            row["ProductID"] = productId;
            row["ProductName"] = productName;
            row["Quantity"] = qty;
            row["Price"] = price;
            detailsTable.Rows.Add(row);
            CalculateTotal();
        }

        private decimal GetAvailableQuantity(int productId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT SUM(Quantity) FROM CurrentStock WHERE ProductID = @pid AND Quantity > 0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pid", productId);
                object res = cmd.ExecuteScalar();
                return res == DBNull.Value ? 0 : Convert.ToDecimal(res);
            }
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            if (dataGridViewDetails.CurrentRow != null)
                dataGridViewDetails.Rows.Remove(dataGridViewDetails.CurrentRow);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in detailsTable.Rows)
            {
                total += Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Price"]);
            }
            txtTotal.Text = total.ToString("N2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Введите наименование клиента");
                return;
            }
            if (detailsTable.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте товары");
                return;
            }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string shipmentNumber = "Р-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    decimal total = Convert.ToDecimal(txtTotal.Text);
                    string sqlShipment = @"INSERT INTO Shipments (ShipmentNumber, ShipmentDate, EmployeeID, CustomerName, TotalAmount) 
                                   VALUES (@num, @date, @emp, @cust, @total); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sqlShipment, conn, trans);
                    cmd.Parameters.AddWithValue("@num", shipmentNumber);
                    cmd.Parameters.AddWithValue("@date", dtpShipmentDate.Value);
                    cmd.Parameters.AddWithValue("@emp", employeeId);
                    cmd.Parameters.AddWithValue("@cust", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@total", total);
                    int shipmentId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (DataRow row in detailsTable.Rows)
                    {
                        int productId = Convert.ToInt32(row["ProductID"]);
                        decimal qtyToShip = Convert.ToDecimal(row["Quantity"]);
                        decimal price = Convert.ToDecimal(row["Price"]);

                        DataTable batches = GetBatchesForProduct(productId);
                        decimal remaining = qtyToShip;
                        foreach (DataRow batch in batches.Rows)
                        {
                            int stockId = Convert.ToInt32(batch["ID_Stock"]);
                            decimal batchQty = Convert.ToDecimal(batch["Quantity"]);
                            decimal shipFromBatch = Math.Min(remaining, batchQty);
                            if (shipFromBatch > 0)
                            {
                                decimal newQty = batchQty - shipFromBatch;
                                UpdateStockQuantity(stockId, newQty, conn, trans);

                                string sqlDetail = @"INSERT INTO ShipmentDetails (ShipmentID, ProductID, Quantity, Price, StockID)
                                             VALUES (@sid, @pid, @qty, @price, @stock)";
                                SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn, trans);
                                cmdDetail.Parameters.AddWithValue("@sid", shipmentId);
                                cmdDetail.Parameters.AddWithValue("@pid", productId);
                                cmdDetail.Parameters.AddWithValue("@qty", shipFromBatch);
                                cmdDetail.Parameters.AddWithValue("@price", price);
                                cmdDetail.Parameters.AddWithValue("@stock", stockId);
                                cmdDetail.ExecuteNonQuery();

                                remaining -= shipFromBatch;
                                if (remaining == 0) break;
                            }
                        }
                        if (remaining > 0)
                            throw new Exception($"Не хватает товара для {productId}");
                    }
                    trans.Commit();

                    if (MessageBox.Show($"Расходная накладная №{shipmentNumber} оформлена. Желаете распечатать?",
                                        "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        frmPrintNakladnaya frm = new frmPrintNakladnaya(shipmentId);
                        ShowPrint(shipmentId);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private DataTable GetBatchesForProduct(int productId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT ID_Stock, Quantity, ExpiryDate FROM CurrentStock 
                               WHERE ProductID = @pid AND Quantity > 0 ORDER BY ExpiryDate";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@pid", productId);
                da.Fill(dt);
            }
            return dt;
        }

        private void UpdateStockQuantity(int stockId, decimal newQty, SqlConnection conn, SqlTransaction trans)
        {
            string sql = "UPDATE CurrentStock SET Quantity = @qty WHERE ID_Stock = @id";
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            cmd.Parameters.AddWithValue("@qty", newQty);
            cmd.Parameters.AddWithValue("@id", stockId);
            cmd.ExecuteNonQuery();
        }

        private void ShowPrint(int shipmentId)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}