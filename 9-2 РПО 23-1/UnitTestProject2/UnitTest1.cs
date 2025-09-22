using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        Human human = new Tests.Human();
        [TestMethod]
        public void SetName_Albert_AlbertReturnTest()
        {
            human.SetName("Albert");
            string expected = "Albert";
            string actual = human.GetName();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetName_null_Ivan_BezimanniReturnTest()
        {
            human.SetName(null);
            string expected = "Ivan Bezimanni";
            string actual = human.GetName();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetName_EmptyString_Ivan_BezimanniReturnTest()
        {
            human.SetName("");
            string expected = "Ivan Bezimanni";
            string actual = human.GetName();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetAge_14_14ReturnTest()
        {
            human.SetAge(14);
            int expected = 14;
            int actual = human.GetAge();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetAge_0_0ReturnTest()
        {
            human.SetAge(0);
            int expected = 0;
            int actual = human.GetAge();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetAge_Minus14_14ReturnTest()
        {
            human.SetAge(-14);
            int expected = 0;
            int actual = human.GetAge();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_Minus14000_22440ReturnTest()
        {
            human.SetSalary(-14000);
            double expected = Human.MROT;
            double actual = human.GetSalary();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_14000_22440ReturnTest()
        {
            human.SetSalary(14000);
            double expected = Human.MROT;
            double actual = human.GetSalary();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_30000_30000ReturnTest()
        {
            human.SetSalary(30000);
            double expected = 30000;
            double actual = human.GetSalary();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_0_22440ReturnTest()
        {
            human.SetSalary(0);
            double expected = 22440;
            double actual = human.GetSalary();
            Assert.AreEqual(expected, actual);
        }
    }
}
