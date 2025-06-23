using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestingClassLibrary;


namespace UnitTestProjectPracticeN3
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void SumInt_3and5_8return()
        {
            // Arrange
            int x = 3;
            int y = 5;
            int expected = 8;
            Calculator calculator = new Calculator();

            // Act
            int actual = calculator.Sum(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SumDouble_3and5_8return()
        {
            // Arrange
            double x = 3.3d;
            double y = 5.5d;
            double expected = 8.8d;
            Calculator calculator = new Calculator();

            // Act
            double actual = calculator.Sum(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SumInt_2and2_not5return()
        {
            // Arrange
            int x = 2;
            int y = 2;
            int expected = 5;
            Calculator calculator = new Calculator();

            // Act
            int actual = calculator.Sum(x, y);

            // Assert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void SumDouble_2and2_not5return()
        {
            // Arrange
            double x = 2.2d;
            double y = 2.2d;
            double expected = 5.5d;
            Calculator calculator = new Calculator();

            // Act
            double actual = calculator.Sum(x, y);

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}
