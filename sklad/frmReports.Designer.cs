namespace sklad
{
    partial class frmReports
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.lblReportType = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dataGridViewReports = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).BeginInit();
            this.SuspendLayout();

            // lblReportType
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(12, 15);
            this.lblReportType.Text = "Тип отчёта:";

            // cmbReportType
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.Location = new System.Drawing.Point(100, 12);
            this.cmbReportType.Size = new System.Drawing.Size(200, 21);
            this.cmbReportType.Items.AddRange(new object[] {
                "Остатки товаров",
                "Движение товаров",
                "Просроченные товары",
                "Приходные накладные",
                "Расходные накладные"
            });
            this.cmbReportType.SelectedIndex = 0;

            // btnGenerate
            this.btnGenerate.Location = new System.Drawing.Point(320, 10);
            this.btnGenerate.Size = new System.Drawing.Size(100, 25);
            this.btnGenerate.Text = "Сформировать";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            // dataGridViewReports
            this.dataGridViewReports.AllowUserToAddRows = false;
            this.dataGridViewReports.AllowUserToDeleteRows = false;
            this.dataGridViewReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReports.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewReports.Name = "dataGridViewReports";
            this.dataGridViewReports.ReadOnly = true;
            this.dataGridViewReports.Size = new System.Drawing.Size(760, 300);
            this.dataGridViewReports.TabIndex = 1;

            // btnExportExcel
            this.btnExportExcel.Location = new System.Drawing.Point(12, 370);
            this.btnExportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcel.Text = "Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // btnExportPdf
            this.btnExportPdf.Location = new System.Drawing.Point(130, 370);
            this.btnExportPdf.Size = new System.Drawing.Size(100, 30);
            this.btnExportPdf.Text = "PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);

            // frmReports
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dataGridViewReports);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.cmbReportType);
            this.Controls.Add(this.lblReportType);
            this.Name = "frmReports";
            this.Text = "Отчёты";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dataGridViewReports;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportPdf;
    }
}