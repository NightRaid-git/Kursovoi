namespace sklad
{
    partial class frmPrintReceipt
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblNumberValue = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblSupplierValue = new System.Windows.Forms.Label();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblEmployeeValue = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();

            this.dataGridViewDetails = new System.Windows.Forms.DataGridView();

            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).BeginInit();
            this.SuspendLayout();

            // Labels
            this.lblNumber.Text = "Номер накладной:";
            this.lblNumber.Location = new System.Drawing.Point(20, 20);

            this.lblNumberValue.Location = new System.Drawing.Point(160, 20);

            this.lblDate.Text = "Дата:";
            this.lblDate.Location = new System.Drawing.Point(20, 50);

            this.lblDateValue.Location = new System.Drawing.Point(160, 50);

            this.lblSupplier.Text = "Поставщик:";
            this.lblSupplier.Location = new System.Drawing.Point(20, 80);

            this.lblSupplierValue.Location = new System.Drawing.Point(160, 80);

            this.lblEmployee.Text = "Ответственный:";
            this.lblEmployee.Location = new System.Drawing.Point(20, 110);

            this.lblEmployeeValue.Location = new System.Drawing.Point(160, 110);

            this.lblTotal.Text = "Итого:";
            this.lblTotal.Location = new System.Drawing.Point(20, 140);

            this.lblTotalValue.Location = new System.Drawing.Point(160, 140);

            // DataGridView
            this.dataGridViewDetails.Location = new System.Drawing.Point(20, 180);
            this.dataGridViewDetails.Size = new System.Drawing.Size(760, 300);

            // Buttons
            this.btnExportPdf.Text = "PDF";
            this.btnExportPdf.Location = new System.Drawing.Point(20, 500);
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);

            this.btnExportExcel.Text = "Excel";
            this.btnExportExcel.Location = new System.Drawing.Point(120, 500);
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnClose.Text = "Закрыть";
            this.btnClose.Location = new System.Drawing.Point(680, 500);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblNumberValue);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblDateValue);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.lblSupplierValue);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblEmployeeValue);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalValue);

            this.Controls.Add(this.dataGridViewDetails);

            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnClose);

            this.Text = "Приходная накладная";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblNumberValue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblSupplierValue;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblEmployeeValue;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;

        private System.Windows.Forms.DataGridView dataGridViewDetails;

        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnClose;
    }
}
