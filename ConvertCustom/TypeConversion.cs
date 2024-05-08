using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConvertCustom
{
    /// <summary>
    /// 常用基本类型转换
    /// </summary>
    public static class TypeConversion
    {
        public static DbType ToDbType(this Type t)
        {
            if (Enum.TryParse(t.Name, out DbType dbt))
            {
                return dbt;
            }
            else
            {
                return DbType.String;
            }
        }

        public static Type ToType(this DbType dbt)
        {
            Dictionary<DbType, Type> typeMap = new Dictionary<DbType, Type>()
            {
                { DbType.UInt64, typeof(UInt64) },
                { DbType.Int64, typeof(Int64) },
                { DbType.Int32, typeof(Int32) },
                { DbType.UInt32, typeof(UInt32) },
                { DbType.Single, typeof(float) },
                { DbType.Date, typeof(DateTime) },
                { DbType.DateTime, typeof(DateTime) },
                { DbType.Time, typeof(DateTime) },
                { DbType.String, typeof(string) },
                { DbType.StringFixedLength, typeof(string) },
                { DbType.AnsiString, typeof(string) },
                { DbType.AnsiStringFixedLength, typeof(string) },
                { DbType.UInt16, typeof(UInt16) },
                { DbType.Int16, typeof(Int16) },
                { DbType.SByte, typeof(byte) },
                { DbType.Object, typeof(object) },
                { DbType.VarNumeric, typeof(decimal) },
                { DbType.Decimal, typeof(decimal) },
                { DbType.Currency, typeof(double) },
                { DbType.Binary, typeof(byte[]) },
                { DbType.Double, typeof(Double) },
                { DbType.Guid, typeof(Guid) },
                { DbType.Boolean, typeof(bool) }
            };
            return typeMap.ContainsKey(dbt) ? typeMap[dbt] : typeof(DBNull);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToFirstLower(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToLower() + input.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToFirstUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;
        }

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