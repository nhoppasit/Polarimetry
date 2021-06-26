using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;

namespace Polarimeter_2020_Unit_Test {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
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
            var sheet1 = workbook.CreateSheet("Sheet1");
            var sheet2 = workbook.CreateSheet("Sheet2");



            var rowIndex = 0; //สร้าง row แล้วกำหนดข้อมูลให้แต่ละคอลัมน์
            var rowA = sheet1.CreateRow(rowIndex);
            var rowB = sheet2.CreateRow(rowIndex);

            //sheet 1
            rowA.CreateCell(0).SetCellValue("Date of creation");
            rowA.CreateCell(1).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));  //วันที่
            rowA.CreateCell(2).SetCellValue("Time - " + DateTime.Now.ToString("HH:mm"));
            rowA.CreateCell(3).SetCellValue("");
            rowA.CreateCell(4).SetCellValue("");

            // sheet 2
            rowB.CreateCell(0).SetCellValue("Sample Name:");
            // rowB.CreateCell(1).SetCellValue(textSampleName.Text);//(this.textSampleName.Name);
            rowB.CreateCell(2).SetCellValue("");
            rowIndex++;

            ////สร้าง DataTable
            DataTable dt = new DataTable();
            // sheet 1
            dt.Columns.Add("Date of creation");  //colum จะเป็นหัว แนวตั้ง
            dt.Columns.Add("Cl1");               //Cl ย่อมาจาก Colum
            dt.Columns.Add("Cl2");
            dt.Columns.Add("Cl3");
            dt.Columns.Add("Cl4");
            // sheet 2
            dt.Columns.Add("1");
            dt.Columns.Add("2");
            dt.Columns.Add("3");
            dt.Columns.Add("4");

            DataRow row2 = dt.NewRow();       //row ภายในนี้จะเป็นแนวนอน บรรทัดแนวนอน
            row2["Date of creation"] = "Device connect (GPIB/USB)";
            row2["Cl1"] = "DMM-34401A";
            row2["Cl2"] = "TEST-1234";//txtDMMAddress.Text; // Properties.Settings.Default.DMMAddress;
            row2["1"] = "";
            row2["2"] = "";
            row2["3"] = "";
            dt.Rows.Add(row2);

            DataRow row3 = dt.NewRow();
            row3["Cl1"] = "MMC-2";
            row3["Cl2"] = "TEST-314.1";//txtMMCAddress.Text; // Properties.Settings.Default.MMC2Address;
            row3["1"] = "Step No.";
            row3["2"] = "Referrence";
            row3["3"] = "Sample 1";
            row3["4"] = "Sample 2";
            dt.Rows.Add(row3);

            DataRow row4 = dt.NewRow();
            row4["1"] = "1.";
            row4["2"] = ".";
            row4["3"] = ".";
            dt.Rows.Add(row4);

            DataRow row5 = dt.NewRow();
            row5["Date of creation"] = "Sample Name:";
            row5["Cl1"] = "Untitle1"; //txtSampleName.Text; //(textSampleName.Text);
            row5["1"] = "2.";
            row5["2"] = ".";
            row5["3"] = ".";
            dt.Rows.Add(row5);

            DataRow row6 = dt.NewRow();
            row6["Date of creation"] = "Number of Sample:";
            row6["Cl1"] = ".";
            row6["1"] = "3.";
            row6["2"] = ".";
            row6["3"] = ".";
            dt.Rows.Add(row6);

            DataRow row7 = dt.NewRow();
            row7["Date of creation"] = "Number of Rotation:";
            row7["Cl1"] = ".";
            dt.Rows.Add(row7);

            DataRow row8 = dt.NewRow();
            row8["Date of creation"] = "Averrage Number:";
            row8["Cl1"] = ".";
            dt.Rows.Add(row8);

            DataRow row9 = dt.NewRow();
            row9["Date of creation"] = "Resolution:";
            row9["Cl1"] = ".";
            dt.Rows.Add(row9);

            DataRow row10 = dt.NewRow();
            row10["Date of creation"] = "Range:";
            row10["Cl1"] = ".";
            dt.Rows.Add(row10);

            DataRow row11 = dt.NewRow();
            //ไว้เพิ่มข้อมูลลงช่องนี้
            dt.Rows.Add(row11);

            DataRow row12 = dt.NewRow();
            row12["Date of creation"] = ".";
            row12["Cl1"] = "Sample";
            row12["Cl2"] = "Null Point";
            row12["Cl3"] = "Angle of Rotation";
            dt.Rows.Add(row12);

            DataRow row13 = dt.NewRow();
            row13["Cl1"] = ".";
            dt.Rows.Add(row13);


            foreach (DataRow dr in dt.Rows) ////นำข้อมูลจาก DataTable มาใส่ลงฟิลด์
            {
                rowA = sheet1.CreateRow(rowIndex);
                rowB = sheet2.CreateRow(rowIndex);
                // sheet 1
                rowA.CreateCell(0).SetCellValue(dr["Date of creation"].ToString());
                rowA.CreateCell(1).SetCellValue(dr["Cl1"].ToString());
                rowA.CreateCell(2).SetCellValue(dr["Cl2"].ToString());
                rowA.CreateCell(3).SetCellValue(dr["Cl3"].ToString());
                rowA.CreateCell(4).SetCellValue(dr["Cl4"].ToString());

                // sheet 2
                rowB.CreateCell(0).SetCellValue(dr["1"].ToString());
                rowB.CreateCell(1).SetCellValue(dr["2"].ToString());
                rowB.CreateCell(2).SetCellValue(dr["3"].ToString());
                // row0.CreateCell(3).SetCellValue(dr["4"].ToString());

                rowIndex++;

            }

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
    }
}
