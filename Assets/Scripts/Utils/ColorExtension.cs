using UnityEngine;

namespace Utils
{
    public static class ColorExtension
    {
        public static int ToInt(this Color color)
        {
            var r = FloatToByte(color.r);
            var g = FloatToByte(color.g);
            var b = FloatToByte(color.b);
            var a = FloatToByte(color.a);
            return ((r << 24) + (g << 16) + (b << 8) + a);
        }

        public static int ToIntWithSomePrecision(this Color color, int divider)
        {
            var r = FloatToByte(color.r);
            var g = FloatToByte(color.g);
            var b = FloatToByte(color.b);
            var a = FloatToByte(color.a);

            r -= r % divider;
            g -= g % divider;
            b -= b % divider;
            return ((r << 24) + (g << 16) + (b << 8) + a);
        }

        public static Color ToColor(this int color)
        {
            float r = (color >> 24 & 0xFF) / 255f;
            float g = (color >> 16 & 0xFF) / 255f;
            float b = (color >> 8 & 0xFF) / 255f;
            float a = (color & 0xFF) / 255f;
            return new Color(r,g,b,a);
        }

        private static int FloatToByte(float value)
        {
            return Mathf.FloorToInt(value* 255);
        }
    }
}