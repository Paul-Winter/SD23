#include <iostream>
#include "Student.h"

using namespace std;

// конструктор по умолчанию
Student::Student()
{
	name = nullptr;
	markCount = 0;
	marks = nullptr;
}

// конструктор параметризованный
Student::Student(const char* studentName)
{
	createName(studentName);
	markCount = 0;
	marks = nullptr;
}

// конструктор параметризованный
Student::Student(const char* studentName, const int studentMarkCount)
{
	createName(studentName);
	markCount = studentMarkCount;
	marks = new int[studentMarkCount];
	for (int i = 0; i < studentMarkCount; i++)
	{
		marks[i] = 0;
	}
}

// конструктор
Student::Student(const char* studentName, const int studentMarkCount, const int* studentMarks)
{
	// присваивание имени
	createName(studentName);

	// присваивание количества оценок
	markCount = studentMarkCount;

	// присваивание списка оценок
	marks = new int[studentMarkCount];
	for (int i = 0; i < studentMarkCount; i++)
	{	
		marks[i] = studentMarks[i];
	}
}

// деструктор
Student::~Student()
{
	std::cout << "Отработал деструктор " << name << std::endl;

	// освобождение памяти для имени
	if (name != nullptr)
	{
		delete[] name;
	}

	// освобождение памяти для списка оценок
	if (marks != nullptr)
	{
		delete[] marks;
	}
}

// присваивание имени
void Student::createName(const char* studentName)
{
	int nameLength = strlen(studentName);
	name = new char[nameLength + 1];
	for (int i = 0; i <= nameLength; i++)
	{
		name[i] = studentName[i];
	}
}

// мутатор имени
void Student::setName(const char* studentName)
{
	delete[] name;
	createName(studentName);
}

// мутатор оценки
void Student::setMark(int mark, int index)
{
	// проверка индекса
	if (index < 0 || index >= markCount)
	{
		return;
	}
	// проверка оценки
	if (mark < 1 || mark > 12)
	{
		mark = 0;
	}
	marks[index] = mark;
}

// вычисление среднего балла
double Student::getAvg()
{
	double sum = 0;
	for (int i = 0; i < markCount; i++)
	{
		sum += marks[i];
	}
	return sum / markCount;
}
