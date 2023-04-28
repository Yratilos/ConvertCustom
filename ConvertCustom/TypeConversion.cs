using System;

namespace ConvertCustom
{
    /// <summary>
    /// 常用基本类型转换
    /// </summary>
    public static class TypeConversion
    {
        /// <summary>
        /// ToBoolean
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool? ToBoolean<T>(this T text)
        {
            if (bool.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// ToDateTime
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
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

        /// <summary>
        /// ToDecimal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal? ToDecimal<T>(this T text)
        {
            if (decimal.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// ToDouble
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double? ToDouble<T>(this T text)
        {
            if (double.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// ToFloat
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static float? ToFloat<T>(this T text)
        {
            if (float.TryParse(text?.ToString(), out var result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// ToInt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
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