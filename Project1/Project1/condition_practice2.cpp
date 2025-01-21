// принять сумму, на которую пользователь закупился
// если сумма больше или равна 1000, пользователь получает 5% скидку
// больше или равно 5000 - 10%
// больше или равно 10000 - 25%
// вывести скидку и результат
#include <iostream>
#include <string>
using namespace std;

int main()
{
	/*float sum = 0;
	cin >> sum;

	if (sum >= 10000) {
		cout << "25% - " << sum * 0.25 << "\nresult - " << sum - sum * 0.25;
	}
	else if (sum >= 5000) {
		cout << "10% - " << sum * 0.1 << "\nresult - " << sum - sum * 0.1;
	}
	else if (sum >= 1000) {
		cout << "5% - " << sum * 0.05 << "\nresult - " << sum - sum * 0.05;
	}*/


	float height = 1.51;
	/*if (height > 1.5) {
		cout << "Enjoy your ride";
	}
	else {
		cout << "You are too short";
	}*/
	cout << ((height > 1.5) ? "Enjoy your ride" : "You are too short");

	return 0;
}