using System;

namespace РПМ_Урок__15_Перегрузка_операторов
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point(5, 7);
            Point p2 = new Point(3, 4);
            Point p3 = new Point();

            Console.WriteLine("Первая точка:");
            Console.WriteLine(p1);
            if (p1)
            {
                Console.WriteLine("Точка\n" + p1 + " находится не в начале координат.\n");
            }
            else
            {
                Console.WriteLine("Точка\n" + p1 + " находится в начале координат.\n");
            }
            Console.WriteLine();
            Console.WriteLine("Вторая точка:");
            Console.WriteLine(p2);
            if (p2)
            {
                Console.WriteLine("Точка\n" + p2 + " находится не в начале координат.\n");
            }
            else
            {
                Console.WriteLine("Точка\n" + p2 + " находится в начале координат.\n");
            }

            if (p3)
            {
                Console.WriteLine("Точка\n" + p3 + " находится не в начале координат.\n");
            }
            else
            {
                Console.WriteLine("Точка\n" + p3 + " находится в начале координат.\n");
            }

            if (p1 == p2)
            {
                Console.WriteLine("\np1 = p2");
            }
            else
            {
                Console.WriteLine("\np1 != p2");
            }

            if (p1 >= p2)
            {
                Console.WriteLine("\np1 >= p2");
            }
            else
            {
                Console.WriteLine("\np1 < p2");
            }

            Console.WriteLine("\nИнкремент точки: ");
            Console.WriteLine(++p3);
            Console.WriteLine("\nСмена знака: ");
            Console.WriteLine(-p3);
            Console.WriteLine("\nДекремент точки:");
            Console.WriteLine(--p3);

            Console.WriteLine("\nСумма двух точек: ");
            p3 = p1 + p2;
            Console.WriteLine(p3);
            Console.WriteLine("\nРазность двух точек: ");
            p3 = p1 - p2;
            Console.WriteLine(p3);
            Console.WriteLine("\nПроизведение двух точек: ");
            p3 = p1 * p2;
            Console.WriteLine(p3);
            Console.WriteLine("\nРазность двух точек: ");
            p3 = p1 / p2;
            Console.WriteLine(p3);
            Console.WriteLine("\nРазность по модулю двух точек: ");
            p3 = p1 % p2;
            Console.WriteLine(p3);


        }
    }

    public class Point
    {
        public int x;
        public int y;

        public Point()
        {
            this.x = 0;
            this.y = 0;
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator  -(Point a)
        {
            a.x = -a.x;
            a.y = -a.y;
            return a;
        }
        public static Point operator ++(Point p)
        {
            p.x++;
            p.y++;
            return p;
        }        
        public static Point operator --(Point p)
        {
            p.x--;
            p.y--;
            return p;
        }

        public static Point operator +(Point p1, Point p2)
        {
            Point result = new Point(p1.x + p2.x, p1.y + p2.y);
            return result;
        }
        public static Point operator +(Point point, int number)
        {
            point.x += number;
            point.y += number;
            return point;
        }
        public static Point operator -(Point a, Point b)
        {
            Point result = new Point(a.x - b.x, a.y - b.y);
            return result;
        }
        public static Point operator *(Point a, Point b)
        {
            Point result = new Point(a.x * b.x, a.y * b.y);
            return result;
        }
        public static Point operator /(Point a, Point b)
        {
            Point result = new Point(a.x / b.x, a.y / b.y);
            return result;
        }
        public static Point operator %(Point a, Point b)
        {
            Point result = new Point(a.x % b.x, a.y % b.y);
            return result;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public static bool operator  >(Point a, Point b)
        {
            return a.x > b.x && a.y > b.y;
        }
        public static bool operator  <(Point a, Point b)
        {
            return a.x < b.x && a.y < b.y;
        }
        public static bool operator >=(Point a, Point b)
        {
            return a.x >= b.x && a.y >= b.y;
        }
        public static bool operator <=(Point a, Point b)
        {
            return a.x <= b.x && a.y <= b.y;
        }

        public static bool operator  true(Point point)
        {
            return point.x != 0 && point.y != 0 ? true : false;
        }
        public static bool operator false(Point point)
        {
            return point.x == 0 && point.y == 0 ? true : false;
        }

        public static Point operator &(Point a, Point b)
        {
            if ((a.x != 0 && b.x != 0) && (b.x != 0 && b.y != 0))
                return b;
            return new Point();
        }
        public static Point operator |(Point a, Point b)
        {
            if ((a.x != 0 || a.y != 0) || (b.x != 0 || b.y != 0))
                return b;
            return new Point();
        }

        public override string ToString()
        {
            return "Point x: " + this.x + "\nPoint y: " + this.y;
        }
        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
