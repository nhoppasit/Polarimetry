using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exam_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            lsvData.View = View.Details;
            lsvData.Columns.Clear();
            lsvData.Columns.Add("ID", 100);
            lsvData.Columns.Add("FirstName", 100);
            lsvData.Columns.Add("LastName", 100);
            lsvData.Columns.Add("NickName", 100);
            lsvData.Columns.Add("Age", 40);
            lsvData.Columns.Add("Gender", 70);
            lsvData.Columns.Add("Province", 70);
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dlgAdd dlg = new dlgAdd();
            DialogResult ret = dlg.ShowDialog();
            if (ret == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = dlg.ID;
                lvi.SubItems.Add(dlg.FirstName);
                lvi.SubItems.Add(dlg.LastName);
                lvi.SubItems.Add(dlg.NickName);
                lvi.SubItems.Add(dlg.Age.ToString());
                lvi.SubItems.Add(dlg.Gender.ToString());
                lvi.SubItems.Add(dlg.Province);
                //lvi.SubItems.Add(dlg.Other);


                //lvi.SubItems[1].Text = dlg.FirstName;
                //lvi.SubItems[2].Text = dlg.LastName;
                //lvi.SubItems[3].Text = dlg.NickName;
                //lvi.SubItems[4].Text = dlg.Age.ToString();
                //lvi.SubItems[5].Text = dlg.Gender.ToString();
                if (dlg.Gender) lvi.SubItems[5].Text = "Male";
                else lvi.SubItems[5].Text = "Female";
                lsvData.Items.Add(lvi);
            }
        }

        private void lsvData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lsvData.Items.Remove(lsvData.SelectedItems[0]);

            }
            catch { }
            
        }
    }
}
