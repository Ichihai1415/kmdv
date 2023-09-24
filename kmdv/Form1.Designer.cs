namespace kmdv
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Gettimer = new System.Windows.Forms.Timer(components);
            MainImage = new PictureBox();
            Text1 = new Label();
            groupBox1 = new GroupBox();
            VersionView = new Label();
            Text0 = new Label();
            KCSview = new Label();
            Text2 = new Label();
            RAMview = new Label();
            LogView = new TextBox();
            MSView = new Label();
            MaxsView = new TextBox();
            ((System.ComponentModel.ISupportInitialize)MainImage).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Gettimer
            // 
            Gettimer.Tick += Gettimer_Tick;
            // 
            // MainImage
            // 
            MainImage.BackgroundImage = Properties.Resources.backmap;
            MainImage.BackgroundImageLayout = ImageLayout.Zoom;
            MainImage.Location = new Point(0, 0);
            MainImage.Name = "MainImage";
            MainImage.Size = new Size(528, 400);
            MainImage.TabIndex = 3;
            MainImage.TabStop = false;
            // 
            // Text1
            // 
            Text1.AutoSize = true;
            Text1.Location = new Point(6, 32);
            Text1.Name = "Text1";
            Text1.Size = new Size(103, 90);
            Text1.TabIndex = 4;
            Text1.Text = "rs-s max:\r\nac-s max:\r\nac-s sum:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(VersionView);
            groupBox1.Controls.Add(Text0);
            groupBox1.Controls.Add(Text1);
            groupBox1.Controls.Add(KCSview);
            groupBox1.Location = new Point(534, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(260, 131);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "KCS";
            // 
            // VersionView
            // 
            VersionView.AutoSize = true;
            VersionView.Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            VersionView.Location = new Point(200, 0);
            VersionView.Name = "VersionView";
            VersionView.Size = new Size(62, 13);
            VersionView.TabIndex = 9;
            VersionView.Text = "kmdv v0.0.0";
            // 
            // Text0
            // 
            Text0.AutoSize = true;
            Text0.Location = new Point(9, 2);
            Text0.Name = "Text0";
            Text0.Size = new Size(82, 30);
            Text0.TabIndex = 9;
            Text0.Text = "<KCS>";
            // 
            // KCSview
            // 
            KCSview.Location = new Point(6, 32);
            KCSview.Name = "KCSview";
            KCSview.Size = new Size(242, 90);
            KCSview.TabIndex = 5;
            KCSview.Text = " - - - - \r\n - - - - \r\n - - - - ";
            KCSview.TextAlign = ContentAlignment.TopRight;
            // 
            // Text2
            // 
            Text2.AutoSize = true;
            Text2.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Text2.Location = new Point(528, 134);
            Text2.Name = "Text2";
            Text2.Size = new Size(185, 25);
            Text2.TabIndex = 6;
            Text2.Text = "ram:                   time:";
            // 
            // RAMview
            // 
            RAMview.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            RAMview.Location = new Point(570, 134);
            RAMview.Name = "RAMview";
            RAMview.Size = new Size(95, 25);
            RAMview.TabIndex = 7;
            RAMview.Text = "- - - - MB";
            RAMview.TextAlign = ContentAlignment.TopRight;
            // 
            // LogView
            // 
            LogView.AcceptsReturn = true;
            LogView.BackColor = Color.FromArgb(30, 60, 90);
            LogView.BorderStyle = BorderStyle.FixedSingle;
            LogView.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LogView.ForeColor = Color.White;
            LogView.Location = new Point(534, 281);
            LogView.Multiline = true;
            LogView.Name = "LogView";
            LogView.ReadOnly = true;
            LogView.ScrollBars = ScrollBars.Vertical;
            LogView.Size = new Size(260, 112);
            LogView.TabIndex = 8;
            LogView.TabStop = false;
            // 
            // MSView
            // 
            MSView.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            MSView.Location = new Point(662, 134);
            MSView.Name = "MSView";
            MSView.Size = new Size(138, 25);
            MSView.TabIndex = 9;
            MSView.Text = " - - - - ms";
            MSView.TextAlign = ContentAlignment.TopRight;
            // 
            // MaxsView
            // 
            MaxsView.AcceptsReturn = true;
            MaxsView.BackColor = Color.FromArgb(30, 60, 90);
            MaxsView.BorderStyle = BorderStyle.FixedSingle;
            MaxsView.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MaxsView.ForeColor = Color.White;
            MaxsView.Location = new Point(534, 163);
            MaxsView.Multiline = true;
            MaxsView.Name = "MaxsView";
            MaxsView.ReadOnly = true;
            MaxsView.ScrollBars = ScrollBars.Vertical;
            MaxsView.Size = new Size(260, 112);
            MaxsView.TabIndex = 10;
            MaxsView.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(30, 60, 90);
            ClientSize = new Size(800, 400);
            Controls.Add(MaxsView);
            Controls.Add(RAMview);
            Controls.Add(Text2);
            Controls.Add(MSView);
            Controls.Add(LogView);
            Controls.Add(groupBox1);
            Controls.Add(MainImage);
            Font = new Font("Yu Gothic UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "kmdv";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)MainImage).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer Gettimer;
        private PictureBox MainImage;
        private Label Text1;
        private GroupBox groupBox1;
        private Label KCSview;
        private Label Text2;
        private Label RAMview;
        private TextBox LogView;
        private Label Text0;
        private Label VersionView;
        private Label MSView;
        private TextBox MaxsView;
    }
}