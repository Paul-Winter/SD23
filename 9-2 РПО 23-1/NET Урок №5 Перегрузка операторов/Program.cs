using System;

namespace NET_Урок__5_Перегрузка_операторов
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point point1 = new Point();
            Point point2 = new Point(12, 15);

            Console.WriteLine(point1);
            Console.WriteLine(point2);

            point1++;
            Console.WriteLine(point1);

            Point point3 = new Point();
            point3 = point1 + point2;
            Console.WriteLine(point3);
        }
    }

    class Point
    {
        private int x;
        private int y;

        public int X
        {
            // аксессор
            get
            {
                //return x != null ? x : new Int32();
                return x;
            }
            // мутатор
            set
            {
                x = value;
            }
        }
        public int Y
        {
            // геттер
            get => y;
            // сеттер
            set => y = value;
        }

        // конструкторы
        public Point()
        {
            X = 0;
            Y = 0;
        }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // арифметические
        public static Point operator ++(Point p)
        {
            p.X++;
            p.Y++;
            return p;
        }
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        // логические
        public static bool operator true(Point p)
        {
            return p.X != 0 || p.Y != 0 ? true : false;
        }
        public static bool operator false(Point p)
        {
            return p.X == 0 || p.Y == 0 ? true : false;
        }

        public static Point operator | (Point p1, Point p2)
        {
            return new Point(p1.x | p2.y, p1.y | p2.y);
        }
        public static Point operator & (Point p1, Point p2)
        {
            return new Point(p1.x & p2.x, p1.y & p2.y);
        }

        // отношения
        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }
        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }
        // преобразование в строку
        public override string ToString()
        {
            return $"x = {X}; y = {Y}";
        }
        // проверка равенства объектов
        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }
        // возврат хэша
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
    // классы: Point    (точка на плоскости: x, y)
    //         Point3D  (точка в пространстве: x, y, z)
    //         Vector   (отрезок на плоскости: Point_Begin, Point_End)
    //                Класс     Операторы
    // Александрова - Point     / и %
    // Антонов      - Point3D   + и *
    // Духина       - Vector    + и ++
    // Землянский   - Point     - и /
    // Золин        - Point3D   % и --
    // Красицкий    - Vector    * и ++
    // Кубанов      - Vector    / и --
    // Лушников     - Point3D   + и *
    // Мамонтова    - Point     - и *
    // Метелицин    - Vector    * и --
    // Чавычалов    - Point3D   - и /
    // Юнусов       - Point     * и --
}
