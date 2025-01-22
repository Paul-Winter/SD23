// FibConsoleApp.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <vector>

using namespace std;

unsigned int fib(unsigned int n);
void calcFib();

int main()
{
	calcFib();
}

unsigned int fib(const unsigned int n)
{
	// создание пустого множества для хранения элементов числового ряда
	std::vector<int> elements(n + 1);
	// задание значений первых элементов в ряду
	elements[0] = 0;
	elements[1] = 1;
	// выполняем сложение двух соседних элементов
	// для нахождения каждого следующего
	for (int i = 0; i < (n - 1); i++)
	{
		elements[i + 2] = elements[i] + elements[i + 1];
	}
	return elements[n];
}

void calcFib() {
	cout << "Input a positive integer number: ";
	unsigned int number = 0;
	cin >> number;
	cout << "Fibonacci number: " << fib(number);
}

// test comment
