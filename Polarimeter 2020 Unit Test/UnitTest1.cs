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

        void AssignTestHeaderSheetForSave(ref HSSFWorkbook workbook, ref ISheet sheet, TestHeaderModel testHeader)
        {
            // ------------------------------------------------------------------------------------------
            // Date of test
            // ------------------------------------------------------------------------------------------
            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue("Date of test");
            cell.CellStyle = HeaderCellStyles.BlueWhiteBoldCenter(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));  //วันที่

            // ------------------------------------------------------------------------------------------
            // Time of test
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(1);
            cell = row.CreateCell(0);
            cell.SetCellValue("Time of test");
            cell.CellStyle = HeaderCellStyles.BrightGreen(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(DateTime.Now.ToString("HH:mm"));

            // ------------------------------------------------------------------------------------------
            // DMM 34401A GPIB Address
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(2);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.GpibAddressOfDmm34401AHeader);
            cell.CellStyle = HeaderCellStyles.LightGreen(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.GpibAddressOfDmm34401A);


            // ------------------------------------------------------------------------------------------
            // MMC-2 GPIB Address
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(3);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.GpibAddressOfMmc2Header);
            cell.CellStyle = HeaderCellStyles.LightGreen(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.GpibAddressOfMmc2);

            // ------------------------------------------------------------------------------------------
            // Sample Name
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(5);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.SampleNameHeader);
            cell.CellStyle = HeaderCellStyles.Custom(workbook, IndexedColors.Coral.Index);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.SampleName);

            // ------------------------------------------------------------------------------------------
            // Number of samples
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(6);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.NumberOfSamplesHeader);
            cell.CellStyle = HeaderCellStyles.Yellow(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.NumberOfSamples);

            // ------------------------------------------------------------------------------------------
            // Number of rotations
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(7);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.NumberOfRotationsHeader);
            cell.CellStyle = HeaderCellStyles.Yellow(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.NumberOfRotations);

            // ------------------------------------------------------------------------------------------
            // Average number
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(8);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.AverageNumberHeader);
            cell.CellStyle = HeaderCellStyles.Yellow(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.AverageNumber);

            // ------------------------------------------------------------------------------------------
            // Resolution
            // ------------------------------------------------------------------------------------------
            row = sheet.CreateRow(9);
            cell = row.CreateCell(0);
            cell.SetCellValue(testHeader.ResolutionHeader);
            cell.CellStyle = HeaderCellStyles.Yellow(workbook);

            cell = row.CreateCell(1);
            cell.SetCellValue(testHeader.Resolution);


            // ------------------------------------------------------------------------------------------
            // Finalize rows and columns style
            // ------------------------------------------------------------------------------------------
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

            AssignTestHeaderSheetForSave(ref workbook, ref sheet1,
                new TestHeaderModel() {
                    GpibAddressOfDmm34401A = "Addr1",
                    GpibAddressOfMmc2 = "Addr2",
                    SampleName = "SM1",
                    NumberOfSamples = 2,
                    NumberOfRotations = 3,
                    AverageNumber = 23,
                    Resolution = 0.2,
                });

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
