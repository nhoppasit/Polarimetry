using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Polarimeter_2020_Unit_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SaveData();
        }

        void AssignTestDetailsSheetForSave(ref HSSFWorkbook workbook, ref ISheet sheet, string gpibAddrDMM34401A, string gpibAddrMMC2)
        {
            ICellStyle headerCellStyle1 = workbook.CreateCellStyle();
            headerCellStyle1.FillPattern = FillPattern.SolidForeground;
            headerCellStyle1.FillForegroundColor = IndexedColors.BrightGreen.Index;
            headerCellStyle1.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            headerCellStyle1.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
            headerCellStyle1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            headerCellStyle1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;

            ICellStyle headerCellStyle2 = workbook.CreateCellStyle();
            headerCellStyle2.FillPattern = FillPattern.SolidForeground;
            headerCellStyle2.FillForegroundColor = IndexedColors.LightGreen.Index;
            headerCellStyle2.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            headerCellStyle2.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
            headerCellStyle2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            headerCellStyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;

            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue("Date of creation");
            cell.CellStyle = headerCellStyle1;

            cell = row.CreateCell(1);
            cell.SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));  //วันที่

            row = sheet.CreateRow(1);
            cell = row.CreateCell(0);
            cell.SetCellValue("Time of creation");
            cell.CellStyle = headerCellStyle1;

            cell = row.CreateCell(1);
            cell.SetCellValue(DateTime.Now.ToString("HH:mm"));

            row = sheet.CreateRow(2);
            cell = row.CreateCell(0);
            cell.SetCellValue("DMM-34401A GPIB Address");
            cell.CellStyle = headerCellStyle2;

            cell = row.CreateCell(1);
            cell.SetCellValue(gpibAddrDMM34401A);

            row = sheet.CreateRow(3);
            cell = row.CreateCell(0);
            cell.SetCellValue("MMC-2 GPIB Address");
            cell.CellStyle = headerCellStyle2;

            cell = row.CreateCell(1);
            cell.SetCellValue(gpibAddrMMC2);

            //sheet.SetColumnWidth(0, 30 * 256);
            sheet.AutoSizeColumn(0);
        }

        private void SaveData()
        {
            // SaveFileDialog dlg = new SaveFileDialog();
            // DialogResult redlg = dlg.ShowDialog();
            SaveFileDialog dlg = new SaveFileDialog();
            //  dlg.Filter = "*.*|All Type, *.xls|Excel";
            dlg.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            DialogResult redlg = dlg.ShowDialog();
            if (redlg != DialogResult.OK)
            {
                MessageBox.Show("Good luck and Bye", " Don't Save?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //-----------------------------------------SAVE----------------------------------------------
            string fullFilePath = dlg.FileName;
            ////สร้าง Workbook และ worksheet ชื่อ Sheet1 และ Sheet2 
            HSSFWorkbook workbook = new HSSFWorkbook();
            var sheet1 = workbook.CreateSheet("Test Details");
            var sheet2 = workbook.CreateSheet("Sheet2");

            AssignTestDetailsSheetForSave(ref workbook, ref sheet1, "Addr1", "Addr2");

            ////จากนั้นสั่ง save ที่ @"d:\...............xls";
            //string filename = @"d:\BookPolarimeter10.xls";
            try
            {
                using (var fileData = new FileStream(fullFilePath, FileMode.Create))
                {
                    workbook.Write(fileData);

                    if (redlg == DialogResult.OK)
                    {
                        MessageBox.Show("Save success!", "Polarimeter 2020", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.StackTrace}", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
