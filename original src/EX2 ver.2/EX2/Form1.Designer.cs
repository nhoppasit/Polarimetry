namespace EX2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.LsvData = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInitPhase = new System.Windows.Forms.TextBox();
            this.txtDt = new System.Windows.Forms.TextBox();
            this.txtOmega = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtA = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCal = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnCalulator2 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LsvData
            // 
            this.LsvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LsvData.Location = new System.Drawing.Point(310, 27);
            this.LsvData.Name = "LsvData";
            this.LsvData.Size = new System.Drawing.Size(453, 465);
            this.LsvData.TabIndex = 0;
            this.LsvData.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Function";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(98, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(175, 24);
            this.comboBox1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInitPhase);
            this.groupBox1.Controls.Add(this.txtDt);
            this.groupBox1.Controls.Add(this.txtOmega);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtB);
            this.groupBox1.Controls.Add(this.txtA);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 250);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Phase";
            // 
            // txtInitPhase
            // 
            this.txtInitPhase.Location = new System.Drawing.Point(83, 177);
            this.txtInitPhase.Name = "txtInitPhase";
            this.txtInitPhase.Size = new System.Drawing.Size(150, 22);
            this.txtInitPhase.TabIndex = 12;
            this.txtInitPhase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox5_KeyPress);
            // 
            // txtDt
            // 
            this.txtDt.Location = new System.Drawing.Point(83, 138);
            this.txtDt.Name = "txtDt";
            this.txtDt.Size = new System.Drawing.Size(150, 22);
            this.txtDt.TabIndex = 11;
            this.txtDt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_KeyPress);
            // 
            // txtOmega
            // 
            this.txtOmega.Location = new System.Drawing.Point(83, 104);
            this.txtOmega.Name = "txtOmega";
            this.txtOmega.Size = new System.Drawing.Size(150, 22);
            this.txtOmega.TabIndex = 10;
            this.txtOmega.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "ΔT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "ω";
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(83, 67);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(150, 22);
            this.txtB.TabIndex = 7;
            this.txtB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(83, 34);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(150, 22);
            this.txtA.TabIndex = 6;
            this.txtA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "B";
            // 
            // btnCal
            // 
            this.btnCal.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCal.Location = new System.Drawing.Point(15, 361);
            this.btnCal.Name = "btnCal";
            this.btnCal.Size = new System.Drawing.Size(129, 66);
            this.btnCal.TabIndex = 4;
            this.btnCal.Text = "Calulator";
            this.btnCal.UseVisualStyleBackColor = false;
            this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Brown;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(150, 361);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(129, 66);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(98, 302);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(150, 22);
            this.txtNumber.TabIndex = 13;
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox6_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnCalulator2
            // 
            this.btnCalulator2.BackColor = System.Drawing.Color.LawnGreen;
            this.btnCalulator2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalulator2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCalulator2.Location = new System.Drawing.Point(15, 433);
            this.btnCalulator2.Name = "btnCalulator2";
            this.btnCalulator2.Size = new System.Drawing.Size(129, 66);
            this.btnCalulator2.TabIndex = 14;
            this.btnCalulator2.Text = "Calulator2";
            this.btnCalulator2.UseVisualStyleBackColor = false;
            this.btnCalulator2.Click += new System.EventHandler(this.btnCalulator2_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Gold;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnStop.Location = new System.Drawing.Point(150, 433);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(129, 66);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 527);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnCalulator2);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LsvData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LsvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInitPhase;
        private System.Windows.Forms.TextBox txtDt;
        private System.Windows.Forms.TextBox txtOmega;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCalulator2;
        private System.Windows.Forms.Button btnStop;
    }
}

