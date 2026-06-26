using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmAddEditEmployee : Form
    {
        private int employeeId;
        private bool isEditMode;

        public frmAddEditEmployee(int id)
        {
            InitializeComponent();
            employeeId = id;
            isEditMode = (id > 0);
            LoadRoles();
            if (isEditMode)
                LoadEmployeeData();
            else
                chkActive.Checked = true;
        }

        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new string[] { "Administrator", "Manager", "Storekeeper", "Accountant" });
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadEmployeeData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT e.LastName, e.FirstName, e.MiddleName, e.Position, e.Phone, e.IsActive,
                           u.Login, u.Role
                    FROM Employees e
                    LEFT JOIN Users u ON e.ID_Employee = u.EmployeeID
                    WHERE e.ID_Employee = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", employeeId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtLastName.Text = reader["LastName"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtMiddleName.Text = reader["MiddleName"].ToString();
                    txtPosition.Text = reader["Position"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    chkActive.Checked = Convert.ToBoolean(reader["IsActive"]);
                    txtLogin.Text = reader["Login"].ToString();
                    string role = reader["Role"].ToString();
                    if (cmbRole.Items.Contains(role))
                        cmbRole.SelectedItem = role;
                    // пароль не загружаем (хеш), оставляем пустым
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Фамилия и имя обязательны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль для нового пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    if (!isEditMode)
                    {
                        // 1. Вставка в Employees
                        string insertEmployee = @"
                            INSERT INTO Employees (LastName, FirstName, MiddleName, Position, Phone, IsActive)
                            VALUES (@last, @first, @middle, @pos, @phone, @active);
                            SELECT SCOPE_IDENTITY();";
                        SqlCommand cmdEmp = new SqlCommand(insertEmployee, conn, transaction);
                        cmdEmp.Parameters.AddWithValue("@last", txtLastName.Text);
                        cmdEmp.Parameters.AddWithValue("@first", txtFirstName.Text);
                        cmdEmp.Parameters.AddWithValue("@middle", txtMiddleName.Text);
                        cmdEmp.Parameters.AddWithValue("@pos", txtPosition.Text);
                        cmdEmp.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmdEmp.Parameters.AddWithValue("@active", chkActive.Checked);
                        int newId = Convert.ToInt32(cmdEmp.ExecuteScalar());
                        employeeId = newId;

                        // 2. Вставка в Users
                        string insertUser = @"
                            INSERT INTO Users (Login, PasswordHash, Role, EmployeeID, IsActive)
                            VALUES (@login, @hash, @role, @empId, 1)";
                        SqlCommand cmdUser = new SqlCommand(insertUser, conn, transaction);
                        cmdUser.Parameters.AddWithValue("@login", txtLogin.Text);
                        cmdUser.Parameters.AddWithValue("@hash", PasswordHelper.HashPassword(txtPassword.Text));
                        cmdUser.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                        cmdUser.Parameters.AddWithValue("@empId", employeeId);
                        cmdUser.ExecuteNonQuery();
                    }
                    else
                    {
                        // Обновление Employees
                        string updateEmployee = @"
                            UPDATE Employees SET 
                                LastName = @last, FirstName = @first, MiddleName = @middle,
                                Position = @pos, Phone = @phone, IsActive = @active
                            WHERE ID_Employee = @id";
                        SqlCommand cmdEmp = new SqlCommand(updateEmployee, conn, transaction);
                        cmdEmp.Parameters.AddWithValue("@id", employeeId);
                        cmdEmp.Parameters.AddWithValue("@last", txtLastName.Text);
                        cmdEmp.Parameters.AddWithValue("@first", txtFirstName.Text);
                        cmdEmp.Parameters.AddWithValue("@middle", txtMiddleName.Text);
                        cmdEmp.Parameters.AddWithValue("@pos", txtPosition.Text);
                        cmdEmp.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmdEmp.Parameters.AddWithValue("@active", chkActive.Checked);
                        cmdEmp.ExecuteNonQuery();

                        // Обновление Users (логин, роль, пароль если изменён)
                        string updateUser = @"
                            UPDATE Users SET 
                                Login = @login,
                                Role = @role,
                                PasswordHash = CASE WHEN @newHash IS NOT NULL THEN @newHash ELSE PasswordHash END,
                                IsActive = @active
                            WHERE EmployeeID = @empId";
                        SqlCommand cmdUser = new SqlCommand(updateUser, conn, transaction);
                        cmdUser.Parameters.AddWithValue("@empId", employeeId);
                        cmdUser.Parameters.AddWithValue("@login", txtLogin.Text);
                        cmdUser.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                        cmdUser.Parameters.AddWithValue("@active", 1); // пользователь всегда активен, если сотрудник активен
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                            cmdUser.Parameters.AddWithValue("@newHash", PasswordHelper.HashPassword(txtPassword.Text));
                        else
                            cmdUser.Parameters.AddWithValue("@newHash", DBNull.Value);
                        cmdUser.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}