#include <iostream>
using namespace std;


int main() {
	//string s = "Hello, world";
	//cout << s + s;

	/* 
	Численные типы
	int (integer) - целое число
	unsigned - неотрицательное
	float - дробное
	double - дробное побольше

	Численные литералы
	5 - int
	5.2 - double
	5.2f - float
	5u - unsigned int
	5l - long int
	5ll - long long int
	5.2l - long double
	*/
	//int a = 5.999 + 5.0f;
	//cout << a;


	int a;
	a = 5;
	cout << a;


	// {} - инициализатор
	int b = {};
	cout << b;

	double c{2.2f};
	cout << c;

	unsigned d{1u};
	cout << d << '\n';

	unsigned long long e = -1;
	cout << e << '\n';
	cout << UINT64_MAX;


	return 0;
}