using ConvertCustom.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace ConvertCustom.Services.Tests
{
    [TestClass()]
    public class DataTableTypeTests
    {
        [TestMethod()]
        [DataRow("field1,field2", "value1,value2", "value3,value4")]
        [DataRow("field1,field2", "value1,value2", "value3,")]
        [DataRow("field3,field4", ",value2", "value3,")]
        [DataRow("field5,field6", ",", ",")]
        public void ParseTest(params string[] values)
        {
            var column1 = values[0].Split(',')[0];
            var column2 = values[0].Split(',')[1];
            var dics = new List<Dictionary<string, string>> {
                new Dictionary<string, string>
                {
                    {column1,values[1].Split(',')[0] },
                    {column2,values[1].Split(',')[1] },
                },
                new Dictionary<string, string>
                {
                    {column1,values[2].Split(',')[0] },
                    {column2,values[2].Split(',')[1] },
                },
            };
            var dt = DataTableType.Parse(dics);
            Assert.AreEqual(dt.Rows[0][column1], values[1].Split(',')[0]);
            Assert.AreEqual(dt.Rows[0][column2], values[1].Split(',')[1]);
            Assert.AreEqual(dt.Rows[1][column1], values[2].Split(',')[0]);
            Assert.AreEqual(dt.Rows[1][column2], values[2].Split(',')[1]);
        }
    }
}