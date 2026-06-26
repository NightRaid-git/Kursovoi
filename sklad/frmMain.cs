using System;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmMain : Form
    {
        private string userRole;
        private int userId;
        private int employeeId;

        public frmMain(string role, int uid, int empId)
        {
            InitializeComponent();
            userRole = role;
            userId = uid;
            employeeId = empId;
            ConfigureMenuByRole();
        }

        /// <summary>
        /// Настраивает видимость пунктов меню в зависимости от роли пользователя.
        /// </summary>
        private void ConfigureMenuByRole()
        {
            // По умолчанию всё видимо, скрываем ненужные пункты для разных ролей
            switch (userRole)
            {
                case "Administrator":
                    // Администратор видит всё
                    сотрудникиToolStripMenuItem.Visible = true;
                    приходToolStripMenuItem.Visible = true;
                    расходToolStripMenuItem.Visible = true;
                    списаниеToolStripMenuItem.Visible = true;
                    инвентаризацияToolStripMenuItem.Visible = true;
                    отчетыToolStripMenuItem.Visible = true;
                    break;

                case "Manager":
                    // Менеджер может управлять сотрудниками и смотреть отчёты
                    сотрудникиToolStripMenuItem.Visible = true;
                    приходToolStripMenuItem.Visible = false;
                    расходToolStripMenuItem.Visible = false;
                    списаниеToolStripMenuItem.Visible = false;
                    инвентаризацияToolStripMenuItem.Visible = false;
                    отчетыToolStripMenuItem.Visible = true;
                    break;

                case "Storekeeper":
                    // Кладовщик: приход, расход, списание, инвентаризация, остатки
                    сотрудникиToolStripMenuItem.Visible = false;
                    приходToolStripMenuItem.Visible = true;
                    расходToolStripMenuItem.Visible = true;
                    списаниеToolStripMenuItem.Visible = true;
                    инвентаризацияToolStripMenuItem.Visible = true;
                    отчетыToolStripMenuItem.Visible = true;
                    break;

                case "Accountant":
                    // Бухгалтер: только отчёты
                    сотрудникиToolStripMenuItem.Visible = false;
                    приходToolStripMenuItem.Visible = false;
                    расходToolStripMenuItem.Visible = false;
                    списаниеToolStripMenuItem.Visible = false;
                    инвентаризацияToolStripMenuItem.Visible = false;
                    отчетыToolStripMenuItem.Visible = true;
                    break;

                default:
                    // На всякий случай
                    сотрудникиToolStripMenuItem.Visible = false;
                    приходToolStripMenuItem.Visible = false;
                    расходToolStripMenuItem.Visible = false;
                    списаниеToolStripMenuItem.Visible = false;
                    инвентаризацияToolStripMenuItem.Visible = false;
                    отчетыToolStripMenuItem.Visible = true;
                    break;
            }
        }


        private void товарыToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmProducts frm = new frmProducts();
            frm.ShowDialog();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmSuppliers frm = new frmSuppliers();
            frm.ShowDialog();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmEmployees frm = new frmEmployees(userRole);
            frm.ShowDialog();
        }

        private void приходToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmReceipt frm = new frmReceipt(employeeId);
            frm.ShowDialog();
        }

        private void расходToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmShipment frm = new frmShipment(employeeId);
            frm.ShowDialog();
        }

        private void списаниеToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmWriteOff frm = new frmWriteOff(employeeId);
            frm.ShowDialog();
        }

        private void остаткиToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmStockView frm = new frmStockView();
            frm.ShowDialog();
        }

        private void инвентаризацияToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmInventory frm = new frmInventory(employeeId);
            frm.ShowDialog();
        }

        private void отчетыToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmReports frm = new frmReports();
            frm.ShowDialog();
        }
        private void накладныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSelectNakladnaya().ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {

        }
    }
}