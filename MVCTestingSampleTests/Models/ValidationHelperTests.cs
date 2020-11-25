using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCTestingSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCTestingSample.Models.Tests
{
    [TestClass()]
    public class ValidationHelperTests
    {
        [TestMethod()]
        [DataRow("9.99")]
        [DataRow("$20.00")] ///works with US currency only
        [DataRow(".99")]
        [DataRow("0")]
        [DataRow("100000000")]
        public void IsValidPriceTest_ValidPrice_ReturnsTrue(string input)
        {
            bool result = ValidationHelper.IsValidPrice(input);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("$5.00.01")]

        public void IsValidPriceTest_ValidPrice_ReturnsFalse(string input)
        {
            bool result = ValidationHelper.IsValidPrice(input);
            Assert.IsFalse(result);
        }
    }
}