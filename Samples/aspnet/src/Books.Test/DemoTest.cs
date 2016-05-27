using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Books.Test
{
    [TestClass]
    public class DemoTest
    {
        [TestMethod]
        public void SomeDemoTest()
        {
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        [Ignore]
        public void IgnoredTest()
        {
            throw new NotImplementedException();
        }
    }
}
