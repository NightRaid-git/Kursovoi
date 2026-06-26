using ClosedXML.Excel;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Data;
using System.Windows.Forms;

namespace sklad
{
    public static class ExportHelper
    {
        // ============================================================
        // 1. УНИВЕРСАЛЬНЫЙ EXCEL (для frmReports)
        // ============================================================
        public static void ExportToExcel(DataTable data, string title, string defaultFileName = "report.xlsx")
        {
            if (data == null || data.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = defaultFileName;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Отчёт");

                ws.Cell("A1").SetValue(title);
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 16;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(1, 1, 1, data.Columns.Count).Merge();

                // Заголовки
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    var cell = ws.Cell(3, i + 1);
                    cell.SetValue(data.Columns[i].ColumnName);
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Данные
                for (int r = 0; r < data.Rows.Count; r++)
                {
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        var cell = ws.Cell(r + 4, c + 1);
                        var value = data.Rows[r][c];

                        if (value is decimal dec) cell.SetValue(dec);
                        else if (value is int i) cell.SetValue(i);
                        else if (value is double d) cell.SetValue(d);
                        else cell.SetValue(value?.ToString());

                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }

                ws.Columns().AdjustToContents();
                wb.SaveAs(sfd.FileName);

                MessageBox.Show("Excel сохранён");
            }
        }

        // ============================================================
        // 2. УНИВЕРСАЛЬНЫЙ PDF (для frmReports)
        // ============================================================
        public static void ExportToPdf(DataTable data, string title, string defaultFileName = "report.pdf")
        {
            if (data == null || data.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = defaultFileName;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var doc = new PdfDocument();
                var page = doc.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var fontTitle = new XFont("Arial", 14, XFontStyle.Bold);
                var fontHeader = new XFont("Arial", 10, XFontStyle.Bold);
                var fontText = new XFont("Arial", 9);

                gfx.DrawString(title, fontTitle, XBrushes.Black,
                    new XRect(0, 20, page.Width, 20), XStringFormats.TopCenter);

                float startX = 30;
                float startY = 60;
                float rowHeight = 20;
                float totalWidth = (float)(page.Width - 60);
                int colCount = data.Columns.Count;
                float colWidth = totalWidth / colCount;

                float y = startY;

                // Заголовки
                for (int i = 0; i < colCount; i++)
                {
                    float x = startX + i * colWidth;
                    gfx.DrawRectangle(XPens.Black, x, y, colWidth, rowHeight);
                    gfx.DrawString(data.Columns[i].ColumnName, fontHeader, XBrushes.Black,
                        new XRect(x, y, colWidth, rowHeight), XStringFormats.Center);
                }

                y += rowHeight;

                // Данные
                for (int r = 0; r < data.Rows.Count; r++)
                {
                    if (y + rowHeight > page.Height - 40)
                    {
                        page = doc.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = startY;

                        for (int i = 0; i < colCount; i++)
                        {
                            float x = startX + i * colWidth;
                            gfx.DrawRectangle(XPens.Black, x, y, colWidth, rowHeight);
                            gfx.DrawString(data.Columns[i].ColumnName, fontHeader, XBrushes.Black,
                                new XRect(x, y, colWidth, rowHeight), XStringFormats.Center);
                        }

                        y += rowHeight;
                    }

                    for (int c = 0; c < colCount; c++)
                    {
                        float x = startX + c * colWidth;
                        gfx.DrawRectangle(XPens.Black, x, y, colWidth, rowHeight);
                        gfx.DrawString(data.Rows[r][c]?.ToString() ?? "",
                            fontText, XBrushes.Black,
                            new XRect(x + 3, y + 3, colWidth, rowHeight),
                            XStringFormats.TopLeft);
                    }

                    y += rowHeight;
                }

                doc.Save(sfd.FileName);
                MessageBox.Show("PDF сохранён");
            }
        }

        // ============================================================
        // 3. EXCEL НАКЛАДНАЯ (frmPrintNakladnaya)
        // ============================================================
        public static void ExportInvoiceToExcel(
            DataTable details,
            string invoiceNumber,
            string customer,
            string employee,
            DateTime date,
            decimal total,
            string defaultFileName = "Накладная.xlsx")
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = defaultFileName;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Накладная");

                int row = 1;

                ws.Cell(row, 1).SetValue("РАСХОДНАЯ НАКЛАДНАЯ");
                ws.Range(row, 1, row, 6).Merge();
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Cell(row, 1).Style.Font.FontSize = 18;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                row += 2;

