namespace sklad
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.товарыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поставщикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.складToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остаткиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инвентаризацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.документыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.приходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.складToolStripMenuItem,
            this.документыToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.товарыToolStripMenuItem,
            this.поставщикиToolStripMenuItem,
            this.сотрудникиToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // товарыToolStripMenuItem
            // 
            this.товарыToolStripMenuItem.Name = "товарыToolStripMenuItem";
            this.товарыToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.товарыToolStripMenuItem.Text = "Товары";
            this.товарыToolStripMenuItem.Click += new System.EventHandler(this.товарыToolStripMenuItem_Click);
            // 
            // поставщикиToolStripMenuItem
            // 
            this.поставщикиToolStripMenuItem.Name = "поставщикиToolStripMenuItem";
            this.поставщикиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.поставщикиToolStripMenuItem.Text = "Поставщики";
            this.поставщикиToolStripMenuItem.Click += new System.EventHandler(this.поставщикиToolStripMenuItem_Click);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.сотрудникиToolStripMenuItem_Click);
            // 
            // складToolStripMenuItem
            // 
            this.складToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.остаткиToolStripMenuItem,
            this.инвентаризацияToolStripMenuItem});
            this.складToolStripMenuItem.Name = "складToolStripMenuItem";
            this.складToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.складToolStripMenuItem.Text = "Склад";
            // 
            // остаткиToolStripMenuItem
            // 
            this.остаткиToolStripMenuItem.Name = "остаткиToolStripMenuItem";
            this.остаткиToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.остаткиToolStripMenuItem.Text = "Текущие остатки";
            this.остаткиToolStripMenuItem.Click += new System.EventHandler(this.остаткиToolStripMenuItem_Click);
            // 
            // инвентаризацияToolStripMenuItem
            // 
            this.инвентаризацияToolStripMenuItem.Name = "инвентаризацияToolStripMenuItem";
            this.инвентаризацияToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.инвентаризацияToolStripMenuItem.Text = "Инвентаризация";
            this.инвентаризацияToolStripMenuItem.Click += new System.EventHandler(this.инвентаризацияToolStripMenuItem_Click);
            // накладныеToolStripMenuItem
            this.накладныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.накладныеToolStripMenuItem.Name = "накладныеToolStripMenuItem";
            this.накладныеToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.накладныеToolStripMenuItem.Text = "Печать накладных";
            this.накладныеToolStripMenuItem.Click += new System.EventHandler(this.накладныеToolStripMenuItem_Click);
            // 
            // документыToolStripMenuItem
            // 
            this.документыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.приходToolStripMenuItem,
            this.расходToolStripMenuItem,
            this.списаниеToolStripMenuItem,
            this.накладныеToolStripMenuItem});
            this.документыToolStripMenuItem.Name = "документыToolStripMenuItem";
            this.документыToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.документыToolStripMenuItem.Text = "Документы";
            // 
            // приходToolStripMenuItem
            // 

            this.приходToolStripMenuItem.Name = "приходToolStripMenuItem";
            this.приходToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.приходToolStripMenuItem.Text = "Приходная накладная";
            this.приходToolStripMenuItem.Click += new System.EventHandler(this.приходToolStripMenuItem_Click);

            // 
            // расходToolStripMenuItem
            // 
            this.расходToolStripMenuItem.Name = "расходToolStripMenuItem";
            this.расходToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.расходToolStripMenuItem.Text = "Расходная накладная";
            this.расходToolStripMenuItem.Click += new System.EventHandler(this.расходToolStripMenuItem_Click);
            // 
            // списаниеToolStripMenuItem
            // 
            this.списаниеToolStripMenuItem.Name = "списаниеToolStripMenuItem";
            this.списаниеToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.списаниеToolStripMenuItem.Text = "Списание товаров";
            this.списаниеToolStripMenuItem.Click += new System.EventHandler(this.списаниеToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчёты";
            this.отчетыToolStripMenuItem.Click += new System.EventHandler(this.отчетыToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Главная - Учёт продовольственного склада";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem товарыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поставщикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem складToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem остаткиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инвентаризацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem документыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem приходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem расходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem накладныеToolStripMenuItem;

    }
}