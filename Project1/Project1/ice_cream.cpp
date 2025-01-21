#include <iostream>
using namespace std;


int main()
{
	int money, ice, i = 0;
	cin >> money >> ice;
	cout << '\n';
	while (money >= ice) {
		money -= ice;
		i++;
		cout << money << '\n';
	}
	cout << "i: " << i;

	return 0;
}