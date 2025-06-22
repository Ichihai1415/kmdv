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
            if (value[1] >= 15 && lastValue[1] < 15 && lastValue[1] != -69)//-1.5
                Form1.PlaySound("tokara1.wav", true);
            if (value[1] >= 25 && value[3] >= 1 && (lastValue[1] < 25 || lastValue[3] < 1) && lastValue[1] != -69)//-0.5,-2.9
                Form1.PlaySound("tokara2.wav", true);

            var flag0 = value[0] > lastValue[0] ? "↑" : value[0] == lastValue[0] ? "→" : "↓";
            var flag1 = value[1] > lastValue[1] ? "↑" : value[1] == lastValue[1] ? "→" : "↓";
            var flag2 = value[2] > lastValue[2] ? "↑" : value[2] == lastValue[2] ? "→" : "↓";
            var flag3 = value[3] > lastValue[3] ? "↑" : value[3] == lastValue[3] ? "→" : "↓";

            for (int i = 0; i < 4; i++)
                lastValue[i] = value[i];

            L_value.Text = (value[0] + flag0 + "\n\n" + value[1] + flag1 + "\n" + value[2] + flag2 + "\n" + value[3] + flag3).Replace("↓-69", "- - -");
            L_color0.BackColor = color[0];
            L_color1.BackColor = color[1];
            L_color2.BackColor = color[2];
            L_color3.BackColor = color[3];
        }

        private void TokaraShakeChecker_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void L_value_Click(object sender, EventArgs e)
        {

        }
    }
}
