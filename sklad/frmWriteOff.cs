using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmWriteOff : Form
    {
        private int employeeId;
        private DataTable expiredStock;

        public frmWriteOff(int empId)
        {
            InitializeComponent();
            employeeId = empId;
            LoadExpiredProducts();
        }

        private void LoadExpiredProducts()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT cs.ID_Stock, p.ProductName, cs.Quantity, cs.ExpiryDate
                               FROM CurrentStock cs
                               JOIN Products p ON cs.ProductID = p.ID_Product
                               WHERE cs.ExpiryDate < GETDATE() AND cs.Quantity > 0";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                expiredStock = new DataTable();
                da.Fill(expiredStock);
                dataGridViewExpired.DataSource = expiredStock;
                dataGridViewExpired.AutoResizeColumns();
                lblInfo.Text = $"Найдено просроченных партий: {expiredStock.Rows.Count}";
            }
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (expiredStock.Rows.Count == 0)
            {
                MessageBox.Show("Нет просроченных товаров для списания");
                return;
            }
            if (MessageBox.Show("Списать все просроченные партии?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string woNumber = "С-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sqlWO = @"INSERT INTO WriteOffs (WriteOffNumber, WriteOffDate, Reason, EmployeeID) 
                                     VALUES (@num, @date, @reason, @emp); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sqlWO, conn, trans);
                    cmd.Parameters.AddWithValue("@num", woNumber);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@reason", "Истек срок годности");
                    cmd.Parameters.AddWithValue("@emp", employeeId);
                    int woId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (DataRow row in expiredStock.Rows)
                    {
                        int stockId = Convert.ToInt32(row["ID_Stock"]);
                        int productId = Convert.ToInt32(row["ID_Stock"]); // неверно, надо получать productId – но в выборке его нет, исправим
                        // к сожалению, в выборке нет productId, сделаем запрос
                        // упростим: получим productId через stockId
                        int pid = GetProductIdByStock(stockId, conn, trans);
                        decimal qty = Convert.ToDecimal(row["Quantity"]);

                        string sqlDetail = @"INSERT INTO WriteOffDetails (WriteOffID, ProductID, Quantity, StockID)
                                             VALUES (@woid, @pid, @qty, @stid)";
                        cmd = new SqlCommand(sqlDetail, conn, trans);
                        cmd.Parameters.AddWithValue("@woid", woId);
                        cmd.Parameters.AddWithValue("@pid", pid);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@stid", stockId);
                        cmd.ExecuteNonQuery();

                        // обнуляем остаток
                        string sqlUpdate = "UPDATE CurrentStock SET Quantity = 0 WHERE ID_Stock = @stid";
                        cmd = new SqlCommand(sqlUpdate, conn, trans);
                        cmd.Parameters.AddWithValue("@stid", stockId);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    MessageBox.Show($"Списание оформлено. Акт №{woNumber}");
                    LoadExpiredProducts(); // обновить список
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private int GetProductIdByStock(int stockId, SqlConnection conn, SqlTransaction trans)
        {
            string sql = "SELECT ProductID FROM CurrentStock WHERE ID_Stock = @id";
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            cmd.Parameters.AddWithValue("@id", stockId);
            return (int)cmd.ExecuteScalar();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadExpiredProducts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}