using System;

namespace UnitTestingClassLibrary
{
    public class Geometry
    {
        // метод вычисляющий площадь прямоугольника
        public int RectangleArea(int a, int b)
        {
            return a * b;
        }

        public double RectangleArea(double a, double b)
        {
            return a * b;
        }

        // метод вычисляющий площадь круга
        public double CircleArea(int a)
        {
            return Math.PI * a * a;
        }

        public double CircleArea(double a)
        {
            return Math.PI * a * a;
        }

        // метод вычисляющий объём цилиндра
        public double VolumeCylinder(int radius, int h)
        {
            return CircleArea(radius) * h;
        }

        public double VolumeCylinder(double radius, int h)
        {
            return CircleArea(radius) * h;
        }

        public double VolumeCylinder(double radius, double h)
        {
            return CircleArea(radius) * h;
        }
    }
}
