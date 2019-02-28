﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polarimeter2019
{
    public partial class frmColorTable : Form
    {
        public frmColorTable()
        {
            InitializeComponent();
        }

        private void frmColorTable_Load(object sender, EventArgs e)
        {
            LoadColorTable();
        }

        private void LoadColorTable()
        {
            lvColorTable.Items.Clear();
            ListViewItem LVI = new ListViewItem();
            LVI.UseItemStyleForSubItems = false;
            LVI.Text = "Reference";
            LVI.SubItems.Add("");
            LVI.SubItems.Add(frmMain.ReferenceColor.ToString);
            LVI.SubItems[1].BackColor = System.Drawing.Color.Red;
            lvColorTable.Items.Add(LVI);

            for (int i = 0; i <= 19; i++)
            {
                LVI = new ListViewItem();
                LVI.UseItemStyleForSubItems = false;
                LVI.Text = "Sample " + (i + 1).ToString();
                LVI.SubItems.Add("");
                LVI.SubItems.Add(frmMain.ColorTable(i).ToString);
                LVI.SubItems[1].BackColor = frmMain.ColorTable(i);
                lvColorTable.Items.Add(LVI);
            }
        }

        private void lvColorTable_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                int i = lvColorTable.SelectedIndices[0];
                ColorDialog dlg = new ColorDialog();
                dlg.Color = lvColorTable.SelectedItems[0].SubItems[1].BackColor;
                dlg.ShowDialog();
                if (i == 0)
                {
                    frmMain.ReferenceColor = dlg.Color;
                }
                else
                {
                    frmMain.ColorTable[i - 1] = dlg.Color;
                }

                lvColorTable.SelectedItems[0].SubItems[1].BackColor = dlg.Color;
                lvColorTable.SelectedItems[0].SubItems[2].Text = dlg.Color.ToString();
            switch (i)
            {
                case 0:
                    {
                        Properties.Settings.Default.ReferenceColor = dlg.Color;
                        break;
                    }

                case 1:
                    {
                        Properties.Settings.Default.color1 = dlg.Color;
                        break;
                    }

                case 2:
                    {
                        Properties.Settings.Default.color2 = dlg.Color;
                        break;
                    }

                case 3:
                    {
                        Properties.Settings.Default.color3 = dlg.Color;
                        break;
                    }

                case 4:
                    {
                        Properties.Settings.Default.color4 = dlg.Color;
                        break;
                    }

                case 5:
                    {
                        Properties.Settings.Default.color5 = dlg.Color;
                        break;
                    }

                case 6:
                    {
                        Properties.Settings.Default.color6 = dlg.Color;
                        break;
                    }

                case 7:
                    {
                        Properties.Settings.Default.color7 = dlg.Color;
                        break;
                    }

                case 8:
                    {
                        Properties.Settings.Default.color8 = dlg.Color;
                        break;
                    }

                case 9:
                    {
                        Properties.Settings.Default.color9 = dlg.Color;
                        break;
                    }

                case 10:
                    {
                        Properties.Settings.Default.color10 = dlg.Color;
                        break;
                    }

                case 11:
                    {
                        Properties.Settings.Default.color11 = dlg.Color;
                        break;
                    }

                case 12:
                    {
                        Properties.Settings.Default.color12 = dlg.Color;
                        break;
                    }

                case 13:
                    {
                        Properties.Settings.Default.color13 = dlg.Color;
                        break;
                    }

                case 14:
                    {
                        Properties.Settings.Default.color14 = dlg.Color;
                        break;
                    }

                case 15:
                    {
                        Properties.Settings.Default.color15 = dlg.Color;
                        break;
                    }

                case 16:
                    {
                        Properties.Settings.Default.color16 = dlg.Color;
                        break;
                    }

                case 17:
                    {
                        Properties.Settings.Default.color17 = dlg.Color;
                        break;
                    }

                case 18:
                    {
                        Properties.Settings.Default.color18 = dlg.Color;
                        break;
                    }

                case 19:
                    {
                        Properties.Settings.Default.color19 = dlg.Color;
                        break;
                    }

                case 20:
                    {
                        Properties.Settings.Default.color20 = dlg.Color;
                        break;
                    }
            }
            Properties.Settings.Default.Save();
            frmMain.ApplyColorTableToSamples();
            }
            catch (Exception ex)
            {

            }
        }

    }
}