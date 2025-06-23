// ООП Урок №8 Виртуальные функции.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <string.h>

using namespace std;

// абстрактный базовый класс
class Animal
{
public:
    // кличка животного
    char name[25];
    // простой конструктор
    Animal(const char *n)
    {
        strcpy_s(name, n);
    }
    // чисто виртуальная функция
    virtual void speak() = 0;
    virtual ~Animal() = 0;
};
Animal::~Animal() {};

class CrazyFrog : public Animal
{
public:
    CrazyFrog(const char* name) : Animal(name) {};
    virtual void speak()
    {
        cout << name << " говорит \'ква-ква\'" << endl;
    }
};

class Giraffe : public Animal
{
public:
    Giraffe(const char* name) : Animal(name) {};
    virtual void speak()
    {
        cout << name << " говорит \'фырк-мууяу\'" << endl;
    }
};

class Gecko : public Animal
{
public:
    Gecko(const char* name) : Animal(name) {};
    virtual void speak()
    {
        cout << name << " говорит \'шкряб-шкряб\'" << endl;
    }
};

class Cat : public Animal
{
public:
    Cat(const char* name) : Animal(name) {};
    virtual void speak()
    {
        cout << name << " говорит \'мяу-мяу\'" << endl;
    }
};

class Tiger : public Cat
{
public:
    Tiger(const char* name) : Cat(name) {};
    //virtual void speak()
    //{
    //    cout << name << " говорит \'ррр-ррр\'" << endl;
    //}
    //virtual int speak() - вызовет ошибку компиляции
    //{
    //    cout << name << " говорит \'ррр-ррр\'" << endl;
    //    return 0;
    //}
    virtual void speak(int when)
    {
        cout << name << " говорит \'ррр-ррр\'" << endl;
    }
};

#pragma region Виртуальный базовый класс

class A
{
public:
    int val;
};

class B : public virtual A {};
class C : public virtual A {};
class D : public B, public C
{
public:
    int GetVal()
    {
        return val;
    }
};

#pragma endregion

class Bird
{
private:
    char* birdName;
    int size;
public:
    Bird(const char *bn, int s)
    {
        size = s;
        birdName = new char[size];
    }
    virtual ~Bird()
    {
        cout << "Птичку жалко" << endl;
        delete[] birdName;
    }
};

class Penguin : public Bird
{
private:
    char* pengName;
    int size2;
public:
    Penguin(const char *bn, int s1, const char *pn, int s2) : Bird(bn, s1)
    {
        size2 = s2;
        pengName = new char[size2];
    }
    virtual ~Penguin()
    {
        cout << "Пингвина жалко" << endl;
        delete[] pengName;
    }
};

int main()
{
    setlocale(LC_ALL, "");

    Animal* animals[5] = { new CrazyFrog("Квакша"),
                          new Giraffe("Оливер"),
                          new Gecko("Саша"),
                          new Cat("Иннокентий"),
                          new Tiger("Лев") };

    for (size_t i = 0; i < 5; ++i)
    {
        animals[i]->speak();
    }
    animals[4]->speak();
    Tiger* tiger = new Tiger("Лев");
    tiger->speak(1);

    cout << endl << endl;
    cout << "________________________________Виртуальный деструктор________________________________" << endl;

    Penguin kovalsky("Рико", 4, "Ricko", 5);
    Bird* bird;
    bird = new Penguin("Ковальски", 9, "Kovalsky", 8);

    return 0;
}
