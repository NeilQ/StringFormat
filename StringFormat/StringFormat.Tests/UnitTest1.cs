using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringFormat.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("a,b,c", StringFormat.Format("{0},{1},{2}", "a", "b", "c"));
            Assert.AreEqual("a,b,null", StringFormat.Format("{0},{1},{2}", "a", "b"));
            Assert.AreEqual("a,b,c", StringFormat.Format("{0},{1},{2}", "a", "b", "c", "d"));
            Assert.AreEqual("{0},b,c", StringFormat.Format("{{0}},{1},{2}", "a", "b", "c"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string output;
            Assert.IsTrue(StringFormat.TryFormat("{0},{1},{2}", out output, "a", "b", "c"));
            Assert.AreEqual("a,b,c", output);
            Assert.IsTrue(StringFormat.TryFormat("{0},{1},{2}", out output, "a", "b"));
            Assert.AreEqual("a,b,null", output);
            Assert.IsTrue(StringFormat.TryFormat("{0},{1},{2}", out output, "a", "b", "c", "d"));
            Assert.AreEqual("a,b,c", output);
            Assert.IsTrue(StringFormat.TryFormat("{{0}},{1},{2}", out output, "a", "b", "c"));
            Assert.AreEqual("{0},b,c", output);

        }
    }
}
