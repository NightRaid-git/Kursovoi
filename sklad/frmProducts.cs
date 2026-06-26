using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
            LoadProducts();
        }

        private DataTable fullProductsTable;

        private void LoadProducts()
        {
            fullProductsTable = new DataTable();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.ID_Product, p.ProductName, c.CategoryName, u.UnitName, 
                                p.ShelfLifeDays, p.StorageCondition, p.PurchasePrice, p.SellingPrice
                         FROM Products p
                         JOIN Categories c ON p.CategoryID = c.ID_Category
                         JOIN UnitsOfMeasure u ON p.UnitID = u.ID_Unit";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(fullProductsTable);
            }
            dataGridViewProducts.DataSource = fullProductsTable;
            dataGridViewProducts.AutoResizeColumns();
        }

        private void ApplyFilter(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                dataGridViewProducts.DataSource = fullProductsTable;
                return;
            }

            DataTable filtered = fullProductsTable.Clone();
            string filter = filterText.ToLower();
            foreach (DataRow row in fullProductsTable.Rows)
            {
                bool match = false;
                foreach (DataColumn col in fullProductsTable.Columns)
                {
                    if (col.DataType == typeof(string) || col.DataType == typeof(decimal) || col.DataType == typeof(int))
                    {
                        string val = row[col]?.ToString()?.ToLower();
                        if (!string.IsNullOrEmpty(val) && val.Contains(filter))
                        {
                            match = true;
                            break;
                        }
                    }
                }
                if (match)
                    filtered.ImportRow(row);
            }
            dataGridViewProducts.DataSource = filtered;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter(txtSearch.Text.Trim());
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ApplyFilter("");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductEdit frm = new frmProductEdit(0);
            if (frm.ShowDialog() == DialogResult.OK)
                LoadProducts();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridViewProducts.CurrentRow.Cells["ID_Product"].Value);
            frmProductEdit frm = new frmProductEdit(id);
            if (frm.ShowDialog() == DialogResult.OK)
                LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.CurrentRow == null) return;
            if (MessageBox.Show("Удалить товар? Будут удалены все связанные операции!", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridViewProducts.CurrentRow.Cells["ID_Product"].Value);
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ID_Product = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadProducts();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}