using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ConvertCustom.Custom
{
    /// <summary>
    /// 转换实体
    /// </summary>
    public static class ModelType
    {
        /// <summary>
        /// 可转枚举
        /// </summary>
        /// <typeparam name="K">notnull</typeparam>
        public static T Parse<K, V, T>(Dictionary<K, V> dic) where T : class
        {
            Type typeFromHandle = typeof(T);
            object tempT = Activator.CreateInstance(typeFromHandle);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (dic.TryGetValue((K)(object)pi.Name, out var name))
                {
                    Type type = pi.PropertyType;
                    SetField(tempT, pi, name, type);
                }
            }
            return (T)tempT;
        }

        /// <summary>
        /// 可转枚举
        /// </summary>
        public static T Parse<T>(DataRow dr)
        {
            Type typeFromHandle = typeof(T);
            object tempT = Activator.CreateInstance(typeFromHandle);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                Type type = pi.PropertyType;
                if (dr.Table.Columns.Contains(pi.Name))
                {
                    object name = dr[pi.Name];
                    SetField(tempT, pi, name, type);
                }
            }
            return (T)tempT;
        }

        /// <summary>
        /// 可转枚举
        /// </summary>
        public static List<T> ParseList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Parse<T>(dr));
            }
            return list;
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        private static void SetField<V>(object tempT, PropertyInfo pi, V name, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                type = Nullable.GetUnderlyingType(pi.PropertyType);
            }
            if (name == null)
            {
                pi.SetValue(tempT, Activator.CreateInstance(type), null);
            }
            else if (type.IsEnum)
            {
                pi.SetValue(tempT, Enum.Parse(type, name.ToString(), ignoreCase: false), null);
            }
            else
            {
                pi.SetValue(tempT, Convert.ChangeType(name, type), null);
            }
        }

        /// <summary>
        /// 实体映射
        /// 只映射与Input相同的属性和字段
        /// </summary>
        /// <param name="output">输出</param>
        /// <param name="input">输入</param>
        public static void EntityMapper<Output, Input>(Output output, Input input) where Output : class where Input : class
        {
            Type outputType = typeof(Output);
            Type inputType = typeof(Input);
            // 属性
            PropertyInfo[] properties = outputType.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                PropertyInfo propInfo = inputType.GetProperty(prop.Name);
                if (propInfo != null)
                {
                    object inValue = propInfo.GetValue(input);
                    prop.SetValue(output, inValue);
                }
            }
            // 字段
            FieldInfo[] fields = outputType.GetFields();
            foreach (FieldInfo field in fields)
            {
                FieldInfo fieldInfo = inputType.GetField(field.Name);
                if (fieldInfo != null)
                {
                    object inValue2 = fieldInfo.GetValue(input);
                    field.SetValue(output, inValue2);
                }
            }
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        public static Output EntityMapper<Output, Input>(Input input) where Output : class where Input : class
        {
            Output val = Activator.CreateInstance<Output>();
            EntityMapper(val, input);
            return val;
        }
    }
}