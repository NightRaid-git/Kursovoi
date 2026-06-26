using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmSuppliers : Form
    {
        public frmSuppliers()
        {
            InitializeComponent();
            LoadSuppliers();
        }

        private DataTable fullSuppliersTable;

        private void LoadSuppliers()
        {
            fullSuppliersTable = new DataTable();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT ID_Supplier, SupplierName, INN, Phone, Address FROM Suppliers";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(fullSuppliersTable);
            }
            dataGridViewSuppliers.DataSource = fullSuppliersTable;
        }

        private void ApplyFilter(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                dataGridViewSuppliers.DataSource = fullSuppliersTable;
                return;
            }

            DataTable filtered = fullSuppliersTable.Clone();
            string filter = filterText.ToLower();
            foreach (DataRow row in fullSuppliersTable.Rows)
            {
                bool match = false;
                foreach (DataColumn col in fullSuppliersTable.Columns)
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
            dataGridViewSuppliers.DataSource = filtered;
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
            frmSupplierEdit frm = new frmSupplierEdit(0);
            if (frm.ShowDialog() == DialogResult.OK)
                LoadSuppliers();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewSuppliers.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridViewSuppliers.CurrentRow.Cells["ID_Supplier"].Value);
            frmSupplierEdit frm = new frmSupplierEdit(id);
            if (frm.ShowDialog() == DialogResult.OK)
                LoadSuppliers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSuppliers.CurrentRow == null) return;
            if (MessageBox.Show("Удалить поставщика? Это действие нельзя отменить.", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridViewSuppliers.CurrentRow.Cells["ID_Supplier"].Value);
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Suppliers WHERE ID_Supplier = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadSuppliers();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }
    }
}