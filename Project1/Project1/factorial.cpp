#include <iostream>
using namespace std;

int main()
{
	unsigned a = 5;
	unsigned i = 1;
	unsigned r = 1;
	cout << "!" << a << " = ";
	if (a == 0) {
		cout << 1;
		return 0;
	}
	while (i <= a) {
		r *= i++;
	}
	cout << r;
	return 0;
}