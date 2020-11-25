using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCTestingSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCTestingSampleTests.Model
{
    [TestClass]
    
    public class ProductTests
    {
        [TestMethod]
        public void Name_SetToNull_ThrowsArguementNullException()
        {
            Product p = new Product();
            Assert.ThrowsException<ArgumentNullException>
                (
                    () => p.Name = null
                );
        }
    }
}
