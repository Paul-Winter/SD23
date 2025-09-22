// ООП Урок №3 Константные методы и перегрузка.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include "DynArray.h"

using namespace std;

// дата
class Date
{
private:
    int day;
    int month;
    int year;
    //const int currentYear;
public:
    //Date(int day, int month, int year) : day{day}, month{month}, year{year}, currentYear{2025}
    //{

    //}
    Date(int day, int month, int year) : day{day}, month{month}, year{year} {}

    explicit Date(int year) : Date(1, 1, year) {}

    Date() : Date(1, 1, 1970) {}

    int getDay()
    {
        return day;
    }
    int getDay() const
    {
        return day;
    }
    int getMonth() const;
    int getYear() const
    {
        return year;
    }
    void setYear(int year)
    {
        //year >= 1970 ? this->year = year : this->year = 1970;
        this->year = year;
    }
    void print() const
    {
        cout << this->getDay() << "/" << this->getMonth() << "/" << this->getYear() << endl;
    }
    friend void displayDate(Date date);
};
int Date::getMonth() const
{
    return month;
}
void displayDate(Date date)
{
    cout << date.day << "/" << date.month << "/" << date.year << endl;
}

// время
class Time
{

};

// дата и время
class DateTime
{

};

// временной интервал
class TimeSpan
{

};

void doSomething(long number)
{
}
float doSomething()
{
    return 10.0;
}

class Point
{
public:
    double x;
    double y;

    Point(double x, double y) : x {x}, y {y}
    {
    }

    void display() const
    {
        cout << "(" << x << "," << y << ")" << endl;
    }

    // функции арифметических операций
    static const Point add(const Point& point1, const Point& point2)
    {
        return Point(point1.x + point2.x, point1.y + point2.y);
    }
    static const Point sub(const Point& point1, const Point& point2)
    {
        return Point(point1.x - point2.x, point1.y - point2.y);
    }
    static const Point mul(const Point& point, double value)
    {
        return Point(point.x * value, point.y * value);
    }
    static const Point div(const Point& point, double value)
    {
        return Point(point.x / value, point.y / value);
    }

    // операторы арифметических операций
    friend const Point operator+(const Point& point1, const Point& point2)
    {
        return add(point1, point2);
    }
    friend const Point operator+(const Point& point, double value)
    {
        return Point(point.x + value, point.y + value);
    }
    friend const Point operator+(double value, const Point& point)
    {
        return Point(point.x + value, point.y + value);
    }
    friend const Point operator-(const Point& point1, const Point& point2)
    {
        return sub(point1, point2);
    }
    friend const Point operator*(const Point& point, double value)
    {
        return mul(point, value);
    }
    friend const Point operator/(const Point& point, double value)
    {
        return div(point, value);
    }

    // перегрузка операторов сравнения
    friend bool operator==(const Point& point1, const Point& point2)
    {
        return point1.x == point2.x && point1.y == point2.y;
    }
    friend bool operator!=(const Point& point1, const Point& point2)
    {
        return !(point1.x == point2.x && point1.y == point2.y);
    }
    friend bool operator>(const Point& point1, const Point& point2)
    {
        return point1.x > point2.x && point1.y > point2.y;
    }
    friend bool operator<(const Point& point1, const Point& point2)
    {
        return !(point1.x > point2.x && point1.y > point2.y);
    }

    // перегрузка операторов ввода-вывода
    friend ostream& operator<< (ostream& output, const Point& point)
    {
        output << "(" << point.x << "," << point.y << ")";
        return output;
    }
    friend istream& operator>> (istream& input, Point& point)
    {
        input >> point.x;
        input.ignore(1);
        input >> point.y;
        return input;
    }

    // перегрузка унарных операторов
    const Point operator-()
    {
        return Point(-x, -y);
    }
    // перегрузка префиксные инкремент и декремент
    Point& operator++()
    {
        ++x; ++y;
        return *this;
    }
    Point& operator--()
    {
        --x; --y;
        return *this;
    }
    // перегрузка постфиксные инкремент и декремент
    const Point operator++(int)
    {
        Point point{ x, y };
        ++(*this);
        return point;
    }
    const Point operator--(int)
    {
        Point point{ x, y };
        --(*this);
        return point;
    }

    // перегрузка через дружественные функции
    //friend Point operator+(const Point& point, int value)
    //{
    //    return Point(point.x + value, point.y + value);
    //}

    // перегрузка через методы класса
    //Point operator+(double value)
    //{
    //    return Point(this->x + value, this->y + value);
    //}
};

// перегрузка через обычные функции (глобальная)
//Point operator+(const Point& point1, const Point& point2)
//{
//    return Point(point1.x + point2.x, point1.y + point2.y);
//}

int main()
{
    setlocale(LC_ALL, "");

    cout << "__________________________________________________Константные_методы__________________________________________________" << endl << endl;
    //const int var = 12;
    //const char* h{ "hello world" };

    const Date today{ 18,02,2025 };
    today.print();

    //today.setYear(2000);
    //cout << "Today is " << today.getYear() << endl;

    cout << endl << endl;
    cout << "__________________________________________________explicit_конструктор__________________________________________________" << endl << endl;
    double d{ 10 };
    d = 5;
    doSomething(5);
    doSomething();

    displayDate((Date)2025);
    Date date = (Date)2007;
    displayDate(date);
    Date millenium{ 2000 };
    displayDate(millenium);

    cout << endl << endl;
    cout << "_________________________________________________Перегрузка_операторов_________________________________________________" << endl << endl;
    Point a{1,1};
    Point b{2,2};
    Point c = a + b;
    //Point c = (double)b + a;
    a.display();
    c.display();

    cout << endl << endl;
    cout << "_________________________________________________Пример_DynArray_________________________________________________" << endl << endl;
    DynArray arr1{ 5 };
    DynArray arr2;

    for (int i{ 0 }; i < 5; ++i)
    {
        arr1[i] = i;
    }
    for (int i{ 0 }; i < 10; ++i)
    {
        arr2[i] = i + 11;
    }
    
    cout << "Size of arr1: " << arr1.length() << endl;
    cout << "Array1: " << arr1 << endl;

    cout << "Size of arr2: " << arr2.length() << endl;
    cout << "Array2: " << arr2 << endl;

    cout << "arr1 == arr2 ? " << endl;
    if (arr1 == arr2)
    {
        cout << "arr1 == arr2" << endl;
    }
    else
    {
        cout << "arr1 != arr2" << endl;
    }

    cout << "arr3{arr1}" << endl;
    DynArray arr3{ arr1 };
    cout << "Size of arr3: " << arr3.length() << endl;
    cout << "Array3: " << arr3 << endl;

    cout << "arr1 = arr2" << endl;
    arr1 = arr2;
    cout << "Array1: " << arr1 << endl;
    cout << "Array2: " << arr2 << endl;

    cout << "arr3[5] = 1234" << endl;
    arr3[5] = 1234;

    return 0;
}
