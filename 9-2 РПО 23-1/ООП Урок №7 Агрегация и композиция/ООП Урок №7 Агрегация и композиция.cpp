#include <iostream>
#include <string.h>

using namespace std;

class Human
{
private:
    class Organ;

    class Tkan
    {
        Organ* org;
    };

    class Organ
    {
        Tkan muscle;
    public:
        int static static_member;
        Organ(int value = 0);
    };
public:
    Organ* liver;
};
Human::Organ::Organ(int val) {};
int Human::Organ::static_member = 100500;

class Point
{
protected:
    int x;
    int y;
public:
    Point()
    {
        x = 0;
        y = 0;
    }
    int& GetX()
    {
        return x;
    }
    int& GetY()
    {
        return y;
    }
};

class Window : public Point
{
    int width;
    int height;
public:
    Window(int width, int height)
    {
        this->width = width;
        this->height = height;
    }
    int& GetWidth()
    {
        return width;
    }
    int& GetHeight()
    {
        return height;
    }
    void MoveX(int dx)
    {
        x += dx;
    }
    void MoveY(int dy)
    {
        y += dy;
    }
    void Show()
    {
        cout << "------------------" << endl;
        cout << "x\t=\t" << x << endl;
        cout << "y\t=\t" << y << endl;
        cout << "width\t=\t" << width << endl;
        cout << "height\t=\t" << height << endl;
        cout << "------------------" << endl << endl;
    }
};

class Roga
{
protected:
    char color[25];
    int weight;
public:
    Roga()
    {
        strcpy_s(color, "Dirty");
        weight = 20;
    }
    Roga(char *color, int weight)
    {
        strcpy_s(this->color, color);
        this->weight = weight;
    }
};
class Kopyta
{
protected:
    char forma[25];
    int size;
public:
    Kopyta()
    {
        strcpy_s(forma, "Big");
        size = 10;
    }
    Kopyta(char *forma, int size)
    {
        strcpy_s(this->forma, forma);
        this->size = size;
    }
};
class Elk : public Roga, public Kopyta
{
public:
    char name[255];
    Elk()
    {
        strcpy_s(name, "unnamed");
    }
    Elk(const char *name)
    {
        strcpy_s(this->name, name);
    }
    void Show()
    {
        cout << "Имя лося: " << name << endl;
        cout << "Цвет рогов: " << color << endl;
        cout << "Вес рогов: " << weight << endl;
        cout << "Форма копыт: " << forma << endl;
        cout << "Размер копыт: " << size << endl;
    }
};
class Mother {};
class Father {};
class Child : public Mother, public Father
{
    // задание №1
    // от мамы: цвет глаз и длину волос
    // от папы: размер ноги, рост
    // вывести значения полей объектов: мама, папа, ребёнок (имя обязательно)
};
    // задание №2
    // используя множественное наследование разработать класс согласно варианта:
    // 
    // Александрова - окружность, вписанная в квадрат
    // Антонов      - окружность, вписанная в равносторонний треугольник
    // Духина       - окружность, вписанная в ромб
    // Землянский   - 
    // Золин        - окружность, вписанная в равнобедренный треугольник
    // Красицкий    - окружность, вписанная в прямоугольный треугольник
    // Кубанов      - прямоугольный треугольник, вписанный в окружность
    // Лушников     - равнобедренный треугольник, вписанный в окружность
    // Мамонтова    -
    // Метелицин    - ромб, вписанный в окружность
    // Назарян      - равносторонний треугольник, вписанный в оружность
    // Чавычалов    - квадрат, вписанный в окружность
    // Юнусов       -

template <class T>
class Pair
{
    T a;
    T b;
public:
    Pair(T t1, T t2) {}
};
template <class T>
Pair <T>::Pair(T t1, T t2) : a{ t1 }, b{ t2 };

template <class T>
class Trio : public Pair <T>
{
    T c;
public:
    Trio(T t1, T t2, T t3) {}
};
template <class T>
Trio <T>::Trio(T t1, T t2, T t3) : Pair <T>(t1, t2), c{ t3 };

int main()
{
    setlocale(LC_ALL, "");

    Elk los1("Ivan");
    los1.Show();

    // создать иерархию классов объемлющий-вложенный с демонстрацией, согласно варианта:
    // 
    // Александрова - теплица-растение
    // Антонов      - комната-мебель
    // Духина       - библиотека-книга
    // Землянский   - 
    // Золин        - компьютер-комплектующая
    // Красицкий    - казино-игра
    // Кубанов      - зимняя одежда-наполнитель
    // Лушников     - музей-картина
    // Мамонтова    - ресторан-блюдо
    // Метелицин    - машина-двигатель
    // Назарян      - автомат-товар
    // Чавычалов    - монитор-матрица
    // Юнусов       - 
    //

    // 
    // Александрова - public    - public
    // Антонов      - protected - public
    // Духина       - private   - public
    // Землянский   - public    - protected
    // Золин        - protected - protected
    // Красицкий    - private   - protected
    // Кубанов      - public    - private
    // Лушников     - protected - private
    // Мамонтова    - private   - private
    // Метелицин    - protected - public
    // Назарян      - private   - public
    // Чавычалов    - private   - private
    // Юнусов       - protected - private
    //

    //Window myWindow(10, 10);
    //myWindow.Show();
    //myWindow.GetX() = 5;
    //myWindow.GetY() = 7;
    //myWindow.GetWidth() = 40;
    //myWindow.GetHeight() = 50;
    //myWindow.Show();
    //myWindow.MoveX(20);
    //myWindow.MoveY(70);
    //myWindow.Show();



    return 0;
}
