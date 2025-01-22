// Урок №7 Указатели.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <stdlib.h>
#include <time.h>

using namespace std;

#pragma region Указатели

    //int x = 12;
    //int y;
    //int *px;

    //px = &x;
    ////y = x + 1;
    //y = *px + 1;
    //
    //cout << "x = " << x << "\t y = " << y << endl;

#pragma endregion

#pragma region Указатели и массивы

    //int a[10] = { 1,2,3,4,5,6,7,8,9,0 };
    //int* pa = &a[0];
    //int x = *pa;
    //cout << "pa = " << pa << "\tx = " << x << endl;
    //      a[i] = *(pa + i)
    // имя массива - синоним местоположения его нулевого элемента
    // фактически имя массива - это указатель на его нулевой элемент
    //      pa = &a[0]
    // Указатель является переменной, т.е. операции pa = a, pa++ имеют смысл и компилируются
    // Имя массива является константой, а не переменной и конструкции а = pa, a++ будут незаконными

    //const int size = 5;
    //int arr[size] = { 33, 44, 7, 8, 12 };
    //for (int i = 0; i < size; i++)
    //{
    //    cout << *(arr + i) << " ";
    //}
    //*(arr + 1) = 25;
    //*(arr + 2) = 9;
    //cout << endl << endl;

    //for (int i = 0; i < size; i++)
    //{
    //    cout << *(arr + i) << " ";
    //}
    //cout << endl << endl;

    //int array[size] = { 33, 44, 7, 8, 12 };
    //int* ptr = array;
    //for (int i = 0; i < size; i++)
    //{
    //    cout << ptr[i] << " ";
    //}
    //ptr[1] = 25;
    //ptr[2] = 9;
    //cout << endl << endl;

    //for (int i = 0; i < size; i++)
    //{
    //    cout << ptr[i] << " ";
    //}
    //cout << endl << endl;

    //cout << "sum of array = " << GetAmount(ptr, size) << endl;

#pragma endregion

#pragma region Swap

void swap(int *x, int *y)
{
    int tmp = *x;
    *x = *y;
    *y = tmp;    
}

    //// swap
    //int x = 10;
    //int y = 52;
    //cout << "Before swap: x = " << x << "\ty = " << y << endl;
    //swap(&x, &y);
    //cout << "After swap: x = " << x << "\ty = " << y << endl;

#pragma endregion

#pragma region Нулевые указатели

    //// нулевые указатели
    //// 1-й путь - вписать значение 0 в указатель
    //// (так делать не рекомендуется)
    //int* ptr1 = 0;
    //if (ptr1 != 0) cout << "действия" << endl;

    //// 2-й путь - использование макроса NULL
    //// наследие языка С (так делать не рекомендуется)
    //int* ptr2 = NULL;
    //if (ptr2 != NULL) cout << "действия" << endl;

    //// 3-й путь - использование нулевого указателя
    //// современный способ С++
    //int* ptr3 = nullptr;
    //if (ptr3 != nullptr) cout << "действия" << endl;

#pragma endregion

#pragma region Классная работа

int GetAmount(int* ptr, int size)
{
    int sum = 0;
    for (int i = 0; i < size; i++)
    {
        sum += *(ptr + i);
    }
    return sum;
}

    //// создать массив из 10 элементов
    //const int size = 10;
    //// заполнить его
    //int arr[size]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 5 };
    //// вывести в консоль
    //for (int i = 0; i < size; i++)
    //{
    //    cout << arr[i] << " ";
    //}
    //cout << endl;
    //// поменять местами чётные и нечётные элементы массива
    //for (int i = 0; i < size - 1; i+=2)
    //{
    //    swap(arr[i], arr[i + 1]);
    //}
    //// вывести в консоль результат
    //for (int i = 0; i < size; i++)
    //{
    //    cout << arr[i] << " ";
    //}
    //cout << endl;

#pragma endregion

#pragma region Константные указатели

    //// константный указатель
    //int x = 12;
    //int* const px = &x;
    //int y = 45;
    ////px = &y;
    //cout << *px << endl;
    //*px = 45;
    //x = 56;
    //cout << *px << endl;
    // 
    //// указатель на константу
    //int x = 73;
    //const int* px;
    //px = &x;
    //cout << "px = " << px << "\n*px = " << *px << endl;
    //int y = 88;
    //px = &y;
    //cout << "px = " << px << "\n*px = " << *px << endl;
    //x = 55;
    //y = 33;
    //cout << "px = " << px << "\n*px = " << *px << endl;
    ////*px = 44;

    ////// константный указатель на константу
    //int x = 7;
    //int y = 12;
    //const int* const px = &x;
    //cout << "px = " << px << "\n*px = " << *px << endl;
    ////*px = &y;

#pragma endregion

#pragma region Ссылки

    //// обращение по имени переменной
    //int x = 123456;
    //// обращение через указатель переменной
    //int* px = &x;
    //// обращение по ссылке
    //int& refx = x;
    //// обращение через указатель ссылки
    //int* pref = &refx;

    //cout << "x = " << x << endl;
    //cout << "px = " << *px << endl;
    //cout << "refx = " << refx << endl;
    //cout << "pref = " << *pref << endl;

#pragma endregion

#pragma region Ссылки в параметрах функций

void change(int& x, int& y)
{
    int temp = x;
    x = y;
    y = temp;
}

void func_ptr(int* p)
{
    *p = 12;
}
void func_ref(int& r)
{
    r = 12;
}

    //int x = 13;
    //int y = 42;
    //cout << "Before swap: x = " << x << "\ty = " << y << endl;
    //change(x, y);
    //cout << "After swap: x = " << x << "\ty = " << y << endl;
    //int* px = &x;
    //int& ry = y;
    //func_ptr(&x);
    //func_ref(y);
    //cout << "After func_ptr: x = " << x << "\ty = " << y << endl;
    //cout << "After func_ref: x = " << x << "\ty = " << y << endl;

#pragma endregion

#pragma region Операторы выделения памяти

#pragma endregion


int main()
{
    setlocale(LC_ALL, "");

    // 1-я форма использования new (переменная)
    int* p = new int (3);
    int* other = p;
    cout << "p = " << *p << endl;

    // 2-я форма использования new (массив)
    cout << "Введите размер массива: ";
    int size;
    cin >> size;
    int* arr = new int[size];
    cout << endl << "Введите элементы массива:" << endl;
    for (int i = 0; i < size; i++)
    {
        cin >> arr[i];
    }
    cout << endl << endl << "Ваш массив:" << endl;
    for (int i = 0; i < size; i++)
    {
        cout << arr[i] << "\t";
    }
    cout << endl << endl;
    
    // удаляет динамический объект типа int
    delete p;
    // удаляет динамический массив
    delete[] arr;

    if (p)
    {
        delete p;
        p = nullptr;
    }
    if (arr)
    {
        delete[] arr;
        arr = nullptr;
    }

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
