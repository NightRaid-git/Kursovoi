using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace sklad
{
    public partial class frmSupplierEdit : Form
    {
        private int supplierId;
        public frmSupplierEdit(int id)
        {
            InitializeComponent();
            supplierId = id;
            if (id > 0) LoadSupplier();
        }

        private void LoadSupplier()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT SupplierName, INN, Phone, Address FROM Suppliers WHERE ID_Supplier = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", supplierId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["SupplierName"].ToString();
                    txtINN.Text = reader["INN"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                if (supplierId == 0)
                {
                    string insert = @"INSERT INTO Suppliers (SupplierName, INN, Phone, Address) 
                                      VALUES (@name, @inn, @phone, @address)";
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@inn", txtINN.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string update = @"UPDATE Suppliers SET SupplierName = @name, INN = @inn, Phone = @phone, Address = @address
                                      WHERE ID_Supplier = @id";
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("@id", supplierId);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@inn", txtINN.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}