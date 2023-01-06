using EnadlaInventory.Core.Helpers;

namespace EnadlaInventory.Core.Tests.Helpers
{
    [TestClass]
    public class TestHexConverter
    {
        [TestMethod]
        public void EmptyByteArray_ToHexString_return_empty_string()
        {
            byte[] bytes = new byte[0];

            string result = HexConverter.ToHexSring(bytes);

            string expectedResult = string.Empty;
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(new byte[] {0,0,0}, "000000")]
        [DataRow(new byte[] {255, 255, 255}, "FFFFFF")]
        [DataRow(new byte[] {1, 16, 98, 140, 230, 254}, "0110628CE6FE")]
        public void GivenByteArray_ToHexString_return_byteArrays_serialized_as_string_with_hex_format(byte[] bytes, string expected)
        {
            string result = HexConverter.ToHexSring(bytes);

            Assert.AreEqual(expected, result);
        }
    }
}
