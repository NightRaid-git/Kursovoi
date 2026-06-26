namespace sklad
{
    partial class frmSelectNakladnaya
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabReceipts = new System.Windows.Forms.TabPage();
            this.dgvReceipts = new System.Windows.Forms.DataGridView();
            this.panelRecFilters = new System.Windows.Forms.Panel();
            this.tableRec = new System.Windows.Forms.TableLayoutPanel();
            this.lblRecNumber = new System.Windows.Forms.Label();
            this.txtRecNumber = new System.Windows.Forms.TextBox();
            this.lblRecSupplier = new System.Windows.Forms.Label();
            this.txtRecSupplier = new System.Windows.Forms.TextBox();
            this.lblRecFrom = new System.Windows.Forms.Label();
            this.dtRecFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRecTo = new System.Windows.Forms.Label();
            this.dtRecTo = new System.Windows.Forms.DateTimePicker();
            this.btnFilterReceipts = new System.Windows.Forms.Button();
            this.btnPrintReceipt = new System.Windows.Forms.Button();

            this.tabShipments = new System.Windows.Forms.TabPage();
            this.dgvShipments = new System.Windows.Forms.DataGridView();
            this.panelShFilters = new System.Windows.Forms.Panel();
            this.tableSh = new System.Windows.Forms.TableLayoutPanel();
            this.lblShNumber = new System.Windows.Forms.Label();
            this.txtShNumber = new System.Windows.Forms.TextBox();
            this.lblShCustomer = new System.Windows.Forms.Label();
            this.txtShCustomer = new System.Windows.Forms.TextBox();
            this.lblShFrom = new System.Windows.Forms.Label();
            this.dtShFrom = new System.Windows.Forms.DateTimePicker();
            this.lblShTo = new System.Windows.Forms.Label();
            this.dtShTo = new System.Windows.Forms.DateTimePicker();
            this.btnFilterShipments = new System.Windows.Forms.Button();
            this.btnPrintShipment = new System.Windows.Forms.Button();

            // ============================
            // TabControl
            // ============================
            this.tabControl1.Controls.Add(this.tabReceipts);
            this.tabControl1.Controls.Add(this.tabShipments);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;

            // ============================
            // TAB: ПРИХОДНЫЕ
            // ============================
            this.tabReceipts.Text = "Приходные";
            this.tabReceipts.Controls.Add(this.dgvReceipts);
            this.tabReceipts.Controls.Add(this.panelRecFilters);
            this.tabReceipts.Controls.Add(this.btnPrintReceipt);

            // DataGridView Receipts
            this.dgvReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceipts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipts_CellDoubleClick);

            // Panel Filters
            this.panelRecFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecFilters.Height = 70;

            // TableLayoutPanel (приходные)
            this.tableRec.ColumnCount = 5;
            this.tableRec.RowCount = 2;
            this.tableRec.Dock = System.Windows.Forms.DockStyle.Fill;

            this.tableRec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableRec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableRec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableRec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableRec.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));

            // Controls
            this.lblRecNumber.Text = "Номер";
            this.lblRecSupplier.Text = "Поставщик";
            this.lblRecFrom.Text = "Дата от";
            this.lblRecTo.Text = "Дата до";
            this.btnFilterReceipts.Text = "Фильтр";

            this.btnFilterReceipts.Click += new System.EventHandler(this.btnFilterReceipts_Click);

            // Add controls to table
            this.tableRec.Controls.Add(this.lblRecNumber, 0, 0);
            this.tableRec.Controls.Add(this.txtRecNumber, 0, 1);

            this.tableRec.Controls.Add(this.lblRecSupplier, 1, 0);
            this.tableRec.Controls.Add(this.txtRecSupplier, 1, 1);

            this.tableRec.Controls.Add(this.lblRecFrom, 2, 0);
            this.tableRec.Controls.Add(this.dtRecFrom, 2, 1);

            this.tableRec.Controls.Add(this.lblRecTo, 3, 0);
            this.tableRec.Controls.Add(this.dtRecTo, 3, 1);

            this.tableRec.Controls.Add(this.btnFilterReceipts, 4, 1);

            this.panelRecFilters.Controls.Add(this.tableRec);

            // Print button
            this.btnPrintReceipt.Text = "Печать";
            this.btnPrintReceipt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPrintReceipt.Height = 40;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);

            // ============================
            // TAB: РАСХОДНЫЕ
            // ============================
            this.tabShipments.Text = "Расходные";
            this.tabShipments.Controls.Add(this.dgvShipments);
            this.tabShipments.Controls.Add(this.panelShFilters);
            this.tabShipments.Controls.Add(this.btnPrintShipment);

            // DataGridView Shipments
            this.dgvShipments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShipments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShipments_CellDoubleClick);

            // Panel Filters
            this.panelShFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShFilters.Height = 70;

            // TableLayoutPanel (расходные)
            this.tableSh.ColumnCount = 5;
            this.tableSh.RowCount = 2;
            this.tableSh.Dock = System.Windows.Forms.DockStyle.Fill;

            this.tableSh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableSh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableSh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableSh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableSh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));

            // Controls
            this.lblShNumber.Text = "Номер";
            this.lblShCustomer.Text = "Клиент";
            this.lblShFrom.Text = "Дата от";
            this.lblShTo.Text = "Дата до";
            this.btnFilterShipments.Text = "Фильтр";

            this.btnFilterShipments.Click += new System.EventHandler(this.btnFilterShipments_Click);

            // Add controls to table
            this.tableSh.Controls.Add(this.lblShNumber, 0, 0);
            this.tableSh.Controls.Add(this.txtShNumber, 0, 1);

            this.tableSh.Controls.Add(this.lblShCustomer, 1, 0);
            this.tableSh.Controls.Add(this.txtShCustomer, 1, 1);

            this.tableSh.Controls.Add(this.lblShFrom, 2, 0);
            this.tableSh.Controls.Add(this.dtShFrom, 2, 1);

            this.tableSh.Controls.Add(this.lblShTo, 3, 0);
            this.tableSh.Controls.Add(this.dtShTo, 3, 1);

            this.tableSh.Controls.Add(this.btnFilterShipments, 4, 1);

            this.panelShFilters.Controls.Add(this.tableSh);

            // Print button
            this.btnPrintShipment.Text = "Печать";
            this.btnPrintShipment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPrintShipment.Height = 40;
            this.btnPrintShipment.Click += new System.EventHandler(this.btnPrintShipment_Click);

            // ============================
            // Form
            // ============================
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabControl1);
            this.Text = "Выбор накладной";
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabReceipts;
        private System.Windows.Forms.TabPage tabShipments;

        private System.Windows.Forms.DataGridView dgvReceipts;
        private System.Windows.Forms.DataGridView dgvShipments;

        private System.Windows.Forms.Panel panelRecFilters;
        private System.Windows.Forms.Panel panelShFilters;

        private System.Windows.Forms.TableLayoutPanel tableRec;
        private System.Windows.Forms.TableLayoutPanel tableSh;

        private System.Windows.Forms.Label lblRecNumber;
        private System.Windows.Forms.TextBox txtRecNumber;
        private System.Windows.Forms.Label lblRecSupplier;
        private System.Windows.Forms.TextBox txtRecSupplier;
        private System.Windows.Forms.Label lblRecFrom;
        private System.Windows.Forms.DateTimePicker dtRecFrom;
        private System.Windows.Forms.Label lblRecTo;
        private System.Windows.Forms.DateTimePicker dtRecTo;
        private System.Windows.Forms.Button btnFilterReceipts;
        private System.Windows.Forms.Button btnPrintReceipt;

        private System.Windows.Forms.Label lblShNumber;
        private System.Windows.Forms.TextBox txtShNumber;
        private System.Windows.Forms.Label lblShCustomer;
        private System.Windows.Forms.TextBox txtShCustomer;
        private System.Windows.Forms.Label lblShFrom;
        private System.Windows.Forms.DateTimePicker dtShFrom;
        private System.Windows.Forms.Label lblShTo;
        private System.Windows.Forms.DateTimePicker dtShTo;
        private System.Windows.Forms.Button btnFilterShipments;
        private System.Windows.Forms.Button btnPrintShipment;
    }
}
