// Урок №12 Работа с файлами.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <stdio.h>
#include <io.h>
//#include <fstream>

using namespace std;

int main()
{
    setlocale(LC_ALL, "");

    const int n = 10;
    int arr[n];
    FILE *myFile;
    const char* path = "C:\\Users\\User\\source\\repos\\9-2 РПО 23-1\\Урок №12 Работа с файлами\\test.txt";

    for (int i = 0; i < n; i++)
    {
        arr[i] = i + 5;
    }

    if ((fopen_s(&myFile, path, "w")) != NULL)
    {
        cout << "The file could not be created!" << endl;
    }
    else
    {
        for (int i = 0; i < n; i++)
        {
            fprintf(myFile, "%1d ", arr[i]);
            fprintf(myFile, "\n");
        }

        if (fclose(myFile) == EOF)
        {
            cout << "The file is not closed!" << endl;
        }
        else
        {
            cout << "The file is closed!" << endl;
        }
    }
    struct _finddata_t fileinfo;
    char path1[100];
    char mask[20];

    cout << "Пожалуйста, введите путь (например: D:\\):\n";
    cin >> path1;

    cout << "Пожалуйста, введите маску (например: *.dat):\n";
    cin >> mask;

    strcat_s(path1, mask);

    long done = _findfirst(path1, &fileinfo);

    while (done != -1)
    {
        cout << fileinfo.name << "\n";
        done = _findnext(done, &fileinfo);
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
