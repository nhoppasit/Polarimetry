using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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

            AssignHeaderForSave(ref sheet1, "Addr1", "Addr2");


            ////จากนั้นสั่ง save ที่ @"d:\...............xls";
            //string filename = @"d:\BookPolarimeter10.xls";
            using (var fileData = new FileStream(fullFilePath, FileMode.Create))
            {
                workbook.Write(fileData);

                if (redlg == DialogResult.OK)
                {
                    MessageBox.Show("Save success!");
                }

            }
        }

        void AssignHeaderForSave(ref ISheet sheet, string gpibAddrDMM34401A, string gpibAddrMMC2)
        {
            //sheet 1
            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.CellStyle.ShrinkToFit = true;
            cell.SetCellValue("Date of creation");

            row.CreateCell(1).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));  //วันที่

            row = sheet.CreateRow(1);
            row.CreateCell(0).SetCellValue("Time of creation" + DateTime.Now.ToString("HH:mm"));
            row.CreateCell(1).SetCellValue(DateTime.Now.ToString("HH:mm"));

            row = sheet.CreateRow(2);
            row.CreateCell(0).SetCellValue("DMM-34401A GPIB Address");
            row.CreateCell(1).SetCellValue(gpibAddrDMM34401A);

            row = sheet.CreateRow(3);
            row.CreateCell(0).SetCellValue("MMC-2 GPIB Address");
            row.CreateCell(1).SetCellValue(gpibAddrMMC2);

        }

    }
}
