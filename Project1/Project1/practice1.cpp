#include <iostream>
using namespace std;

int main()
{
	cout << "Hello, world" << '\n';

	int a = 2;
	int b = 3;

	int c = 0;

	// сложение
	c = a + b;
	cout << "a + b = " << c << endl; // 5
	// вычитание
	c = a - b;
	cout << "a - b = " << c << '\n'; // -1
	// умножение
	c = a * b;
	cout << "a * b = " << c << '\n'; // 6
	// деление
	c = a / b;
	cout << "a / b = " << c << '\n'; // 0
	// остаток от деления
	c = a % b;
	cout << "a % b = " << c << '\n'; // 2


	// НЕ (NOT)	!
	cout << !!c << '\n';
	// И (AND)	&&
	cout << (!!c && a) << '\n';
	// ИЛИ (OR)	||
	cout << (!c || 0) << '\n';



	return 0;
}