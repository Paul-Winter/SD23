// Урок №6 Функции.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <stdlib.h>
#include <time.h>

using namespace std;

#pragma region Функции

//// не принимает параметров и ничего не возвращает
//void Hello()
//{
//    cout << "Hello World!\n";
//}
//// принимает один параметр, но ничего не возвращает
//void StarLine(int count)
//{
//    for (int i = 0; i < count; i++)
//    {
//        cout << "*";
//    }
//    cout << "\n\n";
//}
//// принимает два параметра, но всё ещё ничего не возвращает
//void AnyLine(char symb, int count)
//{
//    for (int i = 0; i < count; i++)
//    {
//        cout << symb;
//    }
//    cout << "\n\n";
//}
//// принимает два параметра и возвращает их сумму
//int Sum(int x, int y)
//{
//    return x + y;
//}
//// принимает два параметра и возвращает максимальное из значений
//int Maximum(int x, int y)
//{
//    if (x > y)
//        return x;
//    return y;
//}
//// принимает два параметра и возвращает минимальное из значений
//int Minimum(int x, int y)
//{
//    return (x < y) ? x : y;
//}

#pragma endregion

#pragma region FarmSimulator

//    int dohod = 0;
//    int rashod = 1000;
//    int chicken = 10;
//    int eggPrice = 3600;
//    int chickenPrice = 70000;
//    int FarmSimulator(int dohod, int rashod, int chicken, int eggPrice, int chickenPrice)
//{
//    int day = 0;
//    int dayX = 0;
//    int eggs = 0;
//    int totalMargin = dohod;
//    int totalExpense = rashod;
//
//    while (dayX < 365)
//    {
//        day++;
//        dayX++;
//        // учитываем ежедневные расходы на содержание фермы
//        totalExpense += 1000;
//
//        if (day == 7)
//        {
//            // собираем яйца
//            eggs += chicken * 1;
//            // продаём яйца
//            totalMargin += eggs * eggPrice;
//            eggs = 0;
//            // покупаем новых курочек
//            totalExpense += 3 * chickenPrice;
//            chicken += 3;
//            day = 0;
//        }
//    }
//    return totalMargin - totalExpense;
//}

#pragma endregion

#pragma region Классная работа
//
//void Antonov(int x )
//{
//       if (x % 2 == 0)
//           cout << x << " Четное"; 
//       else
//       {
//           cout << x << " Нечетное";
//       }
//}
//
//int Aleksandrova(int x)
//{
//    if (x % 2 == 0)
//        return 1;
//    else
//        return 0;
//}
//
//bool Aleksandrova2(int x)
//{
//    if (x % 2 == 0)
//        return true;
//    else
//        return false;
//}
//
//void Kubanov(int x, int y)
//{
//    for (int i = x; i <= y; i++)
//        cout << i << " ";
//}
//
//int Duhina(int x)
//{
//    return(x * x * x);       
//}
//
//void Lushnikov(int x, int y)
//{
//    cout << (x / y) << endl;
//    cout << (x % y) << endl;
//}
//
//int Chavichalov(int x, int y)
//{
//    srand(time(NULL));
//    return rand()% y + x;
//}

#pragma endregion

#pragma region Области видимости

// локальные и глобальные переменные
//int x = 20;

//int Show()
//{
//    //int x = 10;
//    return x += 10;
//}
    //cout << Show() << endl;
    //cout << x << endl;
    //const int row = 4;
    //const int col = 4;
    //int array[row][col] =
    //{
    //    {1,2,3,4},
    //    {5,6,7,8},
    //    {9,0,1,2},
    //    {3,5,7,9}
    //};
    //for (i = 0; i < row; i++)
    //{
    //    for (int i = 0; i < col; i++)
    //    {
    //        cout << array[::i][i] << "\t";
    //    }
    //    cout << endl;
    //}

#pragma endregion

#pragma region Статические переменные

//void SomeFunc()
//{
//    static int x = 0;
//    x++;
//    cout << x << endl;
//}

#pragma endregion

void RandomizerRPO(int students)
{
    srand(time(NULL));
    cout << rand() % students + 1 << endl;    
}

void Star(char symb = '*', int count = 20)
{
    for (int i = 0; i < count; i++)
    {
        cout << symb;
    }
    cout << endl;
}

int Sum(int a, int b)
{
    ++a;
    ++b;
    cout << "a = " << a << "\t b = " << b << endl;
    return a + b;
}

void Summa(int arr[], int size, int num)
{
    for (int i = 0; i < size; i++)
    {
        num += arr[i];
    }
    cout << "num in func = " << num << endl;
}


// написать функцию, которая принимает число
// и выводит в консоль простое оно или нет
// если не простое, то выводит делители этого числа

int main()
{
    setlocale(LC_ALL, "");



    //RandomizerRPO(13);

    //int x = 10;
    //cout << "x = " << x << endl;

    //int y = 20;
    //cout << "y = " << y << endl;

    //int z = Sum(x, y);
    //cout << "z = x + y = " << z << endl;
    //cout << "x = " << x << endl;
    //cout << "y = " << y << endl;

    //int num = 4;

    //int array[] = {1, 2, 3};
    //for (int i = 0; i < 3; i++)
    //{
    //    cout << array[i] << " ";
    //}
    //cout << endl;
    //cout << "num = " << num << endl;
    //Summa(array, 3, num);
    //for (int i = 0; i < 3; i++)
    //{
    //    cout << array[i] << " ";
    //}
    //cout << endl;
    //cout << "num = " << num << endl;

    return 0;
}



// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
