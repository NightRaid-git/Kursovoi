namespace sklad
{
    partial class frmPrintNakladnaya
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblNumberValue = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblCustomerValue = new System.Windows.Forms.Label();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblEmployeeValue = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.dataGridViewDetails = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).BeginInit();
            this.SuspendLayout();

            // lblNumber
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(12, 15);
            this.lblNumber.Text = "Номер накладной:";

            // lblNumberValue
            this.lblNumberValue.AutoSize = true;
            this.lblNumberValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNumberValue.Location = new System.Drawing.Point(130, 15);

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 40);
            this.lblDate.Text = "Дата:";

            // lblDateValue
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Location = new System.Drawing.Point(130, 40);

            // lblCustomer
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(12, 65);
            this.lblCustomer.Text = "Клиент:";

            // lblCustomerValue
            this.lblCustomerValue.AutoSize = true;
            this.lblCustomerValue.Location = new System.Drawing.Point(130, 65);

            // lblEmployee
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(12, 90);
            this.lblEmployee.Text = "Ответственный:";

            // lblEmployeeValue
            this.lblEmployeeValue.AutoSize = true;
            this.lblEmployeeValue.Location = new System.Drawing.Point(130, 90);

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 115);
            this.lblTotal.Text = "Итого:";

            // lblTotalValue
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.Location = new System.Drawing.Point(130, 115);

            // dataGridViewDetails
            this.dataGridViewDetails.AllowUserToAddRows = false;
            this.dataGridViewDetails.AllowUserToDeleteRows = false;
            this.dataGridViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetails.Location = new System.Drawing.Point(12, 150);
            this.dataGridViewDetails.Name = "dataGridViewDetails";
            this.dataGridViewDetails.ReadOnly = true;
            this.dataGridViewDetails.Size = new System.Drawing.Size(660, 250);
            this.dataGridViewDetails.TabIndex = 10;

            // btnExportExcel
            this.btnExportExcel.Location = new System.Drawing.Point(12, 420);
            this.btnExportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcel.Text = "Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // btnExportPdf
            this.btnExportPdf.Location = new System.Drawing.Point(130, 420);
            this.btnExportPdf.Size = new System.Drawing.Size(100, 30);
            this.btnExportPdf.Text = "PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(580, 420);
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // frmPrintNakladnaya
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 470);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dataGridViewDetails);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblEmployeeValue);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblCustomerValue);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblDateValue);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblNumberValue);
            this.Controls.Add(this.lblNumber);
            this.Name = "frmPrintNakladnaya";
            this.Text = "Расходная накладная";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblNumberValue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblCustomerValue;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblEmployeeValue;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.DataGridView dataGridViewDetails;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnClose;
    }
}