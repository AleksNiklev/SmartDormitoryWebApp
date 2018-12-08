using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Services.Extensions;

namespace SmartDormitory.Tests.Services.ExtensionsTests
{
    [TestClass]
    public class Helpers_GetNumbersFromString_Should
    {
        [TestMethod]
        [DataRow("asd 43 asd 43 asd")]
        [DataRow("asd +43 asd -43 asd")]
        [DataRow("asd 43.1 asd 43.1 asd")]
        public void Return_ListOfNumbers(string text)
        {
            var result = Helpers.GetNumbersFromString(text);

            Assert.AreEqual(2, result.Length);
        }
    }
}