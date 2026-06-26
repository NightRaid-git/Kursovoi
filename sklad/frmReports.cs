using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sklad
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int selectedIndex = cmbReportType.SelectedIndex;
            switch (selectedIndex)
            {
                case 0: LoadRemainsReport(); break;
                case 1: LoadMovementReport(); break;
                case 2: LoadExpiredReport(); break;
                case 3: LoadReceiptsReport(); break;
                case 4: LoadShipmentsReport(); break;
                default: break;
            }
        }

        private void LoadRemainsReport()
        {
            string sql = @"SELECT p.ProductName, ISNULL(SUM(cs.Quantity), 0) AS Quantity, u.UnitName
                          FROM Products p
                          LEFT JOIN CurrentStock cs ON p.ID_Product = cs.ProductID
                          LEFT JOIN UnitsOfMeasure u ON p.UnitID = u.ID_Unit
                          GROUP BY p.ProductName, u.UnitName
                          ORDER BY p.ProductName";
            ExecuteReportQuery(sql, "Остатки товаров");
        }

        private void LoadMovementReport()
        {
            string sql = @"SELECT 'Приход' AS Operation, r.ReceiptNumber AS DocNumber, r.ReceiptDate AS DocDate, 
                                  p.ProductName, rd.Quantity, rd.Price, (rd.Quantity * rd.Price) AS Sum
                          FROM Receipts r
                          JOIN ReceiptDetails rd ON r.ID_Receipt = rd.ReceiptID
                          JOIN Products p ON rd.ProductID = p.ID_Product
                          UNION ALL
                          SELECT 'Расход', s.ShipmentNumber, s.ShipmentDate, 
                                 p.ProductName, sd.Quantity, sd.Price, (sd.Quantity * sd.Price) AS Sum
                          FROM Shipments s
                          JOIN ShipmentDetails sd ON s.ID_Shipment = sd.ShipmentID
                          JOIN Products p ON sd.ProductID = p.ID_Product
                          ORDER BY DocDate DESC";
            ExecuteReportQuery(sql, "Движение товаров");
        }

        private void LoadExpiredReport()
        {
            string sql = @"SELECT p.ProductName, cs.Quantity, cs.ProductionDate, cs.ExpiryDate, 
                                  DATEDIFF(day, GETDATE(), cs.ExpiryDate) AS DaysLeft
                          FROM CurrentStock cs
                          JOIN Products p ON cs.ProductID = p.ID_Product
                          WHERE cs.ExpiryDate < DATEADD(day, 3, GETDATE()) AND cs.Quantity > 0
                          ORDER BY cs.ExpiryDate";
            ExecuteReportQuery(sql, "Товары с истекающим сроком");
        }

        private void LoadReceiptsReport()
        {
            string sql = @"SELECT r.ReceiptNumber, r.ReceiptDate, s.SupplierName, 
                                  e.LastName + ' ' + e.FirstName AS Employee, r.TotalAmount
                          FROM Receipts r
                          JOIN Suppliers s ON r.SupplierID = s.ID_Supplier
                          JOIN Employees e ON r.EmployeeID = e.ID_Employee
                          ORDER BY r.ReceiptDate DESC";
            ExecuteReportQuery(sql, "Приходные накладные");
        }

        private void LoadShipmentsReport()
        {
            string sql = @"SELECT s.ShipmentNumber, s.ShipmentDate, s.CustomerName, 
                                  e.LastName + ' ' + e.FirstName AS Employee, s.TotalAmount
                          FROM Shipments s
                          JOIN Employees e ON s.EmployeeID = e.ID_Employee
                          ORDER BY s.ShipmentDate DESC";
            ExecuteReportQuery(sql, "Расходные накладные");
        }

        private void ExecuteReportQuery(string sql, string reportTitle)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewReports.DataSource = dt;
                dataGridViewReports.AutoResizeColumns();
            }
        }

        private DataTable GetCurrentDataTable()
        {
            if (dataGridViewReports.DataSource is DataTable dt)
                return dt.Copy();
            // Если источник не DataTable, создаём из строк
            dt = new DataTable();
            foreach (DataGridViewColumn col in dataGridViewReports.Columns)
            {
                dt.Columns.Add(col.Name, typeof(string));
            }
            foreach (DataGridViewRow row in dataGridViewReports.Rows)
            {
                if (row.IsNewRow) continue;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Value?.ToString() ?? "";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCurrentDataTable();
            if (dt.Rows.Count == 0) return;
            ExportHelper.ExportToExcel(dt, cmbReportType.Text, $"Отчёт_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCurrentDataTable();
            if (dt.Rows.Count == 0) return;
            ExportHelper.ExportToPdf(dt, cmbReportType.Text, $"Отчёт_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        }
    }
}