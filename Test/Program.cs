namespace Test
{
    internal class Program
    {
        static void Main()
        {
            TestEnum();
            //System.TestAttribute();
            //System.TestMapSystem(new System() { Linux = "Centos", Windows = "Win11" });
            //System.TestMapSystem(new System() { Linux = null, Windows = "" });
            //System.MapDictionaryToEntity(new Dictionary<string, string>
            //{
            //    {"Linux","Centos" },
            //    {"Windows","Win11" },
            //    {"Port","4" },
            //    {"SystemEnum","Windows" },
            //}, out System newObj);
            //Console.WriteLine(newObj);
            //System.GetSummary<System>();
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