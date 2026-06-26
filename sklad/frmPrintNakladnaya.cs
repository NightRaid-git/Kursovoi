using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmPrintNakladnaya : Form
    {
        private int shipmentId;
        private DataTable headerData;
        private DataTable detailsData;

        public frmPrintNakladnaya(int id)
        {
            InitializeComponent();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (detailsData == null || detailsData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Парсим дату
            DateTime date = DateTime.Parse(lblDateValue.Text);

            // Парсим сумму (убираем " руб.")
            string totalText = lblTotalValue.Text.Replace(" руб.", "").Trim();
            decimal total = decimal.Parse(totalText);

            ExportHelper.ExportInvoiceToPdf(
                detailsData,
                lblNumberValue.Text,      // invoiceNumber
                lblCustomerValue.Text,    // customer
                lblEmployeeValue.Text,    // employee
                date,                     // date
                total,                    // total
                $"Накладная_{lblNumberValue.Text}.pdf"
            );
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (detailsData == null || detailsData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime date = DateTime.Parse(lblDateValue.Text);
            string totalText = lblTotalValue.Text.Replace(" руб.", "").Trim();
            decimal total = decimal.Parse(totalText);

            ExportHelper.ExportInvoiceToExcel(
                detailsData,
                lblNumberValue.Text,
                lblCustomerValue.Text,
                lblEmployeeValue.Text,
                date,
                total,
                $"Накладная_{lblNumberValue.Text}.xlsx"
            );
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}