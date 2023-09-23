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
            ((System.ComponentModel.ISupportInitialize)MainImage).BeginInit();
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 60, 90);
            ClientSize = new Size(800, 400);
            Controls.Add(MainImage);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "kmdv";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)MainImage).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer Gettimer;
        private PictureBox MainImage;
    }
}