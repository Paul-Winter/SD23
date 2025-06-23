// ООП Урок №1 Введение в ООП.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//
#include <iostream>
#include <iomanip>
#include "Student.h"

using namespace std;

class AccessLevel
{
    int privateByDefault;

public:
    int publicMember;

protected:
    int protectedMember;

private:
    int privateMember;
};

int main()
{
    setlocale(LC_ALL, "");

    cout << "__________Успеваемость студентов:__________" << endl << endl;

    // размер массива объектов
    const int size = 3;

    // создание и инициализация динамического массива
    Student* students = new Student[size]
    {
        {"Иванов И.И.", 3, new int[3] {10, 10, 9}},
        {"Петров П.П.", 3, new int[3] {8, 10, 8}},
        {"Сидоров С.С.", 3, new int[3] {6, 7, 5}}
    };

    // вывод значений
    double sum = 0;
    for (Student* stud = students; stud < students + size; stud++)
    {
        double avg = stud->getAvg();
        cout << "Средний балл " << stud->getName() << " : " << avg << endl;
        sum += avg;
    }
    cout << endl;
    cout << "Средний балл по группе: " << sum / size << endl;
    cout << endl;

    delete[] students;

    return 0;
}
