using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmSelectNakladnaya : Form
    {
        public frmSelectNakladnaya()
        {
            InitializeComponent();
            LoadReceipts();
            LoadShipments();
        }

        // ==========================
        // ПРИХОДНЫЕ НАКЛАДНЫЕ
        // ==========================
        private void LoadReceipts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        r.ID_Receipt,
                        r.ReceiptNumber AS [Номер],
                        r.ReceiptDate AS [Дата],
                        s.SupplierName AS [Поставщик],
                        r.TotalAmount AS [Сумма]
                    FROM Receipts r
                    JOIN Suppliers s ON r.SupplierID = s.ID_Supplier
                    ORDER BY r.ReceiptDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }

            dgvReceipts.DataSource = dt;
            dgvReceipts.Columns["ID_Receipt"].Visible = false;
            dgvReceipts.AutoResizeColumns();
        }

        private void btnFilterReceipts_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        r.ID_Receipt,
                        r.ReceiptNumber AS [Номер],
                        r.ReceiptDate AS [Дата],
                        s.SupplierName AS [Поставщик],
                        r.TotalAmount AS [Сумма]
                    FROM Receipts r
                    JOIN Suppliers s ON r.SupplierID = s.ID_Supplier
                    WHERE r.ReceiptNumber LIKE @num
                      AND r.ReceiptDate BETWEEN @d1 AND @d2
                      AND s.SupplierName LIKE @sup
                    ORDER BY r.ReceiptDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@num", "%" + txtRecNumber.Text + "%");
                da.SelectCommand.Parameters.AddWithValue("@d1", dtRecFrom.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@d2", dtRecTo.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@sup", "%" + txtRecSupplier.Text + "%");

                da.Fill(dt);
            }

            dgvReceipts.DataSource = dt;
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (dgvReceipts.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvReceipts.CurrentRow.Cells["ID_Receipt"].Value);
            new frmPrintReceipt(id).ShowDialog();
        }

        private void dgvReceipts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPrintReceipt_Click(sender, e);
        }

        // ==========================
        // РАСХОДНЫЕ НАКЛАДНЫЕ
        // ==========================
        private void LoadShipments()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        sh.ID_Shipment,
                        sh.ShipmentNumber AS [Номер],
                        sh.ShipmentDate AS [Дата],
                        sh.CustomerName AS [Клиент],
                        sh.TotalAmount AS [Сумма]
                    FROM Shipments sh
                    ORDER BY sh.ShipmentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }

            dgvShipments.DataSource = dt;
            dgvShipments.Columns["ID_Shipment"].Visible = false;
            dgvShipments.AutoResizeColumns();
        }

        private void btnFilterShipments_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        sh.ID_Shipment,
                        sh.ShipmentNumber AS [Номер],
                        sh.ShipmentDate AS [Дата],
                        sh.CustomerName AS [Клиент],
                        sh.TotalAmount AS [Сумма]
                    FROM Shipments sh
                    WHERE sh.ShipmentNumber LIKE @num
                      AND sh.ShipmentDate BETWEEN @d1 AND @d2
                      AND sh.CustomerName LIKE @cust
                    ORDER BY sh.ShipmentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@num", "%" + txtShNumber.Text + "%");
                da.SelectCommand.Parameters.AddWithValue("@d1", dtShFrom.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@d2", dtShTo.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@cust", "%" + txtShCustomer.Text + "%");

                da.Fill(dt);
            }

            dgvShipments.DataSource = dt;
        }

        private void btnPrintShipment_Click(object sender, EventArgs e)
        {
            if (dgvShipments.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvShipments.CurrentRow.Cells["ID_Shipment"].Value);
            new frmPrintNakladnaya(id).ShowDialog();
        }

        private void dgvShipments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPrintShipment_Click(sender, e);
        }
    }
}
