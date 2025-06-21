using System.Collections;

namespace kmdv
{
    public class Converter
    {
        /// <summary>
        /// 強震モニタ画像の種類
        /// </summary>
        /// <remarks>Sur:地表 Bor:地中 _lmoni:長周期サーバー</remarks>
        public enum KyoshinImage
        {
            Sindo_Sur = 1,
            Sindo_Bor = 2,
            PGA_Sur = 3,
            PGA_Bor = 4,
            PGV_Sur = 5,
            PGV_Bor = 6,
            PGD_Sur = 7,
            PGD_Bor = 8,
            Res_0125_Sur = 9,
            Res_0125_Bor = 10,
            Res_0250_Sur = 11,
            Res_0250_Bor = 12,
            Res_0500_Sur = 13,
            Res_0500_Bor = 14,
            Res_1000_Sur = 15,
            Res_1000_Bor = 16,
            Res_2000_Sur = 17,
            Res_2000_Bor = 18,
            Res_4000_Sur = 19,
            Res_4000_Bor = 20,
            EEWSindo = 21,
            PSWave = 22,

            Sindo_Sur_lmoni = 31,
            Sindo_Bor_lmoni = 32,
            PGA_Sur_lmoni = 33,
            PGA_Bor_lmoni = 34,
            PGV_Sur_lmoni = 35,
            PGV_Bor_lmoni = 36,
            PGD_Sur_lmoni = 37,
            PGD_Bor_lmoni = 38,
            Res_0125_Sur_lmoni = 39,
            Res_0125_Bor_lmoni = 40,
            Res_0250_Sur_lmoni = 41,
            Res_0250_Bor_lmoni = 42,
            Res_0500_Sur_lmoni = 43,
            Res_0500_Bor_lmoni = 44,
            Res_1000_Sur_lmoni = 45,
            Res_1000_Bor_lmoni = 46,
            Res_2000_Sur_lmoni = 47,
            Res_2000_Bor_lmoni = 48,
            Res_4000_Sur_lmoni = 49,
            Res_4000_Bor_lmoni = 50,
            EEWSindo_lmoni = 51,
            PSWave_lmoni = 52,

            Cho_Kai = 61,
            Cho_1s = 62,
            Cho_2s = 63,
            Cho_3s = 64,
            Cho_4s = 65,
            Cho_5s = 66,
            Cho_6s = 67,
            Cho_7s = 68
        }

