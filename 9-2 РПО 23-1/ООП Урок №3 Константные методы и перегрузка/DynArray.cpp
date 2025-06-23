#include <iostream>
#include "DynArray.h"

using namespace std;

// конструктор (по умолчанию 10 элементов)
DynArray::DynArray(int aSize) : size{ aSize }, dynArr(new int[size] {})
{
}

// конструктор копирования
DynArray::DynArray(const DynArray& arr) : size{arr.size}, dynArr{new int[size]}
{
	for (int i = 0; i < size; ++i)
	{
		dynArr[i] = arr.dynArr[i];
	}
}

// деструктор
DynArray::~DynArray()
{
	delete[] dynArr;
}

int DynArray::length() const
{
	return size;
}

// оператор присваивания
const DynArray& DynArray::operator=(const DynArray& arr)
{
	if (&arr != this)
	{
		if (size != arr.size)
		{
			delete[] dynArr;
			size = arr.size;
			dynArr = new int[size];
		}
		for (int i = 0; i < size; ++i)
		{
			dynArr[i] = arr.dynArr[i];
		}
	}
	return *this;
}

// оператор сравнения
bool DynArray::operator==(const DynArray& arr) const
{
	if (size != arr.size)
	{
		return false;
	}
	for (int i = 0; i < size; ++i)
	{
		if (dynArr[i] != arr.dynArr[i])
		{
			return false;
		}
	}
	return true;
}

// оператор индексирования
int& DynArray::operator[](int index)
{
	if (index < 0 || index >= size)
	{
		cout << "Индекс вне границ диапазона!" << endl;
		exit(1);
	}
	return dynArr[index];
}

// оператор индексирования
int DynArray::operator[](int index) const
{
	if (index < 0 || index >= size)
	{
		cout << "Индекс вне границ диапазона!" << endl;
		exit(1);
	}
	return dynArr[index];
}

// оператор ввода
istream& operator>>(istream& input, DynArray& arr)
{
	for (int i{ 0 }; i < arr.size; ++i)
	{
		input >> arr.dynArr[i];
	}
	return input;
}

// оператор вывода
ostream& operator<<(ostream& output, const DynArray& arr)
{
	for (int i{ 0 }; i < arr.size; ++i)
	{
		output << arr.dynArr[i] << " ";
	}
	output << endl;
	return output;
}
