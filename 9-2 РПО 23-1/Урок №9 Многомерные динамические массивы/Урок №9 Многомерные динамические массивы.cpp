// Урок №9 Многомерные динамические массивы.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

using namespace std;

#pragma region Создание многомерного динамического массива

    //// 1. сначала объявляется указатель на указатель
    //// m1 - количество строк (задаётся заранее) (rows)
    //int m1 = 5;
    //int m2 = 5;
    //int** A = new int* [m1];

    //// 2. каждый массив указателей (на фактические значения) необходимо инициализировать
    //// данная операция выполняется с помощью цикла
    //for (int i = 0; i < m1; i++)
    //{
    //    // m2 - количество столбцов (задаётся заранее) (columns)
    //    A[i] = new int[m2];
    //}

#pragma endregion

#pragma region Удаление многомерного динамического массива

    //// 1. удаление каждого динамического массива с помощью цикла
    //for (int i = 0; i < m1; i++)
    //{
    //    delete[] A[i];
    //}
    //// 2. удаление динамического массива указателей
    //delete[] A;

#pragma endregion

#pragma region Пример №1

    //int i, j;
    //int rows = 5;
    //int cols = 5;

    //// создать указатель на указатели
    //int** ptrA = new int* [rows];

    //// создать массив указателей в цикле
    //for (i = 0; i < rows; i++)
    //{
    //    ptrA[i] = new int[cols];
    //}

    //// создать заготовку для работы со случайными числами
    //srand(time(NULL));

    //// заполнить двумерный массив используя вложенные циклы
    //for (i = 0; i < cols; i++)
    //{
    //    for (j = 0; j < rows; j++)
    //    {
    //        ptrA[i][j] = rand() % 90 + 10;
    //    }
    //}

    //// вывести содержимое массива используя вложенные циклы
    //for (i = 0; i < cols; i++)
    //{
    //    for (j = 0; j < rows; j++)
    //    {
    //        cout << ptrA[i][j] << "\t";
    //    }
    //    cout << endl;
    //}

    //// удалить массив указателей
    //for (i = 0; i < rows; i++)
    //{
    //    delete[] ptrA[i];
    //}

    //// удалить указатель на указатели
    //delete[] ptrA;

#pragma endregion

#pragma region Создание треугольного массива

    //int i, j, k;
    //int rows = 5;
    //int cols = 5;

    //srand(time(NULL));

    //int** pArr = new int* [rows];
    //k = cols;

    //for (i = 0; i < rows; i++, k--)
    //{
    //    pArr[i] = new int[k];
    //}

    //for (i = 0; i < rows; i++, cols--)
    //{
    //    for (j = 0; j < cols; j++)
    //    {
    //        pArr[i][j] = rand() % 90 + 10;
    //        cout << pArr[i][j] << " ";
    //    }
    //    cout << endl;
    //}

    //for (i = 0; i < rows; i++)
    //{
    //    delete[] pArr[i];
    //}
    //delete[] pArr;

#pragma endregion

#pragma region Создание трёхмерного массива

    //int i, j, k;
    //int length = 3;
    //int width  = 3;
    //int height = 3;

    //int*** pArr = new int** [length];

    //for (i = 0; i < length; i++)
    //{
    //    pArr[i] = new int* [width];

    //    for (j = 0; j < width; j++)
    //    {
    //        pArr[i][j] = new int[height];
    //    }
    //}

    //srand(time(NULL));

    //for (i = 0; i < length; i++)
    //{
    //    for (j = 0; j < width; j++)
    //    {
    //        for (k = 0; k < height; k++)
    //        {
    //            pArr[i][j][k] = rand() % 90 + 10;
    //            cout << pArr[i][j][k] << " ";
    //        }
    //        cout << endl;
    //    }
    //    cout << endl << endl;
    //}

    //for (i = 0; i < length; i++)
    //{
    //    for (j = 0; j < width; j++)
    //    {
    //        delete[] pArr[i][j];
    //    }
    //    delete[] pArr[i];
    //}
    //delete[] pArr;

#pragma endregion

#pragma region Перечисления

#pragma endregion

enum Seasons {
    WINTER,
    SPRING,
    SUMMER,
    FALL };
enum Months {January = 1 , February, March, April, May, June, July, August, September, October, November, December};
enum Planets {Mercury = 1, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune};
enum {Single, Married} status;
enum signal {ON, OFF};
enum answer {YES, NO, MAYBE};

enum menuItems
{
    ENTER_DATA = 1,
    OUTPUT_DATA = 2,
    QUIT = 3
};

// написать программу которая имитирует работу Magic 8 Ball

int main()
{
    setlocale(LC_ALL, "");

    cout << "Your choice:\n";
    cout << "1 - enter data\n";
    cout << "2 - output data\n";
    cout << "3 - quit program\n";

    int userChoice;

    cin >> userChoice;

    switch (userChoice)
    {
    case ENTER_DATA: cout << "dlfkjd"; break;
    case OUTPUT_DATA: cout << "dlfkjsdlf"; break;
    case QUIT: cout << "exit"; break;
    default:
        break;
    }

    Seasons season = WINTER;
    cout << season << endl;

    Months month = May;
    cout << month << endl;

    Planets planet = Earth;
    cout << planet << endl;

    status = Married;
    cout << status << endl;

    return 0;
}