                ws.Cell(row, 1).SetValue("Номер:");
                ws.Cell(row, 2).SetValue(invoiceNumber);
                row++;

                ws.Cell(row, 1).SetValue("Дата:");
                ws.Cell(row, 2).SetValue(date.ToShortDateString());
                row++;

                ws.Cell(row, 1).SetValue("Клиент:");
                ws.Cell(row, 2).SetValue(customer);
                row++;

                ws.Cell(row, 1).SetValue("Ответственный:");
                ws.Cell(row, 2).SetValue(employee);
                row += 2;

                string[] headers = { "Товар", "Кол-во", "Цена", "Сумма" };

                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cell(row, i + 1);
                    cell.SetValue(headers[i]);
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                row++;

                foreach (DataRow dr in details.Rows)
                {
                    ws.Cell(row, 1).SetValue(dr["ProductName"]?.ToString());
                    ws.Cell(row, 2).SetValue(Convert.ToDecimal(dr["Quantity"]));
                    ws.Cell(row, 3).SetValue(Convert.ToDecimal(dr["Price"]));
                    ws.Cell(row, 4).SetValue(Convert.ToDecimal(dr["Sum"]));

                    for (int c = 1; c <= 4; c++)
                        ws.Cell(row, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    row++;
                }

                ws.Cell(row, 3).SetValue("ИТОГО:");
                ws.Cell(row, 4).SetValue(total);
                ws.Cell(row, 4).Style.Font.Bold = true;

                row += 3;

                ws.Cell(row, 1).SetValue("Отпустил:");
                ws.Cell(row, 3).SetValue("Принял:");

                ws.Columns().AdjustToContents();
                wb.SaveAs(sfd.FileName);

                MessageBox.Show("Excel накладная сохранена");
            }
        }

        // ============================================================
        // 4. PDF НАКЛАДНАЯ (frmPrintNakladnaya)
        // ============================================================
        public static void ExportInvoiceToPdf(
            DataTable details,
            string invoiceNumber,
            string customer,
            string employee,
            DateTime date,
            decimal total,
            string defaultFileName = "Накладная.pdf")
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = defaultFileName;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var doc = new PdfDocument();
                var page = doc.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var fontTitle = new XFont("Arial", 16, XFontStyle.Bold);
                var fontHeader = new XFont("Arial", 10, XFontStyle.Bold);
                var fontText = new XFont("Arial", 10);

                float y = 40;

                gfx.DrawString("РАСХОДНАЯ НАКЛАДНАЯ", fontTitle, XBrushes.Black,
                    new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);
                y += 40;

                gfx.DrawString($"Номер: {invoiceNumber}", fontText, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Дата: {date:dd.MM.yyyy}", fontText, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Клиент: {customer}", fontText, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Ответственный: {employee}", fontText, XBrushes.Black, 40, y); y += 30;

                float startX = 40;
                float rowHeight = 20;
                float[] colWidths = { 250, 60, 80, 80 };

                string[] headers = { "Товар", "Кол-во", "Цена", "Сумма" };

                for (int i = 0; i < headers.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, startX, y, colWidths[i], rowHeight);
                    gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                        new XRect(startX, y, colWidths[i], rowHeight), XStringFormats.Center);
                    startX += colWidths[i];
                }

                y += rowHeight;

                foreach (DataRow dr in details.Rows)
                {
                    startX = 40;

                    string[] rowData =
                    {
                        dr["ProductName"].ToString(),
                        dr["Quantity"].ToString(),
                        Convert.ToDecimal(dr["Price"]).ToString("N2"),
                        Convert.ToDecimal(dr["Sum"]).ToString("N2")
                    };

                    for (int i = 0; i < rowData.Length; i++)
                    {
                        gfx.DrawRectangle(XPens.Black, startX, y, colWidths[i], rowHeight);
                        gfx.DrawString(rowData[i], fontText, XBrushes.Black,
                            new XRect(startX + 3, y + 3, colWidths[i], rowHeight), XStringFormats.TopLeft);
                        startX += colWidths[i];
                    }

                    y += rowHeight;
                }

                y += 20;

                gfx.DrawString($"ИТОГО: {total:N2} руб.", fontHeader, XBrushes.Black, 40, y);
                y += 40;

                gfx.DrawString("Отпустил: ____________________", fontText, XBrushes.Black, 40, y);
                gfx.DrawString("Принял: ____________________", fontText, XBrushes.Black, 300, y);

                doc.Save(sfd.FileName);
                MessageBox.Show("PDF накладная сохранена");
            }
        }
    }
}
