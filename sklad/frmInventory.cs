using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmInventory : Form
    {
        private int employeeId;
        private DataTable currentStock;
        public frmInventory(int empId)
        {
            InitializeComponent();
            employeeId = empId;
            LoadCurrentStock();
        }

        private void LoadCurrentStock()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT cs.ID_Stock, p.ProductName, cs.Quantity AS SystemQuantity, cs.Quantity AS ActualQuantity
                               FROM CurrentStock cs
                               JOIN Products p ON cs.ProductID = p.ID_Product
                               WHERE cs.Quantity > 0
                               ORDER BY p.ProductName";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                currentStock = new DataTable();
                da.Fill(currentStock);
                dataGridViewInventory.DataSource = currentStock;
                dataGridViewInventory.Columns["ID_Stock"].Visible = false;
                dataGridViewInventory.Columns["SystemQuantity"].ReadOnly = true;
                dataGridViewInventory.Columns["ActualQuantity"].ReadOnly = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // сохранить инвентаризацию (заголовок)
                    string sqlInv = @"INSERT INTO Inventory (InventoryDate, EmployeeID) VALUES (@date, @emp); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sqlInv, conn, trans);
                    cmd.Parameters.AddWithValue("@date", dtpInventoryDate.Value);
                    cmd.Parameters.AddWithValue("@emp", employeeId);
                    int invId = Convert.ToInt32(cmd.ExecuteScalar());

                    // детали и корректировка остатков
                    foreach (DataRow row in currentStock.Rows)
                    {
                        int stockId = Convert.ToInt32(row["ID_Stock"]);
                        decimal systemQty = Convert.ToDecimal(row["SystemQuantity"]);
                        decimal actualQty = Convert.ToDecimal(row["ActualQuantity"]);
                        if (systemQty != actualQty)
                        {
                            // запись в InventoryDetails
                            string sqlDetail = @"INSERT INTO InventoryDetails (InventoryID, StockID, SystemQuantity, ActualQuantity)
                                                 VALUES (@inv, @stock, @sys, @act)";
                            cmd = new SqlCommand(sqlDetail, conn, trans);
                            cmd.Parameters.AddWithValue("@inv", invId);
                            cmd.Parameters.AddWithValue("@stock", stockId);
                            cmd.Parameters.AddWithValue("@sys", systemQty);
                            cmd.Parameters.AddWithValue("@act", actualQty);
                            cmd.ExecuteNonQuery();

                            // корректируем CurrentStock
                            string sqlUpdate = "UPDATE CurrentStock SET Quantity = @act WHERE ID_Stock = @stock";
                            cmd = new SqlCommand(sqlUpdate, conn, trans);
                            cmd.Parameters.AddWithValue("@act", actualQty);
                            cmd.Parameters.AddWithValue("@stock", stockId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                    MessageBox.Show("Инвентаризация завершена. Остатки скорректированы.");
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