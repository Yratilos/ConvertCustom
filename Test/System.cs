using System.Reflection;
using System.Xml;

namespace Test
{
    /// <summary>
    /// 系统
    /// </summary>
    public class System
    {
        /// <summary>
        /// 系统L
        /// </summary>
        [MySpecial("Centos", 3.1)]
        public string? Linux { get; set; }
        /// <summary>
        /// 系统W
        /// </summary>
        [MySpecial("Windows11", 11.4)]
        public string? Windows { get; set; }
        /// <summary>
        /// 数字
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// 布尔
        /// </summary>
        public bool? Isolated { get; set; }
        /// <summary>
        /// 枚举
        /// </summary>
        public SystemEnum SystemEnum { get; set; }
        /// <summary>
        /// 特性-获取特性信息,类信息
        /// </summary>
        public static void TestAttribute()
        {
            Type type = typeof(System);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                var propertyName = pi.Name;
                var displayName = pi.GetCustomAttribute<MySpecialAttribute>()?.DisplayName;
                var displayWidth = pi.GetCustomAttribute<MySpecialAttribute>()?.DisplayVersion;
                Console.WriteLine("属性名称：" + propertyName + "；显示名称：" + displayName + "；显示版本：" + displayWidth);
            }
        }

        /// <summary>
        /// 获取对象信息
        /// </summary>
        public static void TestMapSystem<T>(T obj) where T : class
        {
            foreach (var item in obj.GetType().GetProperties())
            {
                var value = item.GetValue(obj);
                var msg = $"{item.Name}({item.PropertyType}):{value}";
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// 字典转实体类
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <param name="model"></param>
        public static void MapDictionaryToEntity<V, T>(Dictionary<string, V> dic, out T model) where T : class
        {
            // 判断泛型类型
            Type typeT = typeof(T);
            // 创建实例
            object tempT = Activator.CreateInstance(typeT)!;
            // 循环字段
            foreach (var pi in typeT.GetProperties())
            {
                // 字典中是否存在
                if (dic.ContainsKey(pi.Name))
                {
                    var type = pi.PropertyType;
                    var name = dic[pi.Name];
                    // 是否为泛型，泛型是否可为null
                    if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        // 返回可为null的基础类型参数
                        type = Nullable.GetUnderlyingType(pi.PropertyType)!;
                    }
                    // 类型转换
                    if (string.IsNullOrEmpty(name?.ToString()))
                    {
                        // 为空则创建对应类型的空值
                        pi.SetValue(tempT, Activator.CreateInstance(type)!, null);
                    }
                    else if (type.IsEnum)
                    {
                        // 转换对应枚举类型
                        pi.SetValue(tempT, Enum.Parse(type, name!.ToString()!, false), null);
                    }
                    else
                    {
                        // 对应正常字段赋值
                        pi.SetValue(tempT, Convert.ChangeType(name, type), null);
                    }
                }
            }
            model = (T)tempT;
        }

        /// <summary>
        /// 获取某类中字段的summary注释
        /// </summary>
        public static void GetSummary<T>()
        {
            var doc = new XmlDocument();
            doc.Load("TestXMLFile.xml");
            var currNode = doc.GetElementsByTagName("member");
            foreach (XmlNode node in currNode)
            {
                var type = typeof(T).FullName is null ? "" : $"P:{typeof(T).FullName}";
                if (node?.Attributes?["name"]?.Value.Contains(type) ?? false)
                {
                    Console.WriteLine(node.InnerText);
                    Console.WriteLine(node?.Attributes?["name"]?.Value);
                    Console.WriteLine(typeof(T).FullName);
                }
            }
        }


    }
}