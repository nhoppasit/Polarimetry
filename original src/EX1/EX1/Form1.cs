using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX1
{
    public partial class Form1 : Form
    {
        private DialogResult ret;

        public Form1()
        {
            InitializeComponent();
            IsvData.Clear();
            IsvData.View = View.Details;
            IsvData.Columns.Add("ID",30);
            IsvData.Columns.Add("First Name", 70);
            IsvData.Columns.Add("Last Name", 70);
            IsvData.Columns.Add("Age", 40);
            IsvData.Columns.Add("Gender", 70);
            IsvData.Columns.Add("Address", 70);
            IsvData.GridLines = true;
            IsvData.FullRowSelect = true;               

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dlgAdd dlg = new dlgAdd();
            DialogResult result = dlg.ShowDialog();
            if (ret == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem(dlg.ID);
                lvi.SubItems.Add(dlg.FirstName);
                lvi.SubItems.Add(dlg.LastName);
                lvi.SubItems.Add(dlg.Age.ToString());
                lvi.SubItems.Add(dlg.Gender.ToString());
                lvi.SubItems.Add(dlg.Address);


                lvi.SubItems[1].Text = dlg.FirstName;
                lvi.SubItems[2].Text = dlg.LastName;
                lvi.SubItems[4].Text = dlg.Age.ToString();
                lvi.SubItems[5].Text = dlg.Gender.ToString();
                if (dlg.Gender) lvi.SubItems[5].Text = "Male";
                else lvi.SubItems[5].Text = "Female";
                IsvData.Items.Add(lvi);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }
    }
}
