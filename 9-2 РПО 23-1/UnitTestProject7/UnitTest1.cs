using chavychalov;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject7
{
    [TestClass]
    public class UnitTest1
    {
        Program program = new chavychalov.Program();
        [TestMethod]
        public void XplusY_return10()
        {
            long expected = 10;
            long actual = program.Sum1(5, 5);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void xumnojizy_return20()
        {
       
            long expected = 20;
            long actual = program.Mul(4, 5);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void xdelity_return10()
        {
            long expected = 10;
            long actual = program.Sub(100, 10);
            Assert.AreEqual(expected, actual);
        }
    }
}
