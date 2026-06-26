using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmReceipt : Form
    {
        private int employeeId;
        private DataTable detailsTable;

        public frmReceipt(int empId)
        {
            InitializeComponent();
            employeeId = empId;
            LoadSuppliers();
            LoadProducts();
            InitializeDetailsGrid();
        }

        private void LoadSuppliers()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Supplier, SupplierName FROM Suppliers", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbSupplier.DataSource = dt;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "ID_Supplier";
            }
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
            detailsTable.Columns.Add("ProductionDate", typeof(DateTime));
            detailsTable.Columns.Add("ExpiryDate", typeof(DateTime));
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
            DateTime prodDate = dtpProductionDate.Value;
            DateTime expDate = dtpExpiryDate.Value;

            if (qty <= 0)
            {
                MessageBox.Show("Количество должно быть больше 0");
                return;
            }
            if (expDate <= prodDate)
            {
                MessageBox.Show("Срок годности должен быть позже даты производства");
                return;
            }

            DataRow row = detailsTable.NewRow();
            row["ProductID"] = productId;
            row["ProductName"] = productName;
            row["Quantity"] = qty;
            row["Price"] = price;
            row["ProductionDate"] = prodDate;
            row["ExpiryDate"] = expDate;
            detailsTable.Rows.Add(row);
            CalculateTotal();
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
                decimal qty = Convert.ToDecimal(row["Quantity"]);
                decimal price = Convert.ToDecimal(row["Price"]);
                total += qty * price;
            }
            txtTotal.Text = total.ToString("N2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedValue == null) { MessageBox.Show("Выберите поставщика"); return; }
            if (detailsTable.Rows.Count == 0) { MessageBox.Show("Добавьте товары"); return; }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // вставка заголовка
                    string receiptNumber = "П-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    decimal total = Convert.ToDecimal(txtTotal.Text);
                    string sqlReceipt = @"INSERT INTO Receipts (ReceiptNumber, ReceiptDate, SupplierID, EmployeeID, TotalAmount) 
                                          VALUES (@num, @date, @sup, @emp, @total); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sqlReceipt, conn, trans);
                    cmd.Parameters.AddWithValue("@num", receiptNumber);
                    cmd.Parameters.AddWithValue("@date", dtpReceiptDate.Value);
                    cmd.Parameters.AddWithValue("@sup", (int)cmbSupplier.SelectedValue);
                    cmd.Parameters.AddWithValue("@emp", employeeId);
                    cmd.Parameters.AddWithValue("@total", total);
                    int receiptId = Convert.ToInt32(cmd.ExecuteScalar());

                    // вставка строк и обновление остатков
                    foreach (DataRow row in detailsTable.Rows)
                    {
                        int productId = Convert.ToInt32(row["ProductID"]);
                        decimal qty = Convert.ToDecimal(row["Quantity"]);
                        decimal price = Convert.ToDecimal(row["Price"]);
                        DateTime prodDate = Convert.ToDateTime(row["ProductionDate"]);
                        DateTime expDate = Convert.ToDateTime(row["ExpiryDate"]);

                        string sqlDetail = @"INSERT INTO ReceiptDetails (ReceiptID, ProductID, Quantity, Price, ProductionDate, ExpiryDate)
                                             VALUES (@rid, @pid, @qty, @price, @prod, @exp);
                                             SELECT SCOPE_IDENTITY();";
                        cmd = new SqlCommand(sqlDetail, conn, trans);
                        cmd.Parameters.AddWithValue("@rid", receiptId);
                        cmd.Parameters.AddWithValue("@pid", productId);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@prod", prodDate);
                        cmd.Parameters.AddWithValue("@exp", expDate);
                        int detailId = Convert.ToInt32(cmd.ExecuteScalar());

                        // добавить в CurrentStock (по умолчанию зона = 1 - холодильная, для упрощения)
                        string sqlStock = @"INSERT INTO CurrentStock (ProductID, ZoneID, Quantity, ProductionDate, ExpiryDate, ReceiptDetailID)
                                            VALUES (@pid, 1, @qty, @prod, @exp, @detid)";
                        cmd = new SqlCommand(sqlStock, conn, trans);
                        cmd.Parameters.AddWithValue("@pid", productId);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@prod", prodDate);
                        cmd.Parameters.AddWithValue("@exp", expDate);
                        cmd.Parameters.AddWithValue("@detid", detailId);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    MessageBox.Show("Приход оформлен. Накладная №" + receiptNumber);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}