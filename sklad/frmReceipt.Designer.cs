namespace sklad
{
    partial class frmReceipt
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblReceiptDate = new System.Windows.Forms.Label();
            this.dtpReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.dataGridViewDetails = new System.Windows.Forms.DataGridView();
            this.gbAddItem = new System.Windows.Forms.GroupBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.lblProductionDate = new System.Windows.Forms.Label();
            this.dtpProductionDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).BeginInit();
            this.gbAddItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(12, 15);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(68, 13);
            this.lblSupplier.Text = "Поставщик:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Location = new System.Drawing.Point(90, 12);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 21);
            // 
            // lblReceiptDate
            // 
            this.lblReceiptDate.AutoSize = true;
            this.lblReceiptDate.Location = new System.Drawing.Point(12, 45);
            this.lblReceiptDate.Name = "lblReceiptDate";
            this.lblReceiptDate.Size = new System.Drawing.Size(36, 13);
            this.lblReceiptDate.Text = "Дата:";
            // 
            // dtpReceiptDate
            // 
            this.dtpReceiptDate.Location = new System.Drawing.Point(90, 42);
            this.dtpReceiptDate.Name = "dtpReceiptDate";
            this.dtpReceiptDate.Size = new System.Drawing.Size(200, 20);
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.dataGridViewDetails);
            this.gbDetails.Location = new System.Drawing.Point(12, 70);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(500, 200);
            this.gbDetails.Text = "Товары";
            // 
            // dataGridViewDetails
            // 
            this.dataGridViewDetails.AllowUserToAddRows = false;
            this.dataGridViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDetails.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewDetails.Name = "dataGridViewDetails";
            this.dataGridViewDetails.Size = new System.Drawing.Size(494, 181);
            this.dataGridViewDetails.TabIndex = 0;
            // 
            // gbAddItem
            // 
            this.gbAddItem.Controls.Add(this.lblProduct);
            this.gbAddItem.Controls.Add(this.cmbProduct);
            this.gbAddItem.Controls.Add(this.lblQuantity);
            this.gbAddItem.Controls.Add(this.nudQuantity);
            this.gbAddItem.Controls.Add(this.lblPrice);
            this.gbAddItem.Controls.Add(this.nudPrice);
            this.gbAddItem.Controls.Add(this.lblProductionDate);
            this.gbAddItem.Controls.Add(this.dtpProductionDate);
            this.gbAddItem.Controls.Add(this.lblExpiryDate);
            this.gbAddItem.Controls.Add(this.dtpExpiryDate);
            this.gbAddItem.Controls.Add(this.btnAddRow);
            this.gbAddItem.Location = new System.Drawing.Point(12, 280);
            this.gbAddItem.Name = "gbAddItem";
            this.gbAddItem.Size = new System.Drawing.Size(500, 120);
            this.gbAddItem.Text = "Добавить товар";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(6, 22);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(41, 13);
            this.lblProduct.Text = "Товар:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Location = new System.Drawing.Point(60, 19);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(150, 21);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(220, 22);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(69, 13);
            this.lblQuantity.Text = "Количество:";
            // 
            // nudQuantity
            // 
            this.nudQuantity.DecimalPlaces = 3;
            this.nudQuantity.Location = new System.Drawing.Point(295, 20);
            this.nudQuantity.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(80, 20);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(380, 22);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(36, 13);
            this.lblPrice.Text = "Цена:";
            // 
            // nudPrice
            // 
            this.nudPrice.DecimalPlaces = 2;
            this.nudPrice.Location = new System.Drawing.Point(420, 20);
            this.nudPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(70, 20);
            // 
            // lblProductionDate
            // 
            this.lblProductionDate.AutoSize = true;
            this.lblProductionDate.Location = new System.Drawing.Point(6, 55);
            this.lblProductionDate.Name = "lblProductionDate";
            this.lblProductionDate.Size = new System.Drawing.Size(92, 13);
            this.lblProductionDate.Text = "Дата выработки:";
            // 
            // dtpProductionDate
            // 
            this.dtpProductionDate.Location = new System.Drawing.Point(104, 52);
            this.dtpProductionDate.Name = "dtpProductionDate";
            this.dtpProductionDate.Size = new System.Drawing.Size(120, 20);
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Location = new System.Drawing.Point(230, 55);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(80, 13);
            this.lblExpiryDate.Text = "Срок годности:";
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Location = new System.Drawing.Point(316, 52);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(120, 20);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(380, 85);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(110, 23);
            this.btnAddRow.Text = "Добавить в накладную";
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Location = new System.Drawing.Point(12, 406);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(110, 23);
            this.btnRemoveRow.Text = "Удалить строку";
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(350, 410);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 13);
            this.lblTotal.Text = "Итого:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(400, 407);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(130, 440);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 482);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnRemoveRow);
            this.Controls.Add(this.gbAddItem);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.dtpReceiptDate);
            this.Controls.Add(this.lblReceiptDate);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.lblSupplier);
            this.Name = "frmReceipt";
            this.Text = "Приходная накладная";
            this.gbDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).EndInit();
            this.gbAddItem.ResumeLayout(false);
            this.gbAddItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblReceiptDate;
        private System.Windows.Forms.DateTimePicker dtpReceiptDate;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.DataGridView dataGridViewDetails;
        private System.Windows.Forms.GroupBox gbAddItem;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label lblProductionDate;
        private System.Windows.Forms.DateTimePicker dtpProductionDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}