using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace sklad
{
    public partial class frmProductEdit : Form
    {
        private int productId;
        public frmProductEdit(int id)
        {
            InitializeComponent();
            productId = id;
            LoadCategories();
            LoadUnits();
            if (id > 0) LoadProduct();
        }

        private void LoadCategories()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Category, CategoryName FROM Categories", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "ID_Category";
            }
        }

        private void LoadUnits()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Unit, UnitName FROM UnitsOfMeasure", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbUnit.DataSource = dt;
                cmbUnit.DisplayMember = "UnitName";
                cmbUnit.ValueMember = "ID_Unit";
            }
        }

        private void LoadProduct()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT ProductName, CategoryID, UnitID, ShelfLifeDays, StorageCondition, PurchasePrice, SellingPrice FROM Products WHERE ID_Product = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", productId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["ProductName"].ToString();
                    cmbCategory.SelectedValue = reader["CategoryID"];
                    cmbUnit.SelectedValue = reader["UnitID"];
                    txtShelfLife.Text = reader["ShelfLifeDays"].ToString();
                    txtStorage.Text = reader["StorageCondition"].ToString();
                    txtPurchasePrice.Text = reader["PurchasePrice"].ToString();
                    txtSellingPrice.Text = reader["SellingPrice"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название товара");
                return;
            }
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                if (productId == 0)
                {
                    string insert = @"INSERT INTO Products (ProductName, CategoryID, UnitID, ShelfLifeDays, StorageCondition, PurchasePrice, SellingPrice) 
                                      VALUES (@name, @cat, @unit, @shelf, @storage, @purchase, @sell)";
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@cat", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@unit", cmbUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@shelf", Convert.ToInt32(txtShelfLife.Text));
                    cmd.Parameters.AddWithValue("@storage", txtStorage.Text);
                    cmd.Parameters.AddWithValue("@purchase", Convert.ToDecimal(txtPurchasePrice.Text));
                    cmd.Parameters.AddWithValue("@sell", Convert.ToDecimal(txtSellingPrice.Text));
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string update = @"UPDATE Products SET ProductName = @name, CategoryID = @cat, UnitID = @unit, 
                                      ShelfLifeDays = @shelf, StorageCondition = @storage, PurchasePrice = @purchase, SellingPrice = @sell
                                      WHERE ID_Product = @id";
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@cat", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@unit", cmbUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@shelf", Convert.ToInt32(txtShelfLife.Text));
                    cmd.Parameters.AddWithValue("@storage", txtStorage.Text);
                    cmd.Parameters.AddWithValue("@purchase", Convert.ToDecimal(txtPurchasePrice.Text));
                    cmd.Parameters.AddWithValue("@sell", Convert.ToDecimal(txtSellingPrice.Text));
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