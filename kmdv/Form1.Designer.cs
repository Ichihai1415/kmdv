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
            KCSGraph = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            MaxsView = new TextBox();
            KCSView_rss = new Label();
            Text0 = new Label();
            KCSView_acs = new Label();
            VersionView = new Label();
            Text2 = new Label();
            RAMview = new Label();
            LogView = new TextBox();
            MSView = new Label();
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
            Text1.Location = new Point(6, 25);
            Text1.Name = "Text1";
            Text1.Size = new Size(127, 30);
            Text1.TabIndex = 4;
            Text1.Text = "rss:        acs:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(KCSGraph);
            groupBox1.Controls.Add(MaxsView);
            groupBox1.Controls.Add(KCSView_rss);
            groupBox1.Controls.Add(Text0);
            groupBox1.Controls.Add(Text1);
            groupBox1.Controls.Add(KCSView_acs);
            groupBox1.Location = new Point(534, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(260, 254);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "KCS";
            // 
            // KCSGraph
            // 
            KCSGraph.Location = new Point(5, 58);
            KCSGraph.Margin = new Padding(3, 4, 3, 4);
            KCSGraph.Name = "KCSGraph";
            KCSGraph.Size = new Size(245, 84);
            KCSGraph.TabIndex = 11;
            // 
            // MaxsView
            // 
            MaxsView.AcceptsReturn = true;
            MaxsView.BackColor = Color.FromArgb(30, 60, 90);
            MaxsView.BorderStyle = BorderStyle.FixedSingle;
            MaxsView.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MaxsView.ForeColor = Color.White;
            MaxsView.Location = new Point(6, 148);
            MaxsView.Multiline = true;
            MaxsView.Name = "MaxsView";
            MaxsView.ReadOnly = true;
            MaxsView.ScrollBars = ScrollBars.Vertical;
            MaxsView.Size = new Size(248, 100);
            MaxsView.TabIndex = 10;
            MaxsView.TabStop = false;
            // 
            // KCSView_rss
            // 
            KCSView_rss.Location = new Point(42, 25);
            KCSView_rss.Name = "KCSView_rss";
            KCSView_rss.Size = new Size(49, 30);
            KCSView_rss.TabIndex = 10;
            KCSView_rss.Text = "----";
            KCSView_rss.TextAlign = ContentAlignment.TopRight;
            // 
            // Text0
            // 
            Text0.AutoSize = true;
            Text0.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Text0.Location = new Point(9, 2);
            Text0.Name = "Text0";
            Text0.Size = new Size(71, 25);
            Text0.TabIndex = 9;
            Text0.Text = "<KCS>";
            // 
            // KCSView_acs
            // 
            KCSView_acs.Location = new Point(9, 25);
            KCSView_acs.Name = "KCSView_acs";
            KCSView_acs.Size = new Size(242, 30);
            KCSView_acs.TabIndex = 5;
            KCSView_acs.Text = "----/----";
            KCSView_acs.TextAlign = ContentAlignment.TopRight;
            // 
            // VersionView
            // 
            VersionView.AutoSize = true;
            VersionView.Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            VersionView.Location = new Point(738, 0);
            VersionView.Name = "VersionView";
            VersionView.Size = new Size(62, 13);
            VersionView.TabIndex = 9;
            VersionView.Text = "kmdv v0.0.0";
            // 
            // Text2
            // 
            Text2.AutoSize = true;
            Text2.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Text2.Location = new Point(528, 10);
            Text2.Name = "Text2";
            Text2.Size = new Size(193, 25);
            Text2.TabIndex = 6;
            Text2.Text = "ram:             MB  time:";
            // 
            // RAMview
            // 
            RAMview.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            RAMview.Location = new Point(571, 10);
            RAMview.Name = "RAMview";
            RAMview.Size = new Size(66, 25);
            RAMview.TabIndex = 7;
            RAMview.Text = "--------";
            RAMview.TextAlign = ContentAlignment.TopRight;
            // 
            // LogView
            // 
            LogView.AcceptsReturn = true;
            LogView.BackColor = Color.FromArgb(30, 60, 90);
            LogView.BorderStyle = BorderStyle.FixedSingle;
            LogView.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LogView.ForeColor = Color.White;
            LogView.Location = new Point(534, 294);
            LogView.Multiline = true;
            LogView.Name = "LogView";
            LogView.ReadOnly = true;
            LogView.ScrollBars = ScrollBars.Vertical;
            LogView.Size = new Size(260, 100);
            LogView.TabIndex = 8;
            LogView.TabStop = false;
            // 
            // MSView
            // 
            MSView.Font = new Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            MSView.Location = new Point(662, 10);
            MSView.Name = "MSView";
            MSView.Size = new Size(138, 25);
            MSView.TabIndex = 9;
            MSView.Text = "------ms";
            MSView.TextAlign = ContentAlignment.TopRight;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(30, 60, 90);
            ClientSize = new Size(800, 400);
            Controls.Add(VersionView);
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
        private Label KCSView_acs;
        private Label Text2;
        private Label RAMview;
        private TextBox LogView;
        private Label Text0;
        private Label VersionView;
        private Label MSView;
        private TextBox MaxsView;
        private Label KCSView_rss;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart KCSGraph;
    }
}