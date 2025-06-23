// ООП Урок №10 Преобразование типов.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

#pragma region const_cast

void some_func(const int* ptr)
{
    // создаём временную переменную
    int* temp;

    // снимаем модификатор const
    temp = const_cast<int*>(ptr);

    // изменяем объект
    *temp = *ptr * *ptr;
}

#pragma endregion

#pragma region dynamic_cast

class B
{
public:
    virtual void Test()
    {
        cout << "Test B" << endl;
    }
};

class D : public B
{
public:
    void Test()
    {
        cout << "Test D" << endl;
    }
};

#pragma endregion

#pragma region reinterpret_cast

unsigned short Hash(void* p)
{
    unsigned int val = reinterpret_cast<unsigned int>(p);
    return (unsigned short)(val ^ (val >> 16));
}

#pragma endregion

#pragma region static_cast

// Произвести приведение переменной типа int к типу согласно варианта
// использовать оператор static_cast
// 
// Александрова - short
// Антонов      - long
// Духина       - long long
// Землянский   - float
// Золин        - double
// Красицкий    - char
// Кубанов      - short
// Лушников     - long
// Мамонтова    - long long
// Метелицин    - float
// Назарян      - double
// Чавычалов    - short
// Юнусов       - char

#pragma endregion


int main()
{
    setlocale(LC_ALL, "");

    cout << "________________________________reinterpret_cast________________________________" << endl;

    int a[10];
    for (int i = 0; i < 10; i++)
        cout << Hash(a + i) << endl;

    cout << endl << endl;

    cout << "__________________________________const_cast__________________________________" << endl;
    int x = 10;
    cout << "Before const_cast: " << x << endl;
    some_func(&x);
    cout << "After const_cast: " << x << endl;
    cout << endl << endl;

    cout << "_________________________________dynamic_cast_________________________________" << endl;
    // указатель на класс-родитель
    B* ptrB;
    // объект класса-родителя
    B objB;
    // указатель на класс-потомок
    D* ptrD;
    // объект класса-потомка
    D objD;

    // приводим адрес объекта D* к указателю типа D*
    ptrD = dynamic_cast<D*>(&objD);
    if (ptrD)
    {
        cout << "Произошло приведение типов" << endl;
        ptrD->Test();
    }
    else
    {
        cout << "Произошла ошибка приведения типа" << endl;
    }

    // приводим адрес объекта B* к указателю типа B*
    ptrB = dynamic_cast<B*>(&objB);
    if (ptrB)
    {
        cout << "Произошло приведение типов" << endl;
        ptrB->Test();
    }
    else
    {
        cout << "Произошла ошибка приведения типа" << endl;
    }

    // приводим адрес объекта D* к указателю типа B*
    ptrB = dynamic_cast<B*>(&objD);
    if (ptrB)
    {
        cout << "Произошло приведение типов" << endl;
        ptrB->Test();
    }
    else
    {
        cout << "Произошла ошибка приведения типа" << endl;
    }

    // приводим адрес объекта B* к указателю типа D*
    ptrD = dynamic_cast<D*>(&objB);
    if (ptrD)
    {
        cout << "Произошло приведение типов" << endl;
        ptrD->Test();
    }
    else
    {
        cout << "Произошла ошибка приведения типа" << endl;
    }

    cout << endl << endl;



    return 0;
}
