using ConvertCustom.IServices;
using ConvertCustom.Services;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;

namespace Test
{
    internal class Program
    {
        static void Main()
        {
            //var n = new Program();
            //var m = n[0];
            //Console.WriteLine(m);
            //ObjectCache cache = MemoryCache.Default;
            //cache["1"] = 1;
        }
        List<string> array = new List<string>() { "1", "2" };
        public string this[int param]
        {
            get { return array[param]; }
            set { array[param] = value; }
        }

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