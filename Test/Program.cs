using ConvertCustom;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Test
{
    internal class Program
    {
        static void Main()
        {
            var n = 1;
            var m = n.TryBoolean();
            Console.WriteLine(m);
            //var n = new Program();
            //var m = n[0];
            //Console.WriteLine(m);
            //ObjectCache cache = MemoryCache.Default;
            //cache["1"] = 1;
            //Console.WriteLine(string.Join(",", array.Where(s=>s.n=="5"))=="");
        }
        class nn
        {
            public string n { get; set; }
            public string m { get; set; }
        }
        static List<nn> array = new List<nn>() { new nn { n="1",m="2"}, new nn { n = "3", m = "4" } };
        //public string this[int param]
        //{
        //    get { return array[param]; }
        //    set { array[param] = value; }
        //}

        /// <summary>
        /// 枚举-循环所有枚举
        /// </summary>
        static public void TestEnum()
        {
            foreach (var item in Enum.GetNames(typeof(SystemEnum)))
            {
                Console.WriteLine(item);
            }
        }

    }
}