namespace Polarimeter2019
{
    partial class frmColorTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem41 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "1", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "", System.Drawing.SystemColors.WindowText, System.Drawing.Color.Blue, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "")}, -1);
            System.Windows.Forms.ListViewItem listViewItem42 = new System.Windows.Forms.ListViewItem("2");
            System.Windows.Forms.ListViewItem listViewItem43 = new System.Windows.Forms.ListViewItem("3");
            System.Windows.Forms.ListViewItem listViewItem44 = new System.Windows.Forms.ListViewItem("4");
            System.Windows.Forms.ListViewItem listViewItem45 = new System.Windows.Forms.ListViewItem("5");
            System.Windows.Forms.ListViewItem listViewItem46 = new System.Windows.Forms.ListViewItem("6");
            System.Windows.Forms.ListViewItem listViewItem47 = new System.Windows.Forms.ListViewItem("7");
            System.Windows.Forms.ListViewItem listViewItem48 = new System.Windows.Forms.ListViewItem("8");
            System.Windows.Forms.ListViewItem listViewItem49 = new System.Windows.Forms.ListViewItem("9");
            System.Windows.Forms.ListViewItem listViewItem50 = new System.Windows.Forms.ListViewItem("10");
            System.Windows.Forms.ListViewItem listViewItem51 = new System.Windows.Forms.ListViewItem("11");
            System.Windows.Forms.ListViewItem listViewItem52 = new System.Windows.Forms.ListViewItem("12");
            System.Windows.Forms.ListViewItem listViewItem53 = new System.Windows.Forms.ListViewItem("13");
            System.Windows.Forms.ListViewItem listViewItem54 = new System.Windows.Forms.ListViewItem("14");
            System.Windows.Forms.ListViewItem listViewItem55 = new System.Windows.Forms.ListViewItem("15");
            System.Windows.Forms.ListViewItem listViewItem56 = new System.Windows.Forms.ListViewItem("16");
            System.Windows.Forms.ListViewItem listViewItem57 = new System.Windows.Forms.ListViewItem("17");
            System.Windows.Forms.ListViewItem listViewItem58 = new System.Windows.Forms.ListViewItem("18");
            System.Windows.Forms.ListViewItem listViewItem59 = new System.Windows.Forms.ListViewItem("19");
            System.Windows.Forms.ListViewItem listViewItem60 = new System.Windows.Forms.ListViewItem("20");
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvColorTable = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 300);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 37);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvColorTable
            // 
            this.lvColorTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvColorTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3});
            this.lvColorTable.FullRowSelect = true;
            this.lvColorTable.GridLines = true;
            listViewItem41.UseItemStyleForSubItems = false;
            this.lvColorTable.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem41,
            listViewItem42,
            listViewItem43,
            listViewItem44,
            listViewItem45,
            listViewItem46,
            listViewItem47,
            listViewItem48,
            listViewItem49,
            listViewItem50,
            listViewItem51,
            listViewItem52,
            listViewItem53,
            listViewItem54,
            listViewItem55,
            listViewItem56,
            listViewItem57,
            listViewItem58,
            listViewItem59,
            listViewItem60});
            this.lvColorTable.Location = new System.Drawing.Point(13, 13);
            this.lvColorTable.Margin = new System.Windows.Forms.Padding(4);
            this.lvColorTable.Name = "lvColorTable";
            this.lvColorTable.Size = new System.Drawing.Size(339, 279);
            this.lvColorTable.TabIndex = 0;
            this.lvColorTable.UseCompatibleStateImageBehavior = false;
            this.lvColorTable.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "No.";
            this.ColumnHeader1.Width = 75;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Color";
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Color Value";
            this.ColumnHeader3.Width = 108;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(13, 300);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(115, 37);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmColorTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 348);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvColorTable);
            this.Name = "frmColorTable";
            this.Text = "frmColorTable";
            this.Load += new System.EventHandler(this.frmColorTable_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.ListView lvColorTable;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.Button btnOK;
    }
}