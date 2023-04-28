using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ConvertCustom.Server
{
    public static class ModelType
    {
        public static void EntityMapper<Output, Input>(Output output, Input input) where Output : class where Input : class
        {
            Type outputType = typeof(Output);
            Type inputType = typeof(Input);
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

        public static Output EntityMapper<Output, Input>(Input input) where Output : class where Input : class
        {
            Output val = Activator.CreateInstance<Output>();
            EntityMapper(val, input);
            return val;
        }

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

        public static List<T> ParseList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Parse<T>(dr));
            }
            return list;
        }

        private static void SetField<V>(object tempT, PropertyInfo pi, V name, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                type = Nullable.GetUnderlyingType(pi.PropertyType);
            }
            if (string.IsNullOrEmpty(name.ToString()))
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
    }

}
