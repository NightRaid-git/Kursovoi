namespace sklad
{
    partial class frmWriteOff
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dataGridViewExpired = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnWriteOff = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpired)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExpired
            // 
            this.dataGridViewExpired.AllowUserToAddRows = false;
            this.dataGridViewExpired.AllowUserToDeleteRows = false;
            this.dataGridViewExpired.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExpired.Location = new System.Drawing.Point(12, 40);
            this.dataGridViewExpired.Name = "dataGridViewExpired";
            this.dataGridViewExpired.Size = new System.Drawing.Size(500, 200);
            this.dataGridViewExpired.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 15);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(121, 13);
            this.lblInfo.Text = "Просроченные партии:";
            // 
            // btnWriteOff
            // 
            this.btnWriteOff.Location = new System.Drawing.Point(12, 260);
            this.btnWriteOff.Name = "btnWriteOff";
            this.btnWriteOff.Size = new System.Drawing.Size(120, 30);
            this.btnWriteOff.Text = "Списать всё";
            this.btnWriteOff.Click += new System.EventHandler(this.btnWriteOff_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(150, 260);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(400, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmWriteOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 302);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnWriteOff);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dataGridViewExpired);
            this.Name = "frmWriteOff";
            this.Text = "Списание просроченных товаров";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpired)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridViewExpired;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnWriteOff;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
    }
}