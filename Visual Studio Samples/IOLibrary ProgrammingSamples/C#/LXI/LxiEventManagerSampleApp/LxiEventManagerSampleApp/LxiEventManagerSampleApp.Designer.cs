namespace LxiEventManagerSampleApp
{
    partial class LxiEventManagerSampleApp
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
            this.EventTextBox = new System.Windows.Forms.TextBox();
            this.SendEventButton = new System.Windows.Forms.Button();
            this.SimpleRadioButton = new System.Windows.Forms.RadioButton();
            this.CustomRadioButton = new System.Windows.Forms.RadioButton();
            this.EventIdComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // EventTextBox
            // 
            this.EventTextBox.Location = new System.Drawing.Point(12, 129);
            this.EventTextBox.Multiline = true;
            this.EventTextBox.Name = "EventTextBox";
            this.EventTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.EventTextBox.Size = new System.Drawing.Size(524, 235);
            this.EventTextBox.TabIndex = 0;
            // 
            // SendEventButton
            // 
            this.SendEventButton.Location = new System.Drawing.Point(49, 86);
            this.SendEventButton.Name = "SendEventButton";
            this.SendEventButton.Size = new System.Drawing.Size(75, 23);
            this.SendEventButton.TabIndex = 2;
            this.SendEventButton.Text = "Send Event";
            this.SendEventButton.UseVisualStyleBackColor = true;
            this.SendEventButton.Click += new System.EventHandler(this.SendEventButton_Click);
            // 
            // SimpleRadioButton
            // 
            this.SimpleRadioButton.AutoSize = true;
            this.SimpleRadioButton.Location = new System.Drawing.Point(49, 29);
            this.SimpleRadioButton.Name = "SimpleRadioButton";
            this.SimpleRadioButton.Size = new System.Drawing.Size(87, 17);
            this.SimpleRadioButton.TabIndex = 3;
            this.SimpleRadioButton.TabStop = true;
            this.SimpleRadioButton.Text = "Simple Event";
            this.SimpleRadioButton.UseVisualStyleBackColor = true;
            this.SimpleRadioButton.CheckedChanged += new System.EventHandler(this.SimpleRadioButton_CheckedChanged);
            // 
            // CustomRadioButton
            // 
            this.CustomRadioButton.AutoSize = true;
            this.CustomRadioButton.Location = new System.Drawing.Point(49, 53);
            this.CustomRadioButton.Name = "CustomRadioButton";
            this.CustomRadioButton.Size = new System.Drawing.Size(91, 17);
            this.CustomRadioButton.TabIndex = 4;
            this.CustomRadioButton.TabStop = true;
            this.CustomRadioButton.Text = "Custom Event";
            this.CustomRadioButton.UseVisualStyleBackColor = true;
            // 
            // EventIdComboBox
            // 
            this.EventIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EventIdComboBox.Enabled = false;
            this.EventIdComboBox.FormattingEnabled = true;
            this.EventIdComboBox.Location = new System.Drawing.Point(168, 29);
            this.EventIdComboBox.Name = "EventIdComboBox";
            this.EventIdComboBox.Size = new System.Drawing.Size(121, 21);
            this.EventIdComboBox.TabIndex = 5;
            // 
            // LxiEventManagerSampleApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 388);
            this.Controls.Add(this.EventIdComboBox);
            this.Controls.Add(this.CustomRadioButton);
            this.Controls.Add(this.SimpleRadioButton);
            this.Controls.Add(this.SendEventButton);
            this.Controls.Add(this.EventTextBox);
            this.Name = "LxiEventManagerSampleApp";
            this.Text = "LxiEventManager Sample Appplication";
            this.Load += new System.EventHandler(this.LxiEventManagerSampleApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EventTextBox;
        private System.Windows.Forms.Button SendEventButton;
        private System.Windows.Forms.RadioButton SimpleRadioButton;
        private System.Windows.Forms.RadioButton CustomRadioButton;
        private System.Windows.Forms.ComboBox EventIdComboBox;
    }
}

