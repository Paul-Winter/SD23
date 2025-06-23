using AppTestLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestLibrary;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        Human human = new Human();
        [TestMethod]
        public void SetName_Albert_AlbertReturnTest()
        {
            human.LibSetName("Albert");
            string expected = "Albert";
            string actual = human.LibGetName();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetName_null_Ivan_BezimanniReturnTest()
        {
            human.LibSetName(null);
            string expected = "Ivan Bezimanni";
            string actual = human.LibGetName();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetName_EmptyString_Ivan_BezimanniReturnTest()
        {
            human.LibSetName("");
            string expected = "Ivan Bezimanni";
            string actual = human.LibGetName();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetAge_14_14ReturnTest()
        {
            human.LibSetAge(14);
            int expected = 14;
            int actual = human.LibGetAge();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetAge_0_0ReturnTest()
        {
            human.LibSetAge(0);
            int expected = 0;
            int actual = human.LibGetAge();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetAge_Minus14_14ReturnTest()
        {
            human.LibSetAge(-14);
            int expected = 0;
            int actual = human.LibGetAge();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_Minus14000_22440ReturnTest()
        {
            human.LibSetSalary(-14000);
            double expected = Class1.MROT;
            double actual = human.LibGetSalary();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_14000_22440ReturnTest()
        {
            human.LibSetSalary(14000);
            double expected = Class1.MROT;
            double actual = human.LibGetSalary();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_30000_30000ReturnTest()
        {
            human.LibSetSalary(30000);
            double expected = 30000;
            double actual = human.LibGetSalary();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSalary_0_22440ReturnTest()
        {
            human.LibSetSalary(0);
            double expected = 22440;
            double actual = human.LibGetSalary();
            Assert.AreEqual(expected, actual);
        }
    }
}
