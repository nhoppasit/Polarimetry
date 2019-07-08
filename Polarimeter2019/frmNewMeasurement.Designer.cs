namespace Polarimeter2019
{
    partial class frmNewMeasurement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewMeasurement));
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtSampleNamee = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.numAverageNumber = new System.Windows.Forms.NumericUpDown();
            this.numRepeatation = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numAverageNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeatation)).BeginInit();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(26, 58);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(88, 13);
            this.Label2.TabIndex = 25;
            this.Label2.Text = "Average number:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 84);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(102, 13);
            this.Label1.TabIndex = 24;
            this.Label1.Text = "Number of Samples:";
            // 
            // txtSampleNamee
            // 
            this.txtSampleNamee.Location = new System.Drawing.Point(116, 30);
            this.txtSampleNamee.Name = "txtSampleNamee";
            this.txtSampleNamee.Size = new System.Drawing.Size(241, 20);
            this.txtSampleNamee.TabIndex = 18;
            this.txtSampleNamee.Text = "Sample Name?";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(34, 32);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(76, 13);
            this.Label7.TabIndex = 23;
            this.Label7.Text = "Sample Name:";
            // 
            // numAverageNumber
            // 
            this.numAverageNumber.Location = new System.Drawing.Point(116, 56);
            this.numAverageNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numAverageNumber.Name = "numAverageNumber";
            this.numAverageNumber.Size = new System.Drawing.Size(57, 20);
            this.numAverageNumber.TabIndex = 26;
            this.numAverageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numAverageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numRepeatation
            // 
            this.numRepeatation.Location = new System.Drawing.Point(116, 82);
            this.numRepeatation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numRepeatation.Name = "numRepeatation";
            this.numRepeatation.Size = new System.Drawing.Size(57, 20);
            this.numRepeatation.TabIndex = 27;
            this.numRepeatation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRepeatation.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(207, 101);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 37);
            this.btnOK.TabIndex = 28;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(284, 101);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 37);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmNewMeasurement
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 147);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numRepeatation);
            this.Controls.Add(this.numAverageNumber);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtSampleNamee);
            this.Controls.Add(this.Label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmNewMeasurement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNewMeasurement";
            ((System.ComponentModel.ISupportInitialize)(this.numAverageNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeatation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtSampleNamee;
        internal System.Windows.Forms.Label Label7;
        private System.Windows.Forms.NumericUpDown numAverageNumber;
        private System.Windows.Forms.NumericUpDown numRepeatation;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}