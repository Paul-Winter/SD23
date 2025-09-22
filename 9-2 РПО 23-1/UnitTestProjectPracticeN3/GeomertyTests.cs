using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestingClassLibrary;

namespace UnitTestProjectPracticeN3
{
    [TestClass]
    public class GeomertyTests
    {
        [TestMethod]
        public void RectangleArea_Int_3and5_15return()
        {
            //  Arrange (исходные данные)
            int a = 3;
            int b = 5;
            int expected = 15;

            //  Act     (вычисление значений)
            Geometry geometry = new Geometry();
            int actual = geometry.RectangleArea(a, b);

            //  Assert  (сравнение полученного и ожидаемого результатов)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RectangleArea_Double_3and5_15return()
        {
            // Arrange
            double a = 3.0d;
            double b = 5.0d;
            double expected = 15.0d;

            // Act
            Geometry geometry = new Geometry();
            double actual = geometry.RectangleArea(a, b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CircleArea_Int_10_314return()
        {
            // Arrange
            int radius = 10;
            double expected = Math.PI * radius * radius;

            // Act
            Geometry geometry = new Geometry();
            double actual = geometry.CircleArea(radius);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CircleArea_Double_10_314return()
        {
            // Arrange
            double radius = 10.0d;
            double expected = Math.PI * radius * radius;

            // Act
            Geometry geometry = new Geometry();
            double actual = geometry.CircleArea(radius);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VolumeCylinder_Int_3and5_15return()
        {
            // Arrange
            int radius = 10;
            int height = 10;
            double expected = Math.PI * radius * radius * height;

            // Act
            Geometry geometry = new Geometry();
            double actual = geometry.CircleArea(radius) * height;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VolumeCylinder_Double_3and5_15return()
        {
            // Arrange
            double radius = 10.0d;
            int height = 10;
            double expected = Math.PI * radius * radius * height;

            // Act
            Geometry geometry = new Geometry();
            double actual = geometry.CircleArea(radius) * height;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VolumeCylinder_Double_Int_3and5_15return()
        {
            // Arrange
            double radius = 10.0d;
            double height = 10.0d;
            double expected = Math.PI * radius * radius * height;

            // Act
            Geometry geometry = new Geometry();
            double actual = geometry.CircleArea(radius) * height;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
