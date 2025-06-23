// ООП Урок №11 Стандартная библиотека.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <algorithm>
#include <iostream>
#include <stdlib.h>
#include <string>
#include <vector>

using namespace std;

//void func()
//{
//    FILE* f;
//
//    if (!(f = fopen("test.txt", "rt")))
//    {
//        exit(0);
//    }
//    // какая-то работа с файлом
//    fclose(f);
//}
//
//class FileWork
//{
//    FILE* f;
//
//public:
//    FileWork(const char* fileName, const char* mode)
//    {
//        if (!(f = fopen(fileName, mode)))
//        {
//            exit(0);
//        }
//    }
//    ~FileWork()
//    {
//        fclose(f);
//    }
//
//    void func()
//    {
//        FileWork myFile("test.txt", "rt");
//        // какая-то работа с файлом
//    }
//};

// 1.Создать класс
// 2.Реализовать конструктор, в котором будет открываться файл
// 3.Реализовать деструктор, в котором файл будет закрываться
// 4.Для начала работы с файлом - создать объект класса

#pragma region auto_ptr

class Temp
{
public:
    Temp()
    {
        cout << "Construct Temp" << endl << endl;
    }
    ~Temp()
    {
        cout << "Destruct Temp" << endl << endl;
    }
    void Test()
    {
        cout << "Temp TEST" << endl << endl;
    }
};

#pragma endregion

int main()
{
    setlocale(LC_ALL, "");

    vector<int> vect;

    cout << "Количество элементов, которые можно разместить без выделения памяти: " << vect.capacity() << endl;
    cout << "Количество элементов в векторе: " << vect.size() << endl;

    vect.resize(4, 0);

    cout << "Resize vector" << endl;
    cout << "Количество элементов в векторе: " << vect.size() << endl;

    cout << "_________________________________________________________________________________" << endl << endl;
    cout << "Vector --> ";
    for (size_t i = 0; i < vect.size(); i++)
    {
        cout << vect[i] << "\t";
    }
    cout << endl;
    cout << "_________________________________________________________________________________" << endl << endl;

    cout << "Максимальный размер вектора: " << vect.max_size()/4 << endl;
    vect.push_back(1);

    cout << "_________________________________________________________________________________" << endl << endl;
    cout << "Vector --> ";
    for (size_t i = 0; i < vect.size(); i++)
    {
        cout << vect[i] << "\t";
    }
    cout << endl;
    cout << "_________________________________________________________________________________" << endl << endl;

    vector<int>::reverse_iterator i_rIter = vect.rbegin();
    vector<int>::iterator i_iter = vect.end();

    cout << "Содержимое вектора с использованием реверсивного итератора" << endl;
    cout << "_________________________________________________________________________________" << endl << endl;
    cout << "Vector --> ";
    for (size_t i = 0; i < vect.size(); i++)
    {
        cout << *(i_rIter + i) << "\t";
    }
    cout << endl;
    cout << "_________________________________________________________________________________" << endl << endl;

    cout << "Содержимое вектора с использованием итераторов" << endl;
    cout << "_________________________________________________________________________________" << endl << endl;
    cout << "Vector --> ";
    for (i_iter = vect.begin(); i_iter != vect.end(); i_iter++)
    {
        cout << *(i_rIter) << "\t";
    }
    cout << endl;
    cout << "_________________________________________________________________________________" << endl << endl;

    cout << "Вставка элементов:" << endl;
    i_iter = vect.end();
    vect.insert(i_iter - 1, 3, 2);

    cout << "_________________________________________________________________________________" << endl << endl;
    cout << "Vector --> ";
    for (size_t i = 0; i < vect.size(); i++)
    {
        cout << vect[i] << "\t";
    }
    cout << endl;
    cout << "_________________________________________________________________________________" << endl << endl;

    /*
    // присвоить строку символов объекту типа string
    string hello = "Hello, World!";
    string privet = "Привет, Мир!";

    cout << hello << endl;
    cout << privet << endl;

    // получить первое слово в строке
    int firstWordEnd = hello.find(',');
    string substring = hello.substr(0, firstWordEnd);

    cout << substring << endl;

    // вывести результаты
    printf("String: %s\n", hello.c_str());
    printf("Sub string: %s\n", substring.c_str());
    */    

    ///////////////// list
    //    Фамилия   - метод -   тип
    // 
    // Александрова - clear -   long long
    // Антонов      - erase -   double
    // Духина       - empty -   char
    // Землянский   - assign-   int
    // Золин        - pop_front, pop_back
    // Красицкий    - push_back, push_front
    // Кубанов      - remove-   float
    // Лушников     - resize-   int
    // Мамонтова    - sort  -   int
    // Метелицин    - swap  -   double
    // Назарян      - reverse   char
    // Чавычалов    - clear -   long
    // Юнусов       - swap  -   long long

    //auto_ptr<Temp> ptr1(new Temp);
    //auto_ptr<Temp> ptr2;
    //ptr2 = ptr1;
    //ptr2->Test();
    //Temp* ptr = ptr2.get();
    //ptr->Test();

    return 0;
}
