#include <iostream>
using namespace std;

// Сделать ресторанное меню из 5 блюд
// принять ввод пользователя
// вывести на экран стоимость выбранного блюда

enum
{
	FRIED_CHICKEN = 1,
	W_SALAD,
	E_N_B,
	S_B,
	WATER
};

int main()
{
	/*const int FRIED_CHICKEN = 1,
		W_SALAD = 2,
		E_N_B = 3,
		S_B = 4,
		WATER = 5;*/

	cout << "Welcome to our restaurant!\n"
		<< "Here's what we can get you:\n"
		<< FRIED_CHICKEN << ". Fried chicken\n"
		<< W_SALAD << ". Watermelon salad\n"
		<< E_N_B << ". Eggs'n'bacon\n"
		<< S_B << ". Steamin' boat\n"
		<< WATER << ". Water\n"
		<< "Make your order:";

	bool flag = true;
	int sum = 0;
	while (flag) {
		int order = 0;
		cin >> order;
		switch (order)
		{
		case FRIED_CHICKEN: {
			cout << "Fried chicken - 25$\n";
			sum += 25;
			break;
		}
		case W_SALAD: {
			cout << "Watermelon salad - 10$\n";
			sum += 10;
			break;
		}
		case E_N_B: {
			cout << "Eggs'n'bacon - 12$\n";
			sum += 12;
			break;
		}
		case S_B: {
			cout << "Steamin' boat - 9000$\n";
			sum += 9000;
			break;
		}
		case WATER: {
			cout << "Water - free\n";
			break;
		}
		case 0: {
			cout << "Goodbye\n";
			flag = false;
			break;
		}
		default: {
			cout << "Sorry, we don't serve that\n";
			break;
		}
		}
	}

	cout << "Total: " << sum << "$";

	return 0;
}