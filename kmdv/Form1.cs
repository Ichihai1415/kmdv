using kmdv.Properties;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Media;
using System.Net;
using static kmdv.Converter;

namespace kmdv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            VersionView.Text = "kmdv v0.4.6";
            LogView.Text = $"start:{DateTime.Now:yyyy/MM/dd HH:mm:ss}";
            if (File.Exists("backmap.png"))
                MainImage.BackgroundImage = new Bitmap(File.OpenRead("backmap.png"));
        }

        public static Dictionary<int[], double> RGB2kcs = new(new ArrayEqualityComparer<int>());
        public readonly static HttpClient client = new();
        public readonly static int getDelay = 1500;
        public static double[] kcsMaxs = new double[] { 9, 0, 0, 0 };//最後の分/10(切り捨て),rssm,acsm,acss
        public static string kcsMaxsText = "";
        public readonly static int GraphValueCount = 60;
        public static ObservableCollection<ObservableValue> GraphValue_acss = new();
        public static ObservableCollection<ObservableValue> GraphValue_acsm = new();
        public static ObservableCollection<ObservableValue> GraphValue_rssm = new();
        public static SoundPlayer? player_ac;
        public static SoundPlayer? player_rs;
        public static int latestSindo = 0;

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("sound"))
                Directory.CreateDirectory("sound");

            if (!File.Exists("sound\\alarm15.wav"))
                File.WriteAllBytes("sound\\alarm15.wav", Resources.alarm15wav);
            if (!File.Exists("sound\\alarm25.wav"))
                File.WriteAllBytes("sound\\alarm25.wav", Resources.alarm25wav);
            if (!File.Exists("sound\\alarm35.wav"))
                File.WriteAllBytes("sound\\alarm35.wav", Resources.alarm35wav);

            if (!File.Exists("sound\\s3.wav"))
                File.WriteAllBytes("sound\\s3.wav", Resources.s3wav);
            if (!File.Exists("sound\\s4.wav"))
                File.WriteAllBytes("sound\\s4.wav", Resources.s4wav);
            if (!File.Exists("sound\\s5.wav"))
                File.WriteAllBytes("sound\\s5.wav", Resources.s5wav);
            if (!File.Exists("sound\\s6.wav"))
                File.WriteAllBytes("sound\\s6.wav", Resources.s6wav);
            if (!File.Exists("sound\\s7.wav"))
                File.WriteAllBytes("sound\\s7.wav", Resources.s7wav);
            if (!File.Exists("sound\\s8.wav"))
                File.WriteAllBytes("sound\\s8.wav", Resources.s8wav);
            if (!File.Exists("sound\\s9.wav"))
                File.WriteAllBytes("sound\\s9.wav", Resources.s9wav);

            //setup
            KCSGraph.DrawMargin = new Margin(10);
            KCSGraph.Tooltip = null;
            KCSGraph.XAxes = new Axis[]
            {
                new() {
                    Name = "time(60s)",
                    NamePaint = new SolidColorPaint(SKColors.White),
                    NameTextSize = 12,

                    TextSize = 0,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1 }
                }
            };
            KCSGraph.YAxes = new Axis[]
            {
                new() {
                    Name = "kcs (ac-s sum)",
                    NamePaint = new SolidColorPaint(SKColors.Red),
                    NameTextSize = 12,

                    LabelsPaint = new SolidColorPaint(SKColors.Red),
                    TextSize = 14,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1 },
                    MinLimit = 0
                },
                new() {
                    Name = "kcs (ac-s max)",
                    NamePaint = new SolidColorPaint(SKColors.Yellow),
                    NameTextSize = 10,

                    LabelsPaint = new SolidColorPaint(SKColors.Yellow),
                    TextSize = 12,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1 },

                    MaxLimit = 1,
                    MinLimit = 0
                },
                new() {
                    Name = "kcs (rs-s max)",
                    NamePaint = new SolidColorPaint(SKColors.LightGreen),
                    NameTextSize = 10,

                    LabelsPaint = new SolidColorPaint(SKColors.LightGreen),
                    TextSize = 12,


                    MaxLimit = 1,
                    MinLimit = 0,
                    Position = AxisPosition.End
                }
            };
            KCSGraph.Series = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue?>
                {

                    Values = GraphValue_acss,
                    Stroke = new SolidColorPaint(SKColors.Red, 2),
                    GeometrySize = 0,
                    Fill = null,
                    GeometryStroke = null,
                    ScalesYAt = 0,
                },
                new LineSeries<ObservableValue?>
                {

                    Values = GraphValue_acsm,
                    Stroke = new SolidColorPaint(SKColors.Yellow, 1),
                    GeometrySize = 0,
                    Fill = null,
                    GeometryStroke = null,
                    ScalesYAt = 1
                },
                new LineSeries<ObservableValue?>
                {

                    Values = GraphValue_rssm,
                    Stroke = new SolidColorPaint(SKColors.LightGreen, 1),
                    GeometrySize = 0,
                    Fill = null,
                    GeometryStroke = null,
                    ScalesYAt = 2
                }
            };
            for (int i = 0; i < GraphValueCount; i++)
            {
                GraphValue_acss.Add(new ObservableValue(0));
                GraphValue_acsm.Add(new ObservableValue(0));
                GraphValue_rssm.Add(new ObservableValue(0));
                await Task.Delay(10);
            }

            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            Gettimer.Enabled = true;//start
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
            Task<double> SindokcsTask = Task.Run(() => Image2kcsSindo(SindoImg));
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
            double PGAkcsMax = PGAkcsTask.Result[1];//g=10^(5x-2)
            double sindokcsMax = SindokcsTask.Result;
            MainImage.Image = mainImg;
            g.Dispose();

            GraphInsert_acss(PGAkcsSum);
            GraphInsert_acsm(PGAkcsMax);
            GraphInsert_rssm(sindokcsMax);

            KCSView_rss.Text = $"*{sindokcsMax * 100:0}".Replace("*0", "----").Replace("*", "");
            KCSView_acs.Text = $"*{PGAkcsSum:0}/{PGAkcsMax:0.00}".Replace("*0/", "----/").Replace("/0.00", "/----").Replace("*", "");
            RAMview.Text = $"{Environment.WorkingSet / 1048576d:0.0}";
            MSView.Text = (DateTime.Now - getTime).TotalMilliseconds >= 1000 ? $"{(DateTime.Now - getTime.AddSeconds(1)).TotalMilliseconds:0}ms" : $"{(DateTime.Now - getTime.AddSeconds(1)).TotalMilliseconds:0.0}ms";

            double min_10 = Math.Floor(getTime.Minute / 10d);
            if (min_10 == kcsMaxs[0])
            {
                kcsMaxs[1] = Math.Max(kcsMaxs[1], sindokcsMax);
                kcsMaxs[2] = Math.Max(kcsMaxs[2], PGAkcsMax);
                kcsMaxs[3] = Math.Max(kcsMaxs[3], PGAkcsSum);
            }
            else//10分ごとに更新
            {
                kcsMaxsText = MaxsView.Text;
                kcsMaxs[0] = min_10;
                kcsMaxs[1] = sindokcsMax;
                kcsMaxs[2] = PGAkcsMax;
                kcsMaxs[3] = PGAkcsSum;
            }
            MaxsView.Text = $"{getTime:HH}:{kcsMaxs[0]}0~ rssm:{kcsMaxs[1]:.00} acsm:{kcsMaxs[2]:.000} acss:{kcsMaxs[3]:0}\r\n{kcsMaxsText}";

            int sindo = 0;
            if (sindokcsMax >= 0.95)
                sindo = 9;
            else if (sindokcsMax >= 0.9)
                sindo = 8;
            else if (sindokcsMax >= 0.85)
                sindo = 7;
            else if (sindokcsMax >= 0.8)
                sindo = 6;
            else if (sindokcsMax >= 0.75)
                sindo = 5;
            else if (sindokcsMax >= 0.65)
                sindo = 4;
            else if (sindokcsMax >= 0.55)
                sindo = 3;
            else if (sindokcsMax >= 0.45)
                sindo = 2;
            else if (sindokcsMax >= 0.35)
                sindo = 1;

            if (sindo > 2 && sindo - latestSindo > 0)
                PlaySound($"s{sindo}.wav", false);
            else if (getTime.Second % 2 == 0)
                if (PGAkcsSum >= 2500)
                    PlaySound("alarm35.wav", true);
                else if (PGAkcsSum >= 1500)
                    PlaySound("alarm25.wav", true);
                else if (PGAkcsSum >= 750)
                    PlaySound("alarm15.wav", true);
                else if (PGAkcsMax >= 0.66)//0.6で10 0.66でだいたい20 (log10(x)+2)/5
                    PlaySound("pga20+.wav", true);
            if (sindo != 0)
                latestSindo = sindo;
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
                    throw new Exception($"取得失敗({res.StatusCode})");
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
            catch (Exception ex)//開始時に例外が起こることがある(起動直後のみ?)
            {
                Debug.WriteLine(ex.Message);
                LogView.Text = $"-----{DateTime.Now:MM/dd HH:mm:ss} <Image2kcs>\r\n{ex}\r\n{LogView.Text}";
                return new double[] { 0, 0 };
            }
        }

        /// <summary>
        /// 画像から強震モニタカラースケールの強震震度を取得します。
        /// </summary>
        /// <remarks>テーブル取得式です。カラースケール計算より正確と思われます。</remarks>
        /// <param name="image">強震モニタの画像。</param>
        /// <returns>kcsの最大</returns>
        public double Image2kcsSindo(Bitmap image)
        {
            try
            {
                Debug.WriteLine($"image2kcs");
                if (image.Width != 352 || image.Height != 400)
                    return 0;
                double kcsSum = 0;
                double kcsMax = 0;
                for (int i = 0; i < 352; i++)
                    for (int j = 0; j < 400; j++)
                    {
                        Color color = image.GetPixel(i, j);
                        int[] colors = new int[] { color.R, color.G, color.B };
                        if (colors.Sum() == 0)
                            continue;
                        double kcs = (RGB2Sindo[colors] + 3d) / 10d;
                        kcsMax = Math.Max(kcsMax, kcs);
                        kcsSum += kcs;
                    }
                return kcsMax;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                LogView.Text = $"-----{DateTime.Now:MM/dd HH:mm:ss} <Image2kcsSindo>\r\n{ex}\r\n{LogView.Text}";
                return 0;
            }
        }

        /// <summary>
        /// acssグラフ用データ配列に値を挿入します。
        /// </summary>
        /// <param name="value">挿入する値。0の場合nullとなります。</param>
        public static void GraphInsert_acss(double value)
        {
            for (int i = 0; i < GraphValueCount - 1; i++)
                GraphValue_acss[i] = GraphValue_acss[i + 1];
            GraphValue_acss[GraphValueCount - 1] = new ObservableValue(value == 0 ? null : value);
        }

        /// <summary>
        /// acsmグラフ用データ配列に値を挿入します。
        /// </summary>
        /// <param name="value">挿入する値。0の場合nullとなります。</param>
        public static void GraphInsert_acsm(double value)
        {
            for (int i = 0; i < GraphValueCount - 1; i++)
                GraphValue_acsm[i] = GraphValue_acsm[i + 1];
            GraphValue_acsm[GraphValueCount - 1] = new ObservableValue(value == 0 ? null : value);
        }

        /// <summary>
        /// rssmグラフ用データ配列に値を挿入します。
        /// </summary>
        /// <param name="value">挿入する値。0の場合nullとなります。</param>
        public static void GraphInsert_rssm(double value)
        {
            for (int i = 0; i < GraphValueCount - 1; i++)
                GraphValue_rssm[i] = GraphValue_rssm[i + 1];
            GraphValue_rssm[GraphValueCount - 1] = new ObservableValue(value == 0 ? null : value);
        }

        /// <summary>
        /// 音声を再生します。
        /// </summary>
        /// <remarks>音声は自動でコピーされます。</remarks>
        /// <param name="fileName">再生するファイル名(sound\\)</param>
        /// <param name="isAC">kcs(acs-s/m)の場合True</param>
        public static void PlaySound(string fileName, bool isAC)
        {
            if (!Directory.Exists("sound"))
            {
                Directory.CreateDirectory("sound");

                if (!File.Exists("sound\\alarm15.wav"))
                    File.WriteAllBytes("sound\\alarm15.wav", Resources.alarm15wav);
                if (!File.Exists("sound\\alarm25.wav"))
                    File.WriteAllBytes("sound\\alarm25.wav", Resources.alarm25wav);
                if (!File.Exists("sound\\alarm35.wav"))
                    File.WriteAllBytes("sound\\alarm35.wav", Resources.alarm35wav);

                if (!File.Exists("sound\\s3.wav"))
                    File.WriteAllBytes("sound\\s3.wav", Resources.s3wav);
                if (!File.Exists("sound\\s4.wav"))
                    File.WriteAllBytes("sound\\s4.wav", Resources.s4wav);
                if (!File.Exists("sound\\s5.wav"))
                    File.WriteAllBytes("sound\\s5.wav", Resources.s5wav);
                if (!File.Exists("sound\\s6.wav"))
                    File.WriteAllBytes("sound\\s6.wav", Resources.s6wav);
                if (!File.Exists("sound\\s7.wav"))
                    File.WriteAllBytes("sound\\s7.wav", Resources.s7wav);
                if (!File.Exists("sound\\s8.wav"))
                    File.WriteAllBytes("sound\\s8.wav", Resources.s8wav);
                if (!File.Exists("sound\\s9.wav"))
                    File.WriteAllBytes("sound\\s9.wav", Resources.s9wav);
            }

            if (isAC)
            {
                if (player_ac != null)
                {
                    player_ac.Stop();
                    player_ac.Dispose();
                    player_ac = null;
                }
                player_ac = new SoundPlayer($"sound\\{fileName}");
                player_ac.Play();
            }
            else
            {
                if (player_rs != null)
                {
                    player_rs.Stop();
                    player_rs.Dispose();
                    player_rs = null;
                }
                player_rs = new SoundPlayer($"sound\\{fileName}");
                player_rs.Play();
            }
        }
    }
}