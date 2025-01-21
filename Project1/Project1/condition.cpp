#include <iostream>
using namespace std;

int main()
{
	// == - сравнение
	// != - не равно
	// >, <, >=, <=

	int a = 0;

	cout 
		<< "1 - Coffee\n"
		<< "2 - Tea\n"
		<< "3 - Lemonade\n";

	cin >> a;

	if (a == 1) {
		system("cls"); // функция system вызывает команду cls - очищает текущий экран консоли
		int b = 0;
		cout 
			<< "1 - with milk\n"
			<< "2 - with cream\n"
			<< "3 - bitter\n";
		cin >> b;

		if (b == 1) {
			cout << "milk ";
		}
		if (b == 2) {
			cout << "cream ";
		}
		if (b == 3) {
			cout << "bitter ";
		}

		cout << "coffee\n";
	} 
	else if (a == 2) {
		cout << "Tea\n";
	}
	else if (a == 3) {
		cout << "Lemonade\n";
	}
	else {
		cout << "Error: Incorrect input. No such drink exists";
	}


	return 0;
}