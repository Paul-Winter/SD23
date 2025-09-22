using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using РПМ_Урок__41_44_Дефекты_кода;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestForRefactor
    {
        [TestMethod]
        public void EmptyGroup_AddStudent_OneMoreStudentInGroup()
        {
            StudentGroup group = new StudentGroup();
            Student student = new Student();
            int actual = 1;

            // Act
            group.AddStudent(student);
            int expected = group.Group.Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testArenda_getMovie_info_Komedija_return_8_point_5()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.getMovie("Комедия");
    
            double expected = 8.5;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_getMovie_info_Drama_return_5_point_9()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.getMovie("Драма");

            double expected = 5.9;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_getMovie_info_Triller_return_11()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.getMovie("Триллер");

            double expected = 11;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_getMovie_info_empty_string_return_0()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.getMovie("");

            double expected = 0;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_GiveMovie_day_0_return_5()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.GiveMovie(0);

            double expected = 5;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_GiveMovie_day_1_return_4()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.GiveMovie(1);

            double expected = 4;

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void testArenda_GiveMovie_day_2_return_3()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.GiveMovie(2);

            double expected = 3;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_GiveMovie_day_3_return_2()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.GiveMovie(3);

            double expected = 2;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_GiveMovie_day_4_return_0()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.GiveMovie(4);

            double expected = 0;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void testArenda_GiveMovie_day_minus_1_return_0()
        {

            Arenda arenda = new Arenda();

            double actual = arenda.GiveMovie(-1);

            double expected = 0;

            Assert.AreEqual(expected, actual);
        }



    }
}
