using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static int Clamp(this int value, int maxValue)
        {
            if (value < 0)
            {
                value = (value % maxValue) + maxValue;
            }

            return value % maxValue;
        }

        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        public static string WithColor(this string value, string colorHex)
        {
            return $"<color=#{colorHex}>{value}</color>";
        }

        public static string ToHex(this Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}
