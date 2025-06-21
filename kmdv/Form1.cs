using kmdv.Properties;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Text;
using static kmdv.Converter;

namespace kmdv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            VersionView.Text = "kmdv v0.5.2";
            LogView.Text = "start:" + DateTime.Now.ToString();
            if (File.Exists("backmap.png"))
                MainImage.BackgroundImage = new Bitmap(File.OpenRead("backmap.png"));
            kcsMaxs[0] = Math.Floor(DateTime.Now.Minute / 10d);
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        internal static Dictionary<int[], double> RGB2kcs = new(new ArrayEqualityComparer<int>());
        internal readonly static HttpClient client = new();
        internal readonly static int getDelay = 1500;
        internal static double[] kcsMaxs = [9, 0, 0, 0];//最後の分/10(切り捨て),rssm,acsm,acss
        internal static string kcsMaxsText = "";
        internal readonly static int GraphValueCount = 60;
        internal static ObservableCollection<ObservableValue> GraphValue_acss = [];
        internal static ObservableCollection<ObservableValue> GraphValue_acsm = [];
        internal static ObservableCollection<ObservableValue> GraphValue_rssm = [];
        internal static SoundPlayer? player_ac;
        internal static SoundPlayer? player_rs;
        internal static int latestSindo = 0;
        internal static StringBuilder kcsLog = new();
        public const string LOG_FOLDER = @"D:\Logs\kmdv";
        internal TokaraShakeChecker tokaraShakeChecker = new();
        public readonly static Point[] TokaraPoints =
        [
            new(39, 393),//屋久島
            new(146,88),//笠利
            new(140, 90),//大和
            new(138, 96),//瀬戸内
        ];

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

            if (!File.Exists("sound\\pga20+.wav"))
                File.WriteAllBytes("sound\\pga20+.wav", Resources.pga20_wav);
            if (!File.Exists("sound\\pga100+.wav"))
                File.WriteAllBytes("sound\\pga100+.wav", Resources.pga100_wav);

            //setup
            KCSGraph.DrawMargin = new Margin(10);
            KCSGraph.Tooltip = null;
            KCSGraph.XAxes =
            [
                new Axis() {
                    Name = "time(60s)",
                    NamePaint = new SolidColorPaint(SKColors.White),
                    NameTextSize = 12,

                    TextSize = 0,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1 }
                }
            ];
            KCSGraph.YAxes =
            [
                new Axis() {
                    Name = "kcs (ac-s sum)",
                    NamePaint = new SolidColorPaint(SKColors.Red),
                    NameTextSize = 12,

                    LabelsPaint = new SolidColorPaint(SKColors.Red),
                    TextSize = 14,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1 },
                    MinLimit = 0
                },
                new Axis() {
                    Name = "kcs (ac-s max)",
                    NamePaint = new SolidColorPaint(SKColors.Yellow),
                    NameTextSize = 10,

                    LabelsPaint = new SolidColorPaint(SKColors.Yellow),
                    TextSize = 12,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1 },

                    MaxLimit = 1,
                    MinLimit = 0
                },
                new Axis() {
                    Name = "kcs (rs-s max)",
                    NamePaint = new SolidColorPaint(SKColors.LightGreen),
                    NameTextSize = 10,

                    LabelsPaint = new SolidColorPaint(SKColors.LightGreen),
                    TextSize = 12,


                    MaxLimit = 1,
                    MinLimit = 0,
                    Position = AxisPosition.End
                }
            ];
            KCSGraph.Series =
            [
                new LineSeries<ObservableValue?>
                {

                    Values = GraphValue_acss!,
                    Stroke = new SolidColorPaint(SKColors.Red, 2),
                    GeometrySize = 0,
                    Fill = null,
                    GeometryStroke = null,
                    ScalesYAt = 0,
                },
                new LineSeries<ObservableValue?>
                {

                    Values = GraphValue_acsm!,
                    Stroke = new SolidColorPaint(SKColors.Yellow, 1),
                    GeometrySize = 0,
                    Fill = null,
                    GeometryStroke = null,
                    ScalesYAt = 1
                },
                new LineSeries<ObservableValue?>
                {

                    Values = GraphValue_rssm!,
                    Stroke = new SolidColorPaint(SKColors.LightGreen, 1),
                    GeometrySize = 0,
                    Fill = null,
                    GeometryStroke = null,
                    ScalesYAt = 2
                }
            ];
            for (int i = 0; i < GraphValueCount; i++)
            {
                GraphValue_acss.Add(new ObservableValue(0));
                GraphValue_acsm.Add(new ObservableValue(0));
                GraphValue_rssm.Add(new ObservableValue(0));
                await Task.Delay(10);
            }

            tokaraShakeChecker.Show();

            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            Gettimer.Enabled = true;//start
        }

        private async void Gettimer_Tick(object sender, EventArgs e)
        {
            Text_loading.Size = new Size(20, 20);
            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            DateTime getTime = DateTime.Now.AddSeconds(-1);

            Task<Bitmap> PGATask = Task.Run(() => GetKyoshin(KyoshinImage.PGA_Sur, getTime));
            Task<Bitmap> SindoTask = Task.Run(() => GetKyoshin(KyoshinImage.Sindo_Sur, getTime));
            Task<Bitmap> ChoTask = Task.Run(() => GetKyoshin(KyoshinImage.Cho_Kai, getTime));
            Task<Bitmap> PGDTask = Task.Run(() => GetKyoshin(KyoshinImage.PGD_Sur, getTime));

            await Task.WhenAll(PGATask, SindoTask, ChoTask, PGDTask);
            Text_loading.Size = new Size(0, 0);

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

            KCSView_rss.Text = ("\u200b" + ((int)(sindokcsMax * 100)).ToString()).Replace("\u200b0", "----");//\u200bはゼロ幅スペース　.Replace("\u200b", "")は不要
            KCSView_acs.Text = ("​\u200b" + ((int)PGAkcsSum).ToString() + "/" + PGAkcsMax.ToString("0.00")).Replace("​\u200b​0/", "----/").Replace("/0.00", "/ ----");
            RAMview.Text = (GC.GetTotalMemory(false) / 1048576d).ToString("0.0");
            MSView.Text = (DateTime.Now - getTime).TotalMilliseconds >= 2000
                ? (DateTime.Now - getTime.AddSeconds(1)).TotalMilliseconds.ToString("0.0") + "ms"
                : (DateTime.Now - getTime.AddSeconds(1)).TotalMilliseconds.ToString("0.0") + "ms";

            double min_10 = Math.Floor(getTime.Minute / 10d);
            if (min_10 == kcsMaxs[0])
            {
                kcsMaxs[1] = Math.Max(kcsMaxs[1], sindokcsMax);
                kcsMaxs[2] = Math.Max(kcsMaxs[2], PGAkcsMax);
                kcsMaxs[3] = Math.Max(kcsMaxs[3], PGAkcsSum);
            }
            else//10分ごとに更新
            {
                var saveTime = getTime.AddMinutes(-10);
                Directory.CreateDirectory(LOG_FOLDER + "\\" + saveTime.ToString("yyyyMM"));
                Directory.CreateDirectory(LOG_FOLDER + "\\" + saveTime.ToString("yyyyMM\\\\dd"));
                File.WriteAllText(LOG_FOLDER + "\\" + saveTime.ToString("yyyyMM\\\\dd\\\\HH-mm") + ".csv", kcsLog.ToString());
                kcsLog = new StringBuilder("dateTime,rssm,acsm,acss\n");

                kcsMaxsText = MaxsView.Text;
                kcsMaxs[0] = min_10;
                kcsMaxs[1] = sindokcsMax;
                kcsMaxs[2] = PGAkcsMax;
                kcsMaxs[3] = PGAkcsSum;

            }
            kcsLog.Append(getTime.ToString("yyyy/MM/dd HH:mm:ss"));
            kcsLog.Append(',');
            kcsLog.Append(sindokcsMax.ToString("0.00"));
            kcsLog.Append(',');
            kcsLog.Append(PGAkcsMax.ToString("0.0000"));
            kcsLog.Append(',');
            kcsLog.Append(PGAkcsSum.ToString("0.00"));
            kcsLog.AppendLine();
            MaxsView.Text = $"{getTime:HH}:{kcsMaxs[0]}0~ rssm:{kcsMaxs[1]:.00} acsm:{kcsMaxs[2]:.000} acss:{kcsMaxs[3]:0}\r\n" + kcsMaxsText;

            if (DateTime.Now - getTime > TimeSpan.FromSeconds(3))//取得遅延+処理時間
            {
                KCSLevel_rssm.BackColor = Color.White;
                KCSLevel_acss.BackColor = Color.White;
                KCSLevel_acsm.BackColor = Color.White;
                return;//強モニ重くなって古いのが再生されないように
            }

            if (SindoImg.Width == 352)
                for (int i = 0; i < 4; i++)
                {
                    Color color = SindoImg.GetPixel(TokaraPoints[i].X, TokaraPoints[i].Y);
                    int[] colors = [color.R, color.G, color.B];
                    var value = (int)((RGB2Sindo[colors] + 3) * 10);
                    tokaraShakeChecker.value[i] = value;
                    tokaraShakeChecker.color[i] = color;
                }
            else
                for (int i = 0; i < 4; i++)
                {
                    tokaraShakeChecker.value[i] = -69;
                    tokaraShakeChecker.color[i] = Color.FromArgb(30, 60, 90);
                }
            tokaraShakeChecker.SetValues();

            GraphInsert_acss(PGAkcsSum);
            GraphInsert_acsm(PGAkcsMax);
            GraphInsert_rssm(sindokcsMax);


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

            switch (sindo)
            {
                case 0:
                case 1:
                case 2:
                    KCSLevel_rssm.BackColor = Color.White;
                    break;
                case 3:
                case 4:
                    KCSLevel_rssm.BackColor = Color.Yellow;
                    break;
                case 5:
                case 6:
                    KCSLevel_rssm.BackColor = Color.Red;
                    break;
                case 7:
                case 8:
                case 9:
                    KCSLevel_rssm.BackColor = Color.FromArgb(200, 0, 200);
                    break;
            }

            if (sindo > 2 && sindo - latestSindo > 0)
                PlaySound("s" + sindo + ".wav", false);
            else if (getTime.Second % 2 == 0)
                if (PGAkcsSum >= 2500)
                {
                    PlaySound("alarm35.wav", true);
                    KCSLevel_acss.BackColor = Color.FromArgb(200, 0, 200);
                }
                else if (PGAkcsSum >= 1800)
                {
                    PlaySound("alarm25.wav", true);
                    KCSLevel_acss.BackColor = Color.Red;
                }
                else if (PGAkcsSum >= 1000)
                {
                    PlaySound("alarm15.wav", true);
                    KCSLevel_acss.BackColor = Color.Yellow;
                }
                else
                    KCSLevel_acss.BackColor = Color.White;
            if (getTime.Second % 2 == 1)
                if (PGAkcsMax >= 0.8)//0.8で100 0.6で10 0.66でだいたい20 (log10(x)+2)/5
                    PlaySound("pga100+.wav", true);
                else if (PGAkcsMax >= 0.66)
                    PlaySound("pga20+.wav", true);
            if (PGAkcsMax >= 0.9)//316.2777
                KCSLevel_acsm.BackColor = Color.FromArgb(200, 0, 200);
            else if (PGAkcsMax >= 0.8)
                KCSLevel_acsm.BackColor = Color.Red;
            else if (PGAkcsMax >= 0.6)
                KCSLevel_acsm.BackColor = Color.Yellow;
            else
                KCSLevel_acsm.BackColor = Color.White;

            if (sindo != 0)
                latestSindo = sindo;
            if (getTime.Second == 0)
                GC.Collect();
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
                    throw new Exception("取得失敗(" + res.StatusCode + ")");
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
                Debug.WriteLine("image2kcs");
                if (image.Width != 352 || image.Height != 400)
                    return [0, 0];
                double kcsSum = 0;
                double kcsMax = 0;
                for (int i = 0; i < 352; i++)
                    for (int j = 0; j < 400; j++)
                    {
                        Color color = image.GetPixel(i, j);
                        int[] colors = [color.R, color.G, color.B];
                        if (colors.Sum() == 0)
                            continue;
                        double kcs;
                        if (RGB2kcs.TryGetValue(colors, out double value))
                            kcs = value;
                        else
                        {
                            kcs = Color2KCS(colors);
                            RGB2kcs[colors] = kcs;//Addだと判定潜り抜けて重複エラーになることがある
                        }
                        kcsMax = Math.Max(kcsMax, kcs);
                        kcsSum += kcs;
                    }
                return [kcsSum, kcsMax];

            }
            catch (Exception ex)//開始時に例外が起こることがある(起動直後のみ?)
            {
                Debug.WriteLine(ex.Message);
                LogView.Text = $"-----{DateTime.Now:MM/dd HH:mm:ss} <Image2kcs>\r\n{ex}\r\n" + LogView.Text;
                return [0, 0];
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
                Debug.WriteLine("image2kcs");
                if (image.Width != 352 || image.Height != 400)
                    return 0;
                double kcsSum = 0;
                double kcsMax = 0;
                for (int i = 0; i < 352; i++)
                    for (int j = 0; j < 400; j++)
                    {
                        Color color = image.GetPixel(i, j);
                        int[] colors = [color.R, color.G, color.B];
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
                LogView.Text = $"-----{DateTime.Now:MM/dd HH:mm:ss} <Image2kcsSindo>\r\n{ex}\r\n" + LogView.Text;
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
        /// <param name="isAC">kcs(acs-s/m)の場合True,rsmの場合False その他は基本True</param>
        public static void PlaySound(string fileName, bool isAC)
        {
            if (!Directory.Exists("sound"))
            {
                Directory.CreateDirectory("sound");

                File.WriteAllBytes("sound\\alarm15.wav", Resources.alarm15wav);
                File.WriteAllBytes("sound\\alarm25.wav", Resources.alarm25wav);
                File.WriteAllBytes("sound\\alarm35.wav", Resources.alarm35wav);

                File.WriteAllBytes("sound\\s3.wav", Resources.s3wav);
                File.WriteAllBytes("sound\\s4.wav", Resources.s4wav);
                File.WriteAllBytes("sound\\s5.wav", Resources.s5wav);
                File.WriteAllBytes("sound\\s6.wav", Resources.s6wav);
                File.WriteAllBytes("sound\\s7.wav", Resources.s7wav);
                File.WriteAllBytes("sound\\s8.wav", Resources.s8wav);
                File.WriteAllBytes("sound\\s9.wav", Resources.s9wav);

                File.WriteAllBytes("sound\\tokara1.wav", Resources.tokara1wav);
                File.WriteAllBytes("sound\\tokara2.wav", Resources.tokara2wav);
            }

            if (isAC)
            {
                if (player_ac != null)
                {
                    player_ac.Stop();
                    player_ac.Dispose();
                    player_ac = null;
                }
                player_ac = new SoundPlayer("sound\\" + fileName);
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
                player_rs = new SoundPlayer("sound\\" + fileName);
                player_rs.Play();
            }
        }
    }
}