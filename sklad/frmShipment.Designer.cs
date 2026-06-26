namespace sklad
{
    partial class frmShipment
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblShipmentDate = new System.Windows.Forms.Label();
            this.dtpShipmentDate = new System.Windows.Forms.DateTimePicker();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.dataGridViewDetails = new System.Windows.Forms.DataGridView();
            this.gbAddItem = new System.Windows.Forms.GroupBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
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
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(12, 15);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(52, 13);
            this.lblCustomerName.Text = "Клиент:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(80, 12);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(200, 20);
            // 
            // lblShipmentDate
            // 
            this.lblShipmentDate.AutoSize = true;
            this.lblShipmentDate.Location = new System.Drawing.Point(12, 45);
            this.lblShipmentDate.Name = "lblShipmentDate";
            this.lblShipmentDate.Size = new System.Drawing.Size(36, 13);
            this.lblShipmentDate.Text = "Дата:";
            // 
            // dtpShipmentDate
            // 
            this.dtpShipmentDate.Location = new System.Drawing.Point(80, 42);
            this.dtpShipmentDate.Name = "dtpShipmentDate";
            this.dtpShipmentDate.Size = new System.Drawing.Size(200, 20);
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.dataGridViewDetails);
            this.gbDetails.Location = new System.Drawing.Point(12, 70);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(500, 200);
            this.gbDetails.Text = "Товары к отгрузке";
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
            this.gbAddItem.Controls.Add(this.btnAddRow);
            this.gbAddItem.Location = new System.Drawing.Point(12, 280);
            this.gbAddItem.Name = "gbAddItem";
            this.gbAddItem.Size = new System.Drawing.Size(500, 100);
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
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(380, 60);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(110, 23);
            this.btnAddRow.Text = "Добавить";
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Location = new System.Drawing.Point(12, 386);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(110, 23);
            this.btnRemoveRow.Text = "Удалить строку";
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(350, 390);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 13);
            this.lblTotal.Text = "Итого:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(400, 387);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(130, 430);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 430);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 472);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnRemoveRow);
            this.Controls.Add(this.gbAddItem);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.dtpShipmentDate);
            this.Controls.Add(this.lblShipmentDate);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.lblCustomerName);
            this.Name = "frmShipment";
            this.Text = "Расходная накладная";
            this.gbDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).EndInit();
            this.gbAddItem.ResumeLayout(false);
            this.gbAddItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblShipmentDate;
        private System.Windows.Forms.DateTimePicker dtpShipmentDate;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.DataGridView dataGridViewDetails;
        private System.Windows.Forms.GroupBox gbAddItem;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}