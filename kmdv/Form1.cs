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

        private void Form1_Load(object sender, EventArgs e)
        {
            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            Gettimer.Enabled = true;
        }

        private async void Gettimer_Tick(object sender, EventArgs e)
        {
            Gettimer.Interval = 1000 + getDelay % 1000 - DateTime.Now.Millisecond % 1000;
            DateTime getTime = DateTime.Now.AddSeconds(-1);
            Task<Bitmap> PGATask = Task.Run(() => Get_PGA(getTime));


            await Task.WhenAll(PGATask);
            Bitmap PGAImg = PGATask.Result;

            Task<double[]> PGAkcs = Task.Run(() => Image2kcs(PGAImg));

            await Task.WhenAll(PGAkcs);

            Bitmap mainImg = new(528, 400);
            Graphics g = Graphics.FromImage(mainImg);
            g.DrawImage(PGAImg, 0, 0);
            g.DrawImage(PGAImg, 352, 0,176,200);
            g.DrawImage(PGAImg, 352, 200, 176, 200);


            double PGAkcsSum = PGAkcs.Result[0];
            double PGAkcsMax = PGAkcs.Result[1];
            Debug.WriteLine($"{PGAkcsSum}  {PGAkcsMax} ({KCS2PGA(PGAkcsMax)})");
            MainImage.Image = mainImg;
            g.Dispose();

        }

        public static async Task<Bitmap> Get_PGA(DateTime time)
        {
            Debug.WriteLine($"get_pga");
            //http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/acmap_s/20230922/20230922203835.acmap_s.gif
            HttpResponseMessage res = await client.GetAsync($"http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/acmap_s/{time:yyyyMMdd/yyyyMMddHHmmss}.acmap_s.gif");
            if (res.StatusCode != HttpStatusCode.OK)
                return new Bitmap(0, 0);
            return new Bitmap(res.Content.ReadAsStream());
        }


        public static double[] Image2kcs(Bitmap image)
        {
            Debug.WriteLine($"image2kcs");
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
                        RGB2kcs.Add(colors, kcs);
                    }
                    kcsMax = Math.Max(kcsMax, kcs);
                    kcsSum += kcs;
                    //throw new Exception();
                }
            return new double[] { kcsSum, kcsMax };
        }
    }

}