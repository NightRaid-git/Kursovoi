using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmPrintReceipt : Form
    {
        private int receiptId;
        private DataTable headerData;
        private DataTable detailsData;

        public frmPrintReceipt(int id)
        {
            InitializeComponent();
            receiptId = id;
            LoadData();
            DisplayData();
        }

        private void LoadData()
        {
            // Загрузка шапки
            headerData = new DataTable();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sqlHeader = @"
                    SELECT r.ReceiptNumber,
                           r.ReceiptDate,
                           s.SupplierName,
                           r.TotalAmount,
                           e.LastName + ' ' + e.FirstName AS EmployeeName
                    FROM Receipts r
                    JOIN Suppliers s ON r.SupplierID = s.ID_Supplier
                    JOIN Employees e ON r.EmployeeID = e.ID_Employee
                    WHERE r.ID_Receipt = @id";

                SqlDataAdapter da = new SqlDataAdapter(sqlHeader, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", receiptId);
                da.Fill(headerData);
            }

            // Загрузка строк
            detailsData = new DataTable();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sqlDetails = @"
                    SELECT p.ProductName,
                           rd.Quantity,
                           rd.Price,
                           (rd.Quantity * rd.Price) AS Sum
                    FROM ReceiptDetails rd
                    JOIN Products p ON rd.ProductID = p.ID_Product
                    WHERE rd.ReceiptID = @id";

                SqlDataAdapter da = new SqlDataAdapter(sqlDetails, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", receiptId);
                da.Fill(detailsData);
            }
        }

        private void DisplayData()
        {
            if (headerData.Rows.Count == 0)
            {
                MessageBox.Show("Приходная накладная не найдена", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            DataRow row = headerData.Rows[0];

            lblNumberValue.Text = row["ReceiptNumber"].ToString();
            lblDateValue.Text = Convert.ToDateTime(row["ReceiptDate"]).ToShortDateString();
            lblSupplierValue.Text = row["SupplierName"].ToString();
            lblEmployeeValue.Text = row["EmployeeName"].ToString();
            lblTotalValue.Text = Convert.ToDecimal(row["TotalAmount"]).ToString("N2") + " руб.";

            dataGridViewDetails.DataSource = detailsData;
            dataGridViewDetails.AutoResizeColumns();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (detailsData == null || detailsData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime date = DateTime.Parse(lblDateValue.Text);
            string totalText = lblTotalValue.Text.Replace(" руб.", "").Trim();
            decimal total = decimal.Parse(totalText);

            ExportHelper.ExportInvoiceToPdf(
                detailsData,
                lblNumberValue.Text,
                lblSupplierValue.Text,
                lblEmployeeValue.Text,
                date,
                total,
                $"Приходная_{lblNumberValue.Text}.pdf"
            );
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (detailsData == null || detailsData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime date = DateTime.Parse(lblDateValue.Text);
            string totalText = lblTotalValue.Text.Replace(" руб.", "").Trim();
            decimal total = decimal.Parse(totalText);

            ExportHelper.ExportInvoiceToExcel(
                detailsData,
                lblNumberValue.Text,
                lblSupplierValue.Text,
                lblEmployeeValue.Text,
                date,
                total,
                $"Приходная_{lblNumberValue.Text}.xlsx"
            );
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
