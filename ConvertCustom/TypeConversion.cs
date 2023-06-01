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

        /// <summary>
        /// default:false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TryBoolean<T>(this T text)
        {
            if (bool.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return false;
        }

        /// <summary>
        /// default:1900-01-01
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime TryDateTime<T>(this T text)
        {
            if (DateTime.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return DateTime.Parse("1900-01-01");
        }

        /// <summary>
        /// default:0m
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal TryDecimal<T>(this T text)
        {
            if (decimal.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return 0m;
        }

        /// <summary>
        /// default:0d
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double TryDouble<T>(this T text)
        {
            if (double.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return 0d;
        }

        /// <summary>
        /// default:0f
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static float TryFloat<T>(this T text)
        {
            if (float.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return 0f;
        }

        /// <summary>
        /// default:0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int TryInt<T>(this T text)
        {
            if (int.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return 0;
        }
    }
}