using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmStockView : Form
    {
        public frmStockView()
        {
            InitializeComponent();
            LoadStock();
        }

        private void LoadStock()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT p.ProductName, cs.Quantity, cs.ProductionDate, cs.ExpiryDate, wz.ZoneName
                               FROM CurrentStock cs
                               JOIN Products p ON cs.ProductID = p.ID_Product
                               JOIN WarehouseZones wz ON cs.ZoneID = wz.ID_Zone
                               WHERE cs.Quantity > 0
                               ORDER BY cs.ExpiryDate";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewStock.DataSource = dt;
                dataGridViewStock.AutoResizeColumns();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStock();
        }
    }
}