        /// <summary>
        /// 強震モニタ画像のURL
        /// </summary>
        /// <remarks>{time}:yyyyMMdd/yyyyMMddHHmmss</remarks>
        public static readonly Dictionary<KyoshinImage, string> KyoshinURL = new()
        {
            { KyoshinImage.Sindo_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/jma_s/{time}.jma_s.gif" },
            { KyoshinImage.Sindo_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/jma_b/{time}.jma_b.gif" },
            { KyoshinImage.PGA_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/acmap_s/{time}.acmap_s.gif" },
            { KyoshinImage.PGA_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/acmap_b/{time}.acmap_b.gif" },
            { KyoshinImage.PGV_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/vcmap_s/{time}.vcmap_s.gif" },
            { KyoshinImage.PGV_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/vcmap_b/{time}.vcmap_b.gif" },
            { KyoshinImage.PGD_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/dcmap_s/{time}.dcmap_s.gif" },
            { KyoshinImage.PGD_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/dcmap_b/{time}.dcmap_b.gif" },
            { KyoshinImage.Res_0125_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp0125_s/{time}.rsp0125_s.gif" },
            { KyoshinImage.Res_0125_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp0125_b/{time}.rsp0125_b.gif" },
            { KyoshinImage.Res_0250_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp0250_s/{time}.rsp0250_s.gif" },
            { KyoshinImage.Res_0250_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp0250_b/{time}.rsp0250_b.gif" },
            { KyoshinImage.Res_0500_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp0500_s/{time}.rsp0500_s.gif" },
            { KyoshinImage.Res_0500_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp0500_b/{time}.rsp0500_b.gif" },
            { KyoshinImage.Res_1000_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp1000_s/{time}.rsp1000_s.gif" },
            { KyoshinImage.Res_1000_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp1000_b/{time}.rsp1000_b.gif" },
            { KyoshinImage.Res_2000_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp2000_s/{time}.rsp2000_s.gif" },
            { KyoshinImage.Res_2000_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp2000_b/{time}.rsp2000_b.gif" },
            { KyoshinImage.Res_4000_Sur, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp4000_s/{time}.rsp4000_s.gif" },
            { KyoshinImage.Res_4000_Bor, "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/rsp4000_b/{time}.rsp4000_b.gif" },
            { KyoshinImage.EEWSindo, "http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/{time}.eew.gif" },
            { KyoshinImage.PSWave, "http://www.kmoni.bosai.go.jp/data/map_img/PSWaveImg/eew/{time}.eew.gif" },

            { KyoshinImage.Sindo_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/jma_s/{time}.jma_s.gif" },
            { KyoshinImage.Sindo_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/jma_b/{time}.jma_b.gif" },
            { KyoshinImage.PGA_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/acmap_s/{time}.acmap_s.gif" },
            { KyoshinImage.PGA_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/acmap_b/{time}.acmap_b.gif" },
            { KyoshinImage.PGV_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/vcmap_s/{time}.vcmap_s.gif" },
            { KyoshinImage.PGV_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/vcmap_b/{time}.vcmap_b.gif" },
            { KyoshinImage.PGD_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/dcmap_s/{time}.dcmap_s.gif" },
            { KyoshinImage.PGD_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/dcmap_b/{time}.dcmap_b.gif" },
            { KyoshinImage.Res_0125_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp0125_s/{time}.rsp0125_s.gif" },
            { KyoshinImage.Res_0125_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp0125_b/{time}.rsp0125_b.gif" },
            { KyoshinImage.Res_0250_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp0250_s/{time}.rsp0250_s.gif" },
            { KyoshinImage.Res_0250_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp0250_b/{time}.rsp0250_b.gif" },
            { KyoshinImage.Res_0500_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp0500_s/{time}.rsp0500_s.gif" },
            { KyoshinImage.Res_0500_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp0500_b/{time}.rsp0500_b.gif" },
            { KyoshinImage.Res_1000_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp1000_s/{time}.rsp1000_s.gif" },
            { KyoshinImage.Res_1000_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp1000_b/{time}.rsp1000_b.gif" },
            { KyoshinImage.Res_2000_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp2000_s/{time}.rsp2000_s.gif" },
            { KyoshinImage.Res_2000_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp2000_b/{time}.rsp2000_b.gif" },
            { KyoshinImage.Res_4000_Sur_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp4000_s/{time}.rsp4000_s.gif" },
            { KyoshinImage.Res_4000_Bor_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/rsp4000_b/{time}.rsp4000_b.gif" },
            { KyoshinImage.EEWSindo_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/EstShindoImg/eew/{time}.eew.gif" },
            { KyoshinImage.PSWave_lmoni, "https://smi.lmoniexp.bosai.go.jp/data/map_img/PSWaveImg/eew/{time}.eew.gif" },

            { KyoshinImage.Cho_Kai, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrspmx_s/{time}.abrspmx_s.gif" },
            { KyoshinImage.Cho_1s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp1s_s/{time}.abrsp1s_s.gif" },
            { KyoshinImage.Cho_2s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp2s_s/{time}.abrsp2s_s.gif" },
            { KyoshinImage.Cho_3s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp3s_s/{time}.abrsp3s_s.gif" },
            { KyoshinImage.Cho_4s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp4s_s/{time}.abrsp4s_s.gif" },
            { KyoshinImage.Cho_5s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp5s_s/{time}.abrsp5s_s.gif" },
            { KyoshinImage.Cho_6s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp6s_s/{time}.abrsp6s_s.gif" },
            { KyoshinImage.Cho_7s, "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/abrsp7s_s/{time}.abrsp7s_s.gif" },
        };

        /// <summary>
        /// 強震モニタリアルタイム震度色から震度へ
        /// </summary>
        /// <remarks>データ:https://github.com/ingen084/KyoshinShindoColorMap</remarks>
        public readonly static Dictionary<int[], double> RGB2Sindo = new(new ArrayEqualityComparer<int>())
        {
            { new int[] {0, 0, 0}, -9.9},
            { new int[] {0, 0, 205}, -3.0},
            { new int[] {0, 7, 209}, -2.9},
            { new int[] {0, 14, 214}, -2.8},
            { new int[] {0, 21, 218}, -2.7},
            { new int[] {0, 28, 223}, -2.6},
            { new int[] {0, 36, 227}, -2.5},
            { new int[] {0, 43, 231}, -2.4},
            { new int[] {0, 50, 236}, -2.3},
            { new int[] {0, 57, 240}, -2.2},
            { new int[] {0, 64, 245}, -2.1},
            { new int[] {0, 72, 250}, -2.0},
            { new int[] {0, 85, 238}, -1.9},
            { new int[] {0, 99, 227}, -1.8},
            { new int[] {0, 112, 216}, -1.7},
            { new int[] {0, 126, 205}, -1.6},
            { new int[] {0, 140, 194}, -1.5},
            { new int[] {0, 153, 183}, -1.4},
            { new int[] {0, 167, 172}, -1.3},
            { new int[] {0, 180, 161}, -1.2},
            { new int[] {0, 194, 150}, -1.1},
            { new int[] {0, 208, 139}, -1.0},
            { new int[] {6, 212, 130}, -0.9},
            { new int[] {12, 216, 121}, -0.8},
            { new int[] {18, 220, 113}, -0.7},
            { new int[] {25, 224, 104}, -0.6},
            { new int[] {31, 228, 96}, -0.5},
            { new int[] {37, 233, 88}, -0.4},
            { new int[] {44, 237, 79}, -0.3},
            { new int[] {50, 241, 71}, -0.2},
            { new int[] {56, 245, 62}, -0.1},
            { new int[] {63, 250, 54}, 0.0},
            { new int[] {75, 250, 49}, 0.1},
            { new int[] {88, 250, 45}, 0.2},
            { new int[] {100, 251, 41}, 0.3},
            { new int[] {113, 251, 37}, 0.4},
            { new int[] {125, 252, 33}, 0.5},
            { new int[] {138, 252, 28}, 0.6},
            { new int[] {151, 253, 24}, 0.7},
            { new int[] {163, 253, 20}, 0.8},
            { new int[] {176, 254, 16}, 0.9},
            { new int[] {189, 255, 12}, 1.0},
            { new int[] {195, 254, 10}, 1.1},
            { new int[] {202, 254, 9}, 1.2},
            { new int[] {208, 254, 8}, 1.3},
            { new int[] {215, 254, 7}, 1.4},
            { new int[] {222, 255, 5}, 1.5},
            { new int[] {228, 254, 4}, 1.6},
            { new int[] {235, 255, 3}, 1.7},
            { new int[] {241, 254, 2}, 1.8},
            { new int[] {248, 255, 1}, 1.9},
            { new int[] {255, 255, 0}, 2.0},
            { new int[] {254, 251, 0}, 2.1},
            { new int[] {254, 248, 0}, 2.2},
            { new int[] {254, 244, 0}, 2.3},
            { new int[] {254, 241, 0}, 2.4},
            { new int[] {255, 238, 0}, 2.5},
            { new int[] {254, 234, 0}, 2.6},
            { new int[] {255, 231, 0}, 2.7},
            { new int[] {254, 227, 0}, 2.8},
            { new int[] {255, 224, 0}, 2.9},
            { new int[] {255, 221, 0}, 3.0},
            { new int[] {254, 213, 0}, 3.1},
            { new int[] {254, 205, 0}, 3.2},
            { new int[] {254, 197, 0}, 3.3},
            { new int[] {254, 190, 0}, 3.4},
            { new int[] {255, 182, 0}, 3.5},
            { new int[] {254, 174, 0}, 3.6},
            { new int[] {255, 167, 0}, 3.7},
            { new int[] {254, 159, 0}, 3.8},
            { new int[] {255, 151, 0}, 3.9},
            { new int[] {255, 144, 0}, 4.0},
            { new int[] {254, 136, 0}, 4.1},
            { new int[] {254, 128, 0}, 4.2},
            { new int[] {254, 121, 0}, 4.3},
            { new int[] {254, 113, 0}, 4.4},
            { new int[] {255, 106, 0}, 4.5},
            { new int[] {254, 98, 0}, 4.6},
            { new int[] {255, 90, 0}, 4.7},
            { new int[] {254, 83, 0}, 4.8},
            { new int[] {255, 75, 0}, 4.9},
            { new int[] {255, 68, 0}, 5.0},
            { new int[] {254, 61, 0}, 5.1},
            { new int[] {253, 54, 0}, 5.2},
            { new int[] {252, 47, 0}, 5.3},
            { new int[] {251, 40, 0}, 5.4},
            { new int[] {250, 33, 0}, 5.5},
            { new int[] {249, 27, 0}, 5.6},
            { new int[] {248, 20, 0}, 5.7},
            { new int[] {247, 13, 0}, 5.8},
            { new int[] {246, 6, 0}, 5.9},
            { new int[] {245, 0, 0}, 6.0},
            { new int[] {238, 0, 0}, 6.1},
            { new int[] {230, 0, 0}, 6.2},
            { new int[] {223, 0, 0}, 6.3},
            { new int[] {215, 0, 0}, 6.4},
            { new int[] {208, 0, 0}, 6.5},
            { new int[] {200, 0, 0}, 6.6},
            { new int[] {192, 0, 0}, 6.7},
            { new int[] {185, 0, 0}, 6.8},
            { new int[] {177, 0, 0}, 6.9},
            { new int[] {170, 0, 0}, 7.0}
        };

        /// <summary>
        /// 強震モニタ色からHSVを求めます。
        /// </summary>
        /// <remarks>参考:https://sourcechord.hatenablog.com/entry/20130710/1373476676</remarks>
        /// <param name="color">元の色</param>
        /// <returns>HSV</returns>
        public static float[] Color2HSV(Color color)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;
            float[] list = [r, g, b];
            float max = list.Max();
            float min = list.Min();
            float h, s, v;
            if (max == min)
                h = 0;
            else if (max == r)
                h = (60f * (g - b) / (max - min) + 360f) % 360f;
            else if (max == g)
                h = 60f * (b - r) / (max - min) + 120f;
            else
                h = 60f * (r - g) / (max - min) + 240f;
            if (max == 0)
                s = 0;
            else
                s = (max - min) / max;
            v = max;
            return [h, s, v];
        }

        /// <summary>
        /// HSVから強震モニタカラースケール値を求めます。
        /// </summary>
        /// <remarks>参考:https://qiita.com/NoneType1/items/a4d2cf932e20b56ca444</remarks>
        /// <param name="hsv">HSV</param>
        /// <returns>強震モニタカラースケール値</returns>
        public static double HSV2KCS(float[] hsv)
        {
            double p = 0;
            float h = hsv[0] / 360f;//0~1にする
            float s = hsv[1];
            float v = hsv[2];
            if (v > 0.1 && s > 0.75)
                if (h > 0.1476)
                    p = 280.31 * Math.Pow(h, 6) - 916.05 * Math.Pow(h, 5) + 1142.6 * Math.Pow(h, 4) - 709.95 * Math.Pow(h, 3) + 234.65 * Math.Pow(h, 2) - 40.27 * h + 3.2217;
                else if (h > 0.001)
                    p = 151.4 * Math.Pow(h, 4) - 49.32 * Math.Pow(h, 3) + 6.753 * Math.Pow(h, 2) - 2.481 * h + 0.9033;
                else
                    p = -0.005171 * Math.Pow(v, 2) - 0.3282 * v + 1.2236;
            if (p < 0)
                p = 0;
            return p;
        }

        /// <summary>
        /// 強震モニタ色から強震モニタカラースケール値を求めます。
        /// </summary>
        /// <remarks>Color2HSV,HSV2KCSと同じ</remarks>
        /// <param name="colors">元の色</param>
        /// <returns>強震モニタカラースケール値</returns>
        public static double Color2KCS(int[] colors)
        {
            float r = colors[0] / 255f;
            float g = colors[1] / 255f;
            float b = colors[2] / 255f;
            float max = colors.Max() / 255f;
            float min = colors.Min() / 255f;
            float h, s, v;
            if (max == min)
                h = 0;
            else if (max == r)
                h = (60f * (g - b) / (max - min) + 360f) % 360f;
            else if (max == g)
                h = 60f * (b - r) / (max - min) + 120f;
            else
                h = 60f * (r - g) / (max - min) + 240f;
            if (max == 0)
                s = 0;
            else
                s = (max - min) / max;
            v = max;

            double p = 0;
            h /= 360f;//0~1にする
            if (v > 0.1 && s > 0.75)
                if (h > 0.1476)
                    p = 280.31 * Math.Pow(h, 6) - 916.05 * Math.Pow(h, 5) + 1142.6 * Math.Pow(h, 4) - 709.95 * Math.Pow(h, 3) + 234.65 * Math.Pow(h, 2) - 40.27 * h + 3.2217;
                else if (h > 0.001)
                    p = 151.4 * Math.Pow(h, 4) - 49.32 * Math.Pow(h, 3) + 6.753 * Math.Pow(h, 2) - 2.481 * h + 0.9033;
                else
                    p = -0.005171 * Math.Pow(v, 2) - 0.3282 * v + 1.2236;
            if (p < 0)
                p = 0;
            return p;
        }

        /// <summary>
        /// 強震モニタカラースケール値からPGAを求めます。
        /// </summary>
        /// <param name="kcs"></param>
        /// <returns>PGA</returns>
        public static double KCS2PGA(double kcs)
        {
            return Math.Pow(10, 5 * kcs - 2);
        }

        /// <summary>
        /// 強震モニタカラースケール値から強震震度を求めます。
        /// </summary>
        /// <param name="kcs">強震モニタカラースケール値</param>
        /// <returns>強震震度</returns>
        public static double KCS2Sindo(double kcs)
        {
            return 10 * kcs - 3;
        }




    }
    /// <summary>
    /// Dictionaryでの配列比較に必要です。
    /// </summary>
    class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        public bool Equals(T[]? x, T[]? y)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
        }

        public int GetHashCode(T[] obj)
        {
            return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }
    }
}
