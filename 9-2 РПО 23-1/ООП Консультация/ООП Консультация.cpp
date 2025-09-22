// ООП Консультация.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;
class MyClas {
public:
    int x;
    int y;
    MyClas():x(0),y(0){}
    MyClas(const MyClas& other)
    {
        x = other.x;
            y = other.y;

    }

};

class Human
{
public:
    //friend class Child;
    class Chuzhoi
    {
    public:
        int ves;
        int size;
    };
    int age;
public:
    string name;
protected:
    string family;
private:
    double wallet;
public:
    Human()
    {
        age = 10;
        name = "Альберт ";
        family = "Кубанов ";
        wallet = 10.0;
    }
    Human(string familyk, string namek, double walletk, int agek)
    {
        age = agek;
        name = namek;
        family = familyk;
        wallet = walletk;
    }
    ~Human()
    {

    }
    void print()
    {
        cout << "лет: " << age << "\nимя:  " << name << "\nфамилия:    " << family << "\nкошелёк: " << wallet;
    }    
    double GetWallet()
    {
        return wallet;
    }
    void SetWallet(double money)
    {
        wallet = money;
    }
    void print(int a)
    {
        cout << a;
    }
};
class Child : Human
{
    double wallet = 1500000.0;
    string family = "Петров";
    Child()
    {
    }
};

class Mom
{

};
class Dad
{

};
class Son :Mom, Dad
{

};
class Daugther :Mom, Dad
{

};

namespace Family
{
    class Mom
    {
    public:
        virtual void Show() = 0;
    };
    class Dad
    {
        virtual void Show() = 0;
    };

    class Son :Mom, Dad
    {
        void Show()
        {

        }
    };
    class Daugther :Mom, Dad
    {
        void Show()
        {

        }
    };
}
void globalFunc()
{
    cout << "Глобальная функция";
}

class Point
{
public:
    int x;
    int y;
    Point(int a, int b) : x{ a }, y{ b } {};

    Point operator +(Point p)
    {
        return Point(this->x + p.x, this->y + p.y);
    }
    Point operator -(Point p)
    {
        return Point(this->x - p.x, this->y - p.y);
    }
    // инкремент
    Point operator ++()
    {
        return Point(this->x + 1, this->y + 1);
    }
    // декремент
    Point operator --()
    {
        return Point(this->x - 1, this->y - 1);
    }
   
  Point():x (0), y(0){}
  Point(const Point& point )
  {
      x = point.x;
      y = point.y;
  }
};

void GlobalFunc(Point point) 
{
    cout << point.x << endl;
    cout << point.y << endl;

}

int main()
{
    setlocale(LC_ALL, "");

    const Point point2(5, 6);
   
    Point point{ 3, 4 };

    GlobalFunc(point);

    GlobalFunc(point2);

    //___Инициализация___
    // копирующая
    int i = 1;
    // прямая
    int j(2);
    // унифицированная
    int k{ 3 };

    Human dvsd;
    dvsd.print();
    Human Lushnikov;
    Lushnikov.name = "Oleg\n";
    
    Human oleg;
    cout << oleg.GetWallet() << endl;
    oleg.SetWallet(233.45);
    cout <<oleg.GetWallet() << endl;

    globalFunc();
    Human::Chuzhoi Ben;
    Ben.ves = 500;

    oleg.print();
    oleg.print(1123);    

    const int fruct = 10;
    // создаём временную переменную
    int car;
    // снимаем модификатор const
    car = const_cast<int&>(fruct);
    // изменяем объект
    car = fruct + 10;

    cout << endl << endl;
    cout << fruct << endl;
    cout << car << endl;

    /*const_cast<;
    dynamic_cast;
    reinterpret_cast;
    static_cast;*/

    int a;
    int b;
    cout << "Введите первое число: ";
    cin >> a;
    cout << "Введите второе число: ";
    cin >> b;
    cout << endl;

    try
    {
        if (b != 0)
        {
            cout << "a / b = " << a / b << endl;
        }
        else
        {
            throw b;
        }
    }
    catch (int ex)
    {
        cout << "Нельзя делить на " << b << endl;
    }

}
