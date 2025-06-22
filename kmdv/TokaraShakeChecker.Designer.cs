namespace kmdv
{
    partial class TokaraShakeChecker
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
            L_text = new Label();
            L_value = new Label();
            L_color0 = new Label();
            L_color1 = new Label();
            L_color2 = new Label();
            L_color3 = new Label();
            SuspendLayout();
            // 
            // L_text
            // 
            L_text.AutoSize = true;
            L_text.Font = new Font("Yu Gothic UI", 16F);
            L_text.ForeColor = SystemColors.Control;
            L_text.Location = new Point(0, 0);
            L_text.Name = "L_text";
            L_text.Size = new Size(179, 210);
            L_text.TabIndex = 0;
            L_text.Text = "<観測点(北から)>\r\n[屋久島]\r\n屋久　：\r\n[奄美大島]\r\n笠利　：\r\n大和　：\r\n瀬戸内：";
            // 
            // L_value
            // 
            L_value.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            L_value.Location = new Point(105, 60);
            L_value.Name = "L_value";
            L_value.Size = new Size(50, 150);
            L_value.TabIndex = 1;
            L_value.Text = "---\r\n\r\n---\r\n---\r\n---";
            L_value.TextAlign = ContentAlignment.TopRight;
            // 
            // L_color0
            // 
            L_color0.BackColor = Color.FromArgb(30, 60, 90);
            L_color0.ForeColor = SystemColors.ControlLightLight;
            L_color0.Location = new Point(105, 85);
            L_color0.Name = "L_color0";
            L_color0.Size = new Size(50, 5);
            L_color0.TabIndex = 2;
            // 
            // L_color1
            // 
            L_color1.BackColor = Color.FromArgb(30, 60, 90);
            L_color1.ForeColor = SystemColors.ControlLightLight;
            L_color1.Location = new Point(105, 145);
            L_color1.Name = "L_color1";
            L_color1.Size = new Size(50, 5);
            L_color1.TabIndex = 3;
            // 
            // L_color2
            // 
            L_color2.BackColor = Color.FromArgb(30, 60, 90);
            L_color2.ForeColor = SystemColors.ControlLightLight;
            L_color2.Location = new Point(105, 175);
            L_color2.Name = "L_color2";
            L_color2.Size = new Size(50, 5);
            L_color2.TabIndex = 4;
            // 
            // L_color3
            // 
            L_color3.BackColor = Color.FromArgb(30, 60, 90);
            L_color3.ForeColor = SystemColors.ControlLightLight;
            L_color3.Location = new Point(106, 205);
            L_color3.Name = "L_color3";
            L_color3.Size = new Size(50, 5);
            L_color3.TabIndex = 5;
            // 
            // TokaraShakeChecker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 60, 90);
            ClientSize = new Size(180, 210);
            Controls.Add(L_color3);
            Controls.Add(L_color2);
            Controls.Add(L_color1);
            Controls.Add(L_color0);
            Controls.Add(L_value);
            Controls.Add(L_text);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "TokaraShakeChecker";
            Text = "TokaraShakeChecker";
            FormClosed += TokaraShakeChecker_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_text;
        private Label L_value;
        private Label L_color0;
        private Label L_color1;
        private Label L_color2;
        private Label L_color3;
    }
}