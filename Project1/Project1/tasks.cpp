#include <iostream>
using namespace std;

int main()
{
	/*cout << "Enter your name:";
	string name;
	cin >> name;
	if ((name == "Ivan") || (name == "Ira")) {
		cout << "Hello, " << name << "!";
	}
	else {
		cout << "Hello, guest!";
	}*/


	/*int a, sum = 0;
	cin >> a;
	for (int i = 1; i <= a; i++)
	{
		if ((i % 3 == 0) || (i % 5 == 0)) 
		{
			sum += i;
		}
	}
	cout << "SUM: " << sum;*/

	/*for (int i = 2; i <= 12; i++)
	{
		for (int j = 2; j <= 12; j++)
		{
			cout << i << " * " << j << " = " << j * i << '\n';
		}
	}*/


	// % 400 == 0 +
	// % 100 == 0 -
	// % 4 == 0 +
	for (int i = 2022; i < 2442; i++)
	{
		if ((i % 400 == 0) || ((i % 100 != 0) && (i % 4 == 0))) {
			cout << i << " ";
		}
	}


	return 0;
}