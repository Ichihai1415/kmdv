namespace kmdv
{
    /// <summary>
    /// トカラモニター
    /// </summary>
    /// <remarks>震度は(+3)*10したrsm形式で</remarks>
    public partial class TokaraShakeChecker : Form
    {
        internal int[] value = [-69, -69, -69, -69];
        internal Color[] color = [Color.FromArgb(30, 60, 90), Color.FromArgb(30, 60, 90), Color.FromArgb(30, 60, 90), Color.FromArgb(30, 60, 90)];
        private double[] lastValue = [-69, -69, -69, -69];
        public TokaraShakeChecker()
        {
            InitializeComponent();
        }

        public void SetValues()
        {
            if (value[1] > 20 && lastValue[1] <= 20 && lastValue[1] != -69)//-1.5
                Form1.PlaySound("tokara1.wav", true);
            if (value[1] > 30 && value[3] > 1 && (lastValue[1] <= 30 || lastValue[3] <= 1) && lastValue[1] != -69)//0,-2.9
                Form1.PlaySound("tokara2.wav", true);

            for (int i = 0; i < 4; i++)
                lastValue[i] = value[i];

            L_value.Text = $"{value[0]}\n\n{value[1]}\n{value[2]}\n{value[3]}".Replace("-69", "---");
            L_color0.BackColor = color[0];
            L_color1.BackColor = color[1];
            L_color2.BackColor = color[2];
            L_color3.BackColor = color[3];
        }
    }
}
