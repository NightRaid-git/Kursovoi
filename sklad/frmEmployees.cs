// frmEmployees.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmEmployees : Form
    {
        private string currentRole;
        public frmEmployees(string role)
        {
            InitializeComponent();
            currentRole = role;
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT e.ID_Employee, e.LastName, e.FirstName, e.MiddleName, e.Position, 
                                        e.Phone, u.Login, u.Role, u.IsActive 
                                 FROM Employees e
                                 LEFT JOIN Users u ON e.ID_Employee = u.EmployeeID";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            dataGridViewEmployees.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // форма добавления нового сотрудника и пользователя
            frmAddEditEmployee frm = new frmAddEditEmployee(0);
            if (frm.ShowDialog() == DialogResult.OK)
                LoadEmployees();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridViewEmployees.CurrentRow.Cells["ID_Employee"].Value);
            frmAddEditEmployee frm = new frmAddEditEmployee(id);
            if (frm.ShowDialog() == DialogResult.OK)
                LoadEmployees();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridViewEmployees.CurrentRow.Cells["ID_Employee"].Value);
            if (MessageBox.Show("Деактивировать сотрудника? Вход в систему станет невозможным.", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Employees SET IsActive = 0 WHERE ID_Employee = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    // также деактивировать пользователя
                    SqlCommand cmdUser = new SqlCommand("UPDATE Users SET IsActive = 0 WHERE EmployeeID = @id", conn);
                    cmdUser.Parameters.AddWithValue("@id", id);
                    cmdUser.ExecuteNonQuery();
                }
                LoadEmployees();
            }
        }
    }
}