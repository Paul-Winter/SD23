// ООП Урок №2 Инициализация.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

class BadOrder
{
    int fieldOne;
    int fieldTwo;
public:
    BadOrder(int param) : fieldTwo{param}, fieldOne{fieldTwo + 10} {}
    void print()
    {
        cout << fieldOne << ":" << fieldTwo << endl;
    }
};

class GoodOrder
{
    int fieldOne;
    int fieldTwo;
public:
    GoodOrder() : fieldOne{0}, fieldTwo{0} {}
    GoodOrder(int param) : fieldOne{param + 10}, fieldTwo{param} {}
    void print()
    {
        cout << fieldOne << ":" << fieldTwo << endl;
    }
};

class Point
{
    int x;
    int y;
public:
    // конструктор по умолчанию
    Point() : x{0}, y{0} {}
    // конструктор с параметрами
    Point(int pX, int pY) : x{pX}, y{pY} {}
    // метод вывода в консоль
    void print()
    {
        cout << "Point " << x << ":" << y << endl;
    }
};

class Rectangle
{
    Point leftUpperCorner;
    int width;
    int height;
public:
    // конструктор по умолчанию
    Rectangle() : leftUpperCorner{0,0}, width{0}, height{0} {}
    // конструктор с параметрами
    Rectangle(int x, int y, int widthR, int heightR) : leftUpperCorner{x, y}, width{widthR}, height{heightR} {}
    // метод вывода в консоль
    void print()
    {
        cout << "Rectangle: leftUpperCorner ";
        leftUpperCorner.print();
        cout << "width: " << width << " height: " << height << endl;
    }
};

class Human
{
    char* name;
    uint16_t age;

    //  uint16_t - unsigned integer 16 bit type рекомендуемая современным стандартом нотация
    //  целочисленных типов, занимающая предсказуемое количество байт на любой архитектуре
    //  (аналог unsigned short)

    uint32_t socialId; // аналог unsigned int
public:
    Human(const char* nameH, uint16_t ageH, uint32_t socialIdH)
        : name{ nameH ? new char[strlen(nameH) + 1] : nullptr }, age{ ageH }, socialId{ socialIdH }
    {
        if (name)
        {
            strcpy_s(name, strlen(nameH) + 1, nameH);
        }
        cout << "Работа параметризованного конструктора имени и возраста и идентификатора" << endl;
    }
    Human() : Human{nullptr, 0, 0}
    {
        cout << "Работа конструктора по умолчанию" << endl;
    }
    Human(const char* nameH) : Human{ nameH, 0, 0 }
    {
        cout << "Работа параметризованного конструктора имени" << endl;
    }
    Human(const char* nameH, uint16_t ageH) : Human{nameH, ageH, 0}
    {
        cout << "Работа параметризованного конструктора имени и возраста" << endl;
    }
    ~Human()
    {
        delete[] name;
        cout << "Работа деструктора класса" << endl;
    }
    void print()
    {
        if (name)
        {
            cout << "Name: " << name << "\nAge: " << age << "\nSocialId: " << socialId << endl;
        }
        else
        {
            cout << "empty human" << endl;
        }
    }
};

class DemoStatic
{
public:
    int personal;
    static int common;
    DemoStatic(int personal)
    {
        this->personal = personal;
    }
    static void print()
    {
        cout << "common = " << DemoStatic::common << endl;
    }
};
int DemoStatic::common{ 0 };

class Date
{
    int day;
    int month;
    int year;
    //int hours;
    //int minutes;
    //int seconds;

public:
    Date(int dayD, int monthD, int yearD) : day{ dayD }, month{ monthD }, year{ yearD }
    {
        cout << "Работа параметризованного конструктора: " << this << endl;
    }

    Date() : Date(1,1,1970)
    {
        cout << "Работа конструктора по умолчанию: " << this << endl;
    }

    ~Date()
    {
        cout << "Работа деструктора: " << this << endl;
    }

    Date& setDay(int day)
    {
        this->day = day;
        return *this;
    }

    Date& setMonth(int month)
    {
        this->month = month;
        return *this;
    }

    Date& setYear(int year)
    {
        this->year = year;
        return *this;
    }

    void print()
    {
        cout << this->day << "." << this->month << "." << this->year << endl;
        //"||" << this->hours << ":" << this->minutes << ":" << this->seconds << endl;
    }
};

class Fraction
{
    int numerator;
    int denominator;

public:
    Fraction(int num, int denom) : numerator{num}, denominator{denom}
    {
        cout << "Конструктор дроби параметризованный " << this << endl;
    }
    Fraction() : Fraction(1,1)
    {
        cout << "Конструктор дроби по умолчанию " << this << endl;
    }
    ~Fraction()
    {
        cout << "Fraction destructed for " << this << endl;
    }
    Fraction(const Fraction& fract) : numerator{fract.numerator}, denominator{fract.denominator}
    {
        cout << "Fraction copy constructed " << this << endl;
    }

    void print()
    {
        cout << "(" << numerator << "/" << denominator << ")" << endl;
    }
};

class DynArray
{
    int* arr;
    int size;
public:
    DynArray(int sizeP) : arr{ new int[sizeP] {} }, size{sizeP}
    {
        cout << "DynArr constructed for " << size << " elements, for " << this << endl;
    }
    DynArray() : DynArray(5) {}
    DynArray(const DynArray& object) : arr{ new int[object.size]}, size{object.size}
    {
        for (int i{ 0 }; i < size; ++i)
        {
            arr[i] = object.arr[i];
        }
        cout << "DynArr constructed for " << size << " elements, for " << this << endl;
    }
    
