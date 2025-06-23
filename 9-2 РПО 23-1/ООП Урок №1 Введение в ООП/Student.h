#pragma once

// класс "Студент"
// ----------------------------------------------------------------------------------------------
// определение класса
class Student
{   
private:
    // ФИО
    char* name;

    // количество оценок
    int markCount;

    // оценки
    int* marks;

    // служебная функция
    void createName(const char* studentName);

public:
    // конструктор по умолчанию
    Student();

    // конструктор параметризованный
    Student(const char* studentName);

    // конструктор параметризованный
    Student(const char* studentName, const int studentMarkCount);

    // конструктор параметризованный
    Student(const char* studentName, const int studentMarkCount, const int* studentMarks);

    // деструктор
    ~Student();

    // вычисление среднего балла
    double getAvg();

    // аксессор имени
    const char* getName()
    {
        return name;
    }

    // мутатор имени
    void setName(const char* studentName);

    // аксессор оценки
    int getMark(int index)
    {
        return marks[index];
    }

    // мутатор оценки
    void setMark(int mark, int index);
};
// ----------------------------------------------------------------------------------------------
