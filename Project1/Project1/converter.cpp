#include <iostream>
using namespace std;

int main()
{
	int a, base;
	cout << "Enter a decimal number to convert:";
	cin >> a;
	cout << "Enter a base (up to 9):";
	cin >> base;
	int b = 0;
	int i = 1;

	do {
		b += a % base * i;
		a /= base;
		i *= 10;
	} while (a > 0);

	cout << "The result is: " << b;
	return 0;
}