    int getElem(int index)
    {
        return arr[index];
    }

    void setElem(int index, int value)
    {
        arr[index] = value;
    }

    void print();
    void randomize();

    ~DynArray()
    {
        cout << "Очистка памяти объекта DynArray для " << arr << " pointer" << endl;
        delete[] arr;
        cout << "DynArr destructed for " << size << " elements, for " << this << endl;
    }
};

void DynArray::print()
{
    for (int i{ 0 }; i < size; ++i)
    {
        cout << arr[i] << " ";
    }
    cout << endl;
}

void DynArray::randomize()
{
    for (int i{ 0 }; i < size; ++i)
    {
        arr[i] = rand() % 10;
    }
}

class Pryamougol
{
    int width;
    int height;

public:
    Pryamougol(int widthP, int heightP) : width{ widthP }, height{ heightP }
    {
        cout << "Конструктор параметризованный работает!" << endl;
    }

    Pryamougol() : Pryamougol(1, 1)
    {
        cout << "Конструктор по умолчанию работает!" << endl;
    }

    int getWidth()
    {
        return width;
    }

    void setWidth(int value)
    {
        width = value;
    }

    void print()
    {
        cout << width << " " << height << endl;
    }
};

class Student
{
    int id;
    int* ocenki;
public:
    Student(int idD, int size) : id{ idD }, ocenki{ new int [size] {} }
    {
        cout << "Параметризованный работает!" << endl;
    }

    Student() : Student{0, 5}
    {
        cout << "По умолчанию работает!" << endl;
    }

    Student(const Student& student) : id{ student.id }, ocenki{ new int [5] }
    {
        
        for (int i{ 0 }; i < 5; ++i)
        {
            ocenki[i] = student.ocenki[i];
        }
        cout << "Student constructed for " << " elements, for " << this << endl;
        cout << "Копирование работает!" << endl;
    }

    int getId()
    {
        return id;
    }

    void setId(int neg)
    {
        id = neg;
    }

    int getOcenki(int index)
    {
        return ocenki[index];
    }

    void setOcenki(int index, int value)
    {
        ocenki[index] = value;
    }

    ~Student()
    {
        cout << "Очистка памяти объекта Student для " << ocenki << " pointer" << endl;
        delete[] ocenki;
        cout << "Student destructed for " << " elements, for " << this << endl;
    }

    void randomize();

    void print();
    
};

void Student::randomize()
{
    for (int i{ 0 }; i < 5; ++i)
    {
        ocenki[i] = rand() % 10;
    }
}

void Student::print()
{
    cout << "ID студента: " << id << " Оценки студента: ";
    for (int i{ 0 }; i < 5; ++i)
    {
        cout << ocenki[i] << " ";
    }
    cout << endl;
}


int main()
{
    setlocale(LC_ALL, "");

    Student stud1;
    stud1.print();
    stud1.setId(1);
    stud1.randomize();
    stud1.print();
    cout << endl;




    Pryamougol pr1{ 3, 6 };
    pr1.print();
    Pryamougol pr2{ pr1 };
    pr2.print();
    pr1.setWidth(2);
    pr1.print();
    pr2.print();



    cout << endl << "____________________________________________Конструктор_копирования____________________________________________" << endl;
    Fraction a{ 2,3 };
    cout << "a = ";
    a.print();
    Fraction b{ a };
    cout << "b = ";
    b.print();
    Fraction c{ Fraction{3,4} };
    cout << "c = ";
    c.print();

    DynArray arr1{ 10 };
    arr1.randomize();
    cout << "array1 elements: ";
    arr1.print();
    DynArray arr2{ arr1 };
    cout << "array2 elements: ";
    arr2.print();

    cout << endl << "________________________________________________Указатель_this________________________________________________" << endl;
    Date date1{ 12,2,2025 };
    Date date2;
    date1.print();
    date2.print();
    date2.setDay(12);
    date2.setMonth(12);
    date2.setYear(2012);
    date1.setYear(2020).setMonth(2).setDay(20);
    date1.print();
    date2.print();
    
    cout << endl << "___________________________________________Статические_члены_класса___________________________________________" << endl;
    //DemoStatic ds1{ 12 };
    //DemoStatic ds2{ 63 };
    //ds1.common = 88;
    //cout << "ds1.personal = " << ds1.personal << "\tds2.personal = " << ds2.personal << endl;
    //cout << "ds1.common = " << ds1.common << endl;
    DemoStatic::print();
    DemoStatic::common = 54;
    cout << "static field common = " << DemoStatic::common << endl;


    cout << endl << "_________________________________________________Инициализация_________________________________________________" << endl;
    Point p1;
    Point p2{2,31};
    Rectangle r1;
    Rectangle r2{12, 47, 128, 256};
    p1.print();
    p2.print();
    r1.print();
    r2.print();
    cout << endl << "___________________________________________Делегирование_конструкторов___________________________________________" << endl;
    Human nobody;
    nobody.print();
    Human person1{ "Ivan Ivanov" };
    person1.print();
    Human person2{ "Petr Petrov", 25 };
    person2.print();
    Human person3{ "Sydor Sydorov", 23, 123456789 };
    person3.print();

    return 0;
}
