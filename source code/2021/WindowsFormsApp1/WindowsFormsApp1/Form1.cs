using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void mnuDevicesAddress_Click(object sender, EventArgs e) {
            dlgDMMAddress dlg = new dlgDMMAddress();
            dlg.ShowDialog();            
        }

        private void mnuConnect_Click(object sender, EventArgs e) {
            connect();
        }

        void connect() { }

        private void mnuSave_Click(object sender, EventArgs e) {
            save1();
        }
        void save1() {
            HSSFWorkbook workbook = new HSSFWorkbook();
            var sheet1 = workbook.CreateSheet("Sheet1");
            var sheet2 = workbook.CreateSheet("Sheet2");


            var rowIndex = 0;
            var row = sheet1.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Username");
            row.CreateCell(1).SetCellValue("Email");
            rowIndex++;
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Email");
            DataRow row1 = dt.NewRow();
            row1["Name"] = "Jack";
            row1["Email"] = "mr.phaisarn@gmail.com";
            dt.Rows.Add(row1);
            DataRow row2 = dt.NewRow();
            row2["Name"] = "Example";
            row2["Email"] = "example@gmail.com";
            dt.Rows.Add(row2);
            foreach(DataRow dr in dt.Rows) {
                row = sheet1.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(dr["Name"].ToString());
                row.CreateCell(1).SetCellValue(dr["Email"].ToString());
                rowIndex++;
            }


            string filename = @"d:\Book.xls";
            using(var fileData = new FileStream(filename, FileMode.Create)) {
                workbook.Write(fileData);
            }
        }
    }
}
