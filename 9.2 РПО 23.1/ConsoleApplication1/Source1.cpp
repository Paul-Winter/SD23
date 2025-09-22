#include <iostream>
#include <ctime>
#include <cstring>
using namespace std;
int fun()
{
	cout << 3;
	return 3;
}

int fun1()
{
	cout << 7;
	return 7;
}


int main()
{
	using namespace std;
	setlocale(LC_ALL, "rus");
	srand(time(NULL));
	
	/*enum menuItems {ENTER_LOGIN=1, OUTPUT_LOGIN=2,QUIT=3};

	int userChoice;
	char userLogin[30] = "admin";
	
	do {
		cout << "Сделайте выбор" << endl;
		cout << "1 - Ввести логин" << endl;
		cout << "2 - Посмотреть логин" << endl;
		cout << "3 - Выход" << endl;
		
		cin >> userChoice;

		switch (userChoice) {
		case ENTER_LOGIN:
		{
			cout << "Введите новый логин ";
			cin >> userLogin;
			break;
		}
		case OUTPUT_LOGIN:
		{
			cout << "логин " << userLogin << endl;
			break;
		}

		case QUIT:
		{
			cout << "Пока пока" << endl;
			break;
		}

		default:
		{
			cout << "Нет такого варианта" << endl;
			break;
		}
		}

	} while (userChoice != 3);*/

	int(*Ptr)();
	Ptr = fun;
	(*Ptr)();//вызов фнкц по указателю
	fun(); //вызов фнкц по имени
	Ptr();

	
}