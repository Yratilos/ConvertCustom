using System;

namespace ConvertCustom
{
    /// <summary>
    /// 常用基本类型转换
    /// </summary>
    public static class TypeConversion
    {
        public static bool? ToBoolean<T>(this T text)
        {
            if (bool.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        public static DateTime? ToDateTime<T>(this T text)
        {
            if (DateTime.TryParse(text?.ToString(), out var result))
            {
                if (result == DateTime.Parse("1900-01-01"))
                {
                    return null;
                }
                return result;
            }
            return null;
        }

        public static decimal? ToDecimal<T>(this T text)
        {
            if (decimal.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        public static double? ToDouble<T>(this T text)
        {
            if (double.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        public static float? ToFloat<T>(this T text)
        {
            if (float.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        public static int? ToInt<T>(this T text)
        {
            if (int.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }
    }
}