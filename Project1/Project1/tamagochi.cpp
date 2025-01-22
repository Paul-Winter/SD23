#include <iostream>
using namespace std;

int main()
{
	string name;
	cout << "Enter the name of your pet:";
	cin >> name;

	bool run = false;
	do {
		system("cls");
		cout << "Your pet, " << name 
			<< ", is happily lying in its corner, wagging its tail\n"
			<< "What do you do?\n"
			<< "1. Pet your pet.\n"
			<< "2. Feed your pet.\n"
			<< "3. Walk your pet outside.\n"
			<< "4. Put your pet to sleep (exit).\n";
		//
		string u;
		cin >> u;
		if (u == "1") {
			cout << "You extend your hand and pet the head of " << name
				<< ", who happily accepts and rubs against your hand.\n";
		}
		else if (u == "2") {}
		else if (u == "3") {}
		else if (u == "4") {
			return 0;
			// return выходит из функции
			run = false; 
			// переменная-флаг не даёт зайти в цикл
			// в начале итерации
			//break; // break выходит из цикла, обрывая его
		}
		else {
			cout << "Wrong command...\n";
		}

		/*int u = 0;
		cin >> u;
		switch (u)
		{
		case 1: {
			cout << "You extend your hand and pet the head of " << name
				<< ", who happily accepts and rubs against your hand.\n";
			break;
		}
		case 2: {
			break;
		}
		case 3: {
			break;
		}
		case 4: {
			break;
		}
		default: {
			cout << "Wrong command...\n";
			break;
		}
		}*/


		system("pause");

	} while (run);

	return 0;
}