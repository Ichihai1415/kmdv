using System.Diagnostics;
using System.Net;
using static kmdv.Converter;

namespace kmdv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static Dictionary<int[], double> RGB2kcs = new(new ArrayEqualityComparer<int>())
        {
            { new int[]{ 0, 0, 0 }, 0 }
        };
        public readonly static HttpClient client = new();
        public readonly static int getDelay = 1500;
        public static double[] kcsMaxs = new double[] { 9, 0, 0, 0 };//最後の分/10(切り捨てt),rssm,acsm,acss
        public static string kcsMaxsText = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            VersionView.Text = "kmdv v0.2.4";
            LogView.Text = $"start:{DateTime.Now:yyyy/MM/dd HH:mm:ss}";
            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            Gettimer.Enabled = true;
        }

        private async void Gettimer_Tick(object sender, EventArgs e)
        {
            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            DateTime getTime = DateTime.Now.AddSeconds(-1);

            Task<Bitmap> PGATask = Task.Run(() => GetKyoshin(KyoshinImage.PGA_Sur, getTime));
            Task<Bitmap> SindoTask = Task.Run(() => GetKyoshin(KyoshinImage.Sindo_Sur, getTime));
            Task<Bitmap> ChoTask = Task.Run(() => GetKyoshin(KyoshinImage.Cho_Kai, getTime));
            Task<Bitmap> PGDTask = Task.Run(() => GetKyoshin(KyoshinImage.PGD_Sur, getTime));
            await Task.WhenAll(PGATask, SindoTask, ChoTask, PGDTask);

            Bitmap PGAImg = PGATask.Result;
            Bitmap SindoImg = SindoTask.Result;
            Bitmap ChoImg = ChoTask.Result;
            Bitmap PGDImg = PGDTask.Result;

            Task<double[]> PGAkcsTask = Task.Run(() => Image2kcs(PGAImg));
            Task<double[]> SindokcsTask = Task.Run(() => Image2kcs(SindoImg));
            await Task.WhenAll(PGAkcsTask, SindokcsTask);

            Bitmap mainImg = new(528, 400);
            Graphics g = Graphics.FromImage(mainImg);
            if (PGAImg.Width == 352)
                g.DrawImage(PGAImg, 0, 0);
            if (ChoImg.Width == 352)
                g.DrawImage(ChoImg, 352, 0, 176, 200);
            if (PGDImg.Width == 352)
                g.DrawImage(PGDImg, 352, 200, 176, 200);

            double PGAkcsSum = PGAkcsTask.Result[0];
            double PGAkcsMax = PGAkcsTask.Result[1];
            Debug.WriteLine($"{PGAkcsSum}  {PGAkcsMax} ({KCS2PGA(PGAkcsMax)})");
            double SindokcsSum = SindokcsTask.Result[0];
            double SindokcsMax = SindokcsTask.Result[1];
            Debug.WriteLine($"{SindokcsSum}  {SindokcsMax} ({KCS2Sindo(SindokcsMax)})");
            MainImage.Image = mainImg;
            g.Dispose();

            Debug.WriteLine($"{Environment.WorkingSet / 1048576}MB");

            KCSview.Text = $"{SindokcsMax:.0000}\n{PGAkcsMax:.0000}\n{PGAkcsSum:.0000}";
            RAMview.Text = $"{Environment.WorkingSet / 1048576d:0.00}MB";
            MSView.Text = $"{(DateTime.Now - getTime.AddSeconds(1)).TotalMilliseconds:0.000}ms";

            double min_10 = Math.Floor(getTime.Minute / 10d);
            if (min_10 == kcsMaxs[0])
            {
                kcsMaxs[1] = Math.Max(kcsMaxs[1], SindokcsMax);
                kcsMaxs[2] = Math.Max(kcsMaxs[2], PGAkcsMax);
                kcsMaxs[3] = Math.Max(kcsMaxs[3], PGAkcsSum);
            }
            else
            {
                kcsMaxsText = MaxsView.Text;
                kcsMaxs[0] = min_10;
                kcsMaxs[1] = SindokcsMax;
                kcsMaxs[2] = PGAkcsMax;
                kcsMaxs[3] = PGAkcsSum;
            }
            MaxsView.Text = $"{getTime:HH}:{kcsMaxs[0]}0~ rssm:{kcsMaxs[1]:.000} acsm:{kcsMaxs[2]:.000} acss:{kcsMaxs[3]:0}\r\n{kcsMaxsText}";


        }


        /// <summary>
        /// 強震モニタの画像を取得します。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns>強震モニタの画像。失敗時はBitmap(1, 1)。</returns>
        public static async Task<Bitmap> GetKyoshin(KyoshinImage type, DateTime time)
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync(KyoshinURL[type].Replace("{time}", time.ToString("yyyyMMdd/yyyyMMddHHmmss")));
                if (res.StatusCode != HttpStatusCode.OK)
                    throw new Exception("取得失敗");
                return new Bitmap(res.Content.ReadAsStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new Bitmap(1, 1);
            }
        }

        /// <summary>
        /// 画像から強震モニタカラースケールを取得します。
        /// </summary>
        /// <param name="image">強震モニタの画像。</param>
        /// <returns>[0]:kcsの合計 [1]:kcsの最大</returns>
        public double[] Image2kcs(Bitmap image)
        {
            try
            {
                Debug.WriteLine($"image2kcs");
                if (image.Width != 352 || image.Height != 400)
                    return new double[] { 0, 0 };
                double kcsSum = 0;
                double kcsMax = 0;
                for (int i = 0; i < 352; i++)
                    for (int j = 0; j < 400; j++)
                    {
                        Color color = image.GetPixel(i, j);
                        int[] colors = new int[] { color.R, color.G, color.B };
                        if (colors.Sum() == 0)
                            continue;
                        double kcs;
                        if (RGB2kcs.ContainsKey(colors))
                            kcs = RGB2kcs[colors];
                        else
                        {
                            kcs = Color2KCS(colors);
                            RGB2kcs[colors] = kcs;//Addだと判定潜り抜けて重複エラーになることがある
                        }
                        kcsMax = Math.Max(kcsMax, kcs);
                        kcsSum += kcs;
                    }
                return new double[] { kcsSum, kcsMax };

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                LogView.Text = $"-----{DateTime.Now:MM/dd HH:mm:ss} <Image2kcs>\r\n{ex}\r\n{LogView.Text}";
                return new double[] { 0, 0 };
            }
        }
    }
}