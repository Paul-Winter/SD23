// ООП Урок №9 Исключительные ситуации.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

#pragma region Исключительные ситуации

void test(int num)
{
    cout << "начало" << endl;

    try
    {
        if (num == 2)
        {
            throw "\nError - num is 2\n";
        }
        else if (num == 3)
        {
            throw "\nError - num is 3\n";
        }
    }
    catch (char* ex)
    {
        cout << "\n\n!!! Exception !!!\n\n";
        throw;
    }
}

#pragma endregion

namespace combat
{
    void fire()
    {
        cout << "Выстрел!" << endl;
    }
}
namespace explore
{
    void fire()
    {
        cout << "Горит факел!" << endl;
    }
}
namespace combat
{
    int patrony = 100;
}
int main()
{
    setlocale(LC_ALL, "");

    using explore::fire;
    combat::fire();
    fire();
    cout << combat::patrony;

    try
    {
        test(1);
        test(2);
    }
    catch (char* ex)
    {
        cout << ex;
    }

    try
    {
        int* ptr = 0;
        int size;

        cout << "Введите количество элементов массива (от 1 до 100): ";
        cin >> size;
        cout << endl;

        if (size < 1 || size > 100)
        {
            throw "\n\nError size!!!\n\n";
        }

        ptr = new int[size];

        if (!ptr)
        {
            throw "\n\nError memory!!!\n\n";
        }

        int a;
        cout << "Введите тестовую переменную a = ";
        cin >> a;

        if (a == 0)
        {
            throw a;
        }
    }
    catch (int exception)
    {
        cout << "Исключительная ситуация! a = " << exception << endl;
    }
    //catch (char* exception)
    //{
    //    cout << "Исключительная ситуация! " << exception << endl;
    //}
    catch (...)
    {
        cout << "Some error!" << endl;
    }

    return 0;
}

