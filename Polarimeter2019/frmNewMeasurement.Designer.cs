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
            this.Label2 = new System.Windows.Forms.Label();
            this.numAverageNumber = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.numRepeatation = new System.Windows.Forms.NumericUpDown();
            this.txtSampleName = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAverageNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeatation)).BeginInit();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(37, 49);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(117, 17);
            this.Label2.TabIndex = 25;
            this.Label2.Text = "Average number:";
            // 
            // numAverageNumber
            // 
            this.numAverageNumber.Location = new System.Drawing.Point(156, 47);
            this.numAverageNumber.Margin = new System.Windows.Forms.Padding(4);
            this.numAverageNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAverageNumber.Name = "numAverageNumber";
            this.numAverageNumber.Size = new System.Drawing.Size(81, 22);
            this.numAverageNumber.TabIndex = 19;
            this.numAverageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numAverageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(357, 128);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(229, 128);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 38);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(16, 81);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 17);
            this.Label1.TabIndex = 24;
            this.Label1.Text = "Number of Samples:";
            // 
            // numRepeatation
            // 
            this.numRepeatation.Location = new System.Drawing.Point(156, 79);
            this.numRepeatation.Margin = new System.Windows.Forms.Padding(4);
            this.numRepeatation.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRepeatation.Name = "numRepeatation";
            this.numRepeatation.Size = new System.Drawing.Size(81, 22);
            this.numRepeatation.TabIndex = 20;
            this.numRepeatation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRepeatation.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // txtSampleName
            // 
            this.txtSampleName.Location = new System.Drawing.Point(156, 15);
            this.txtSampleName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.Size = new System.Drawing.Size(320, 22);
            this.txtSampleName.TabIndex = 18;
            this.txtSampleName.Text = "Sample Name?";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(47, 18);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(100, 17);
            this.Label7.TabIndex = 23;
            this.Label7.Text = "Sample Name:";
            // 
            // frmNewMeasurement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 181);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.numAverageNumber);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.numRepeatation);
            this.Controls.Add(this.txtSampleName);
            this.Controls.Add(this.Label7);
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
        internal System.Windows.Forms.NumericUpDown numAverageNumber;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.NumericUpDown numRepeatation;
        internal System.Windows.Forms.TextBox txtSampleName;
        internal System.Windows.Forms.Label Label7;
    }
}