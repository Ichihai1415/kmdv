using System.Collections;

namespace kmdv
{
    internal class Converter
    {
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
            float[] list = new float[] { r, g, b };
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
            return new float[] { h, s, v };
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
        /// <returns></returns>
        public static double KCS2PGA(double kcs)
        {
            return Math.Pow(10, 5 * kcs - 2);
        }






    }
    class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        public bool Equals(T[] x, T[] y)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
        }

        public int GetHashCode(T[] obj)
        {
            return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }
    }
}
