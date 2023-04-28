using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConvertCustom.Tests
{
    [TestClass()]
    public class TypeConversionTests
    {
        [TestMethod()]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow(false, false)]
        [DataRow(true, true)]
        [DataRow("1900-01-01", null)]
        [DataRow("2020-01-01", null)]
        [DataRow(0, null)]
        [DataRow(0.5, null)]
        [DataRow(1, null)]
        [DataRow(-1, null)]
        public void ToBooleanTest(object input, bool? expected)
        {
            Assert.AreEqual(input.ToBoolean(), expected);
        }

        [TestMethod()]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow(false, null)]
        [DataRow(true, null)]
        [DataRow("1900-01-01", null)]
        [DataRow("2020-01-01", "2020-01-01")]
        [DataRow(0, null)]
        [DataRow(0.5, null)]
        [DataRow(1, null)]
        [DataRow(-1, null)]
        public void ToDateTimeTest(object input, object expected)
        {
            var msg = DateTime.TryParse(expected?.ToString(), out DateTime res);
            Assert.AreEqual(input.ToDateTime(), msg ? res : expected);
        }

        [TestMethod()]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow(false, null)]
        [DataRow(true, null)]
        [DataRow("1900-01-01", null)]
        [DataRow("2020-01-01", null)]
        [DataRow(0, 0)]
        [DataRow(0.5, 0.5)]
        [DataRow(1, 1)]
        [DataRow(-1, -1)]
        public void ToDecimalTest(object input, object expected)
        {
            var msg = decimal.TryParse(expected?.ToString(), out decimal res);
            Assert.AreEqual(input.ToDecimal(), msg ? res : expected);
        }

        [TestMethod()]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow(false, null)]
        [DataRow(true, null)]
        [DataRow("1900-01-01", null)]
        [DataRow("2020-01-01", null)]
        [DataRow(0, 0)]
        [DataRow(0.5, 0.5)]
        [DataRow(1, 1)]
        [DataRow(-1, -1)]
        public void ToDoubleTest(object input, object expected)
        {
            var msg = double.TryParse(expected?.ToString(), out double res);
            Assert.AreEqual(input.ToDouble(), msg ? res : expected);
        }

        [TestMethod()]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow(false, null)]
        [DataRow(true, null)]
        [DataRow("1900-01-01", null)]
        [DataRow("2020-01-01", null)]
        [DataRow(0, 0)]
        [DataRow(0.5, 0.5)]
        [DataRow(1, 1)]
        [DataRow(-1, -1)]
        public void ToFloatTest(object input, object expected)
        {
            var msg = float.TryParse(expected?.ToString(), out float res);
            Assert.AreEqual(input.ToFloat(), msg ? res : expected);
        }

        [TestMethod()]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow(false, null)]
        [DataRow(true, null)]
        [DataRow("1900-01-01", null)]
        [DataRow("2020-01-01", null)]
        [DataRow(0, 0)]
        [DataRow(0.5, null)]
        [DataRow(1, 1)]
        [DataRow(-1, -1)]
        public void ToIntTest(object input, object expected)
        {
            var msg = int.TryParse(expected?.ToString(), out int res);
            Assert.AreEqual(input.ToInt(), msg ? res : expected);
        }
    }
}