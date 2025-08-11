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
            KCS_GroupBox = new GroupBox();
            KCSLevel_acsm = new Label();
            KCSLevel_acss = new Label();
            KCSLevel_rssm = new Label();
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
            Text_loading = new Label();
            T_StopAlertRestarter = new System.Windows.Forms.Timer(components);
            CMS = new ContextMenuStrip(components);
            TSMI_StopAlertSound = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)MainImage).BeginInit();
            KCS_GroupBox.SuspendLayout();
            CMS.SuspendLayout();
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
            // KCS_GroupBox
            // 
            KCS_GroupBox.Controls.Add(KCSLevel_acsm);
            KCS_GroupBox.Controls.Add(KCSLevel_acss);
            KCS_GroupBox.Controls.Add(KCSLevel_rssm);
            KCS_GroupBox.Controls.Add(KCSGraph);
            KCS_GroupBox.Controls.Add(MaxsView);
            KCS_GroupBox.Controls.Add(KCSView_rss);
            KCS_GroupBox.Controls.Add(Text0);
            KCS_GroupBox.Controls.Add(Text1);
            KCS_GroupBox.Controls.Add(KCSView_acs);
            KCS_GroupBox.Location = new Point(534, 35);
            KCS_GroupBox.Name = "KCS_GroupBox";
            KCS_GroupBox.Size = new Size(260, 304);
            KCS_GroupBox.TabIndex = 5;
            KCS_GroupBox.TabStop = false;
            KCS_GroupBox.Text = "KCS";
            // 
            // KCSLevel_acsm
            // 
            KCSLevel_acsm.BackColor = Color.White;
            KCSLevel_acsm.ForeColor = Color.White;
            KCSLevel_acsm.Location = new Point(202, 56);
            KCSLevel_acsm.Name = "KCSLevel_acsm";
            KCSLevel_acsm.Size = new Size(40, 1);
            KCSLevel_acsm.TabIndex = 14;
            // 
            // KCSLevel_acss
            // 
            KCSLevel_acss.BackColor = Color.White;
            KCSLevel_acss.ForeColor = Color.White;
            KCSLevel_acss.Location = new Point(141, 56);
            KCSLevel_acss.Name = "KCSLevel_acss";
            KCSLevel_acss.Size = new Size(50, 1);
            KCSLevel_acss.TabIndex = 13;
            // 
            // KCSLevel_rssm
            // 
            KCSLevel_rssm.BackColor = Color.White;
            KCSLevel_rssm.ForeColor = Color.White;
            KCSLevel_rssm.Location = new Point(44, 56);
            KCSLevel_rssm.Name = "KCSLevel_rssm";
            KCSLevel_rssm.Size = new Size(40, 1);
            KCSLevel_rssm.TabIndex = 12;
            // 
            // KCSGraph
            // 
            KCSGraph.Location = new Point(5, 58);
            KCSGraph.Margin = new Padding(3, 4, 3, 4);
            KCSGraph.MatchAxesScreenDataRatio = false;
            KCSGraph.Name = "KCSGraph";
            KCSGraph.Size = new Size(245, 134);
            KCSGraph.TabIndex = 11;
            // 
            // MaxsView
            // 
            MaxsView.AcceptsReturn = true;
            MaxsView.BackColor = Color.FromArgb(30, 60, 90);
            MaxsView.BorderStyle = BorderStyle.FixedSingle;
            MaxsView.Font = new Font("Yu Gothic UI", 10F);
            MaxsView.ForeColor = Color.White;
            MaxsView.Location = new Point(6, 198);
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
            Text0.Font = new Font("Yu Gothic UI", 14F);
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
            KCSView_acs.Text = "----/ ----";
            KCSView_acs.TextAlign = ContentAlignment.TopRight;
            // 
            // VersionView
            // 
            VersionView.AutoSize = true;
            VersionView.Font = new Font("Yu Gothic UI", 8F);
            VersionView.Location = new Point(732, 0);
            VersionView.Name = "VersionView";
            VersionView.Size = new Size(70, 13);
            VersionView.TabIndex = 9;
            VersionView.Text = "kmdv v0.0.0.0";
            // 
            // Text2
            // 
            Text2.AutoSize = true;
            Text2.Font = new Font("Yu Gothic UI", 14F);
            Text2.Location = new Point(528, 10);
            Text2.Name = "Text2";
            Text2.Size = new Size(193, 25);
            Text2.TabIndex = 6;
            Text2.Text = "ram:             MB  time:";
            // 
            // RAMview
            // 
            RAMview.Font = new Font("Yu Gothic UI", 14F);
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
            LogView.Font = new Font("Yu Gothic UI", 10F);
            LogView.ForeColor = Color.White;
            LogView.Location = new Point(534, 344);
            LogView.Multiline = true;
            LogView.Name = "LogView";
            LogView.ReadOnly = true;
            LogView.ScrollBars = ScrollBars.Vertical;
            LogView.Size = new Size(260, 50);
            LogView.TabIndex = 8;
            LogView.TabStop = false;
            // 
            // MSView
            // 
            MSView.Font = new Font("Yu Gothic UI", 14F);
            MSView.Location = new Point(662, 10);
            MSView.Name = "MSView";
            MSView.Size = new Size(138, 25);
            MSView.TabIndex = 9;
            MSView.Text = "------ms";
            MSView.TextAlign = ContentAlignment.TopRight;
            // 
            // Text_loading
            // 
            Text_loading.Font = new Font("Yu Gothic UI", 11F);
            Text_loading.ForeColor = Color.Yellow;
            Text_loading.Location = new Point(528, -3);
            Text_loading.Name = "Text_loading";
            Text_loading.Size = new Size(20, 20);
            Text_loading.TabIndex = 10;
            Text_loading.Text = "↻";
            // 
            // T_StopAlertRestarter
            // 
            T_StopAlertRestarter.Interval = 600000;
            T_StopAlertRestarter.Tick += T_StopAlertRestarter_Tick;
            // 
            // CMS
            // 
            CMS.Items.AddRange(new ToolStripItem[] { TSMI_StopAlertSound });
            CMS.Name = "CMS";
            CMS.Size = new Size(377, 26);
            // 
            // TSMI_StopAlertSound
            // 
            TSMI_StopAlertSound.Name = "TSMI_StopAlertSound";
            TSMI_StopAlertSound.Size = new Size(376, 22);
            TSMI_StopAlertSound.Text = "警告音再生10分間無効(acssは有効,経過後も高い場合延長)";
            TSMI_StopAlertSound.Click += TSMI_StopAlertSound_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(30, 60, 90);
            ClientSize = new Size(800, 400);
            ContextMenuStrip = CMS;
            Controls.Add(Text_loading);
            Controls.Add(VersionView);
            Controls.Add(RAMview);
            Controls.Add(Text2);
            Controls.Add(MSView);
            Controls.Add(LogView);
            Controls.Add(KCS_GroupBox);
            Controls.Add(MainImage);
            Font = new Font("Yu Gothic UI", 16F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "kmdv";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)MainImage).EndInit();
            KCS_GroupBox.ResumeLayout(false);
            KCS_GroupBox.PerformLayout();
            CMS.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer Gettimer;
        private PictureBox MainImage;
        private Label Text1;
        private GroupBox KCS_GroupBox;
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
        private Label Text_loading;
        private Label KCSLevel_rssm;
        private Label KCSLevel_acss;
        private Label KCSLevel_acsm;
        private System.Windows.Forms.Timer T_StopAlertRestarter;
        private ContextMenuStrip CMS;
        private ToolStripMenuItem TSMI_StopAlertSound;
    }
}