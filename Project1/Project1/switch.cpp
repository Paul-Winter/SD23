#include <iostream>
using namespace std;

int main()
{
	while (true) {
		system("cls");
		float A = 0, B = 0, RES = 0;
		cout << "Enter first digit:\n";
		cin >> A;
		cout << "Enter second digit:\n";
		cin >> B;
		// реализация программного меню
		char key = '\0';
		cout << "\nSelect operator:\n";
		cout << "\n + - if you want to see SUM.\n";
		cout << "\n - - if you want to see DIFFERENCE.\n";
		cout << "\n * - if you want to see PRODUCT.\n";
		cout << "\n / - if you want to see QUOTIENT.\n";
		// ожидание выбора пользователя
		cin >> key;

		//if (key == '+') { // если пользователь выбрал
		//// сложение
		//	RES = A + B;
		//	cout << "\nAnswer: " << RES << "\n";
		//}
		//else if (key == '-') { // если пользователь выбрал
		//// вычитание
		//	RES = A - B;
		//	cout << "\nAnswer: " << RES << "\n";
		//}
		//else if (key == '*') { // если пользователь выбрал
		//// умножение
		//	RES = A * B;
		//	cout << "\nAnswer: " << RES << "\n";
		//}
		//else if (key == '/') { // если пользователь
		//// выбрал деление
		//	if (B) { // если делитель не равен нулю
		//		RES = A / B;
		//		cout << "\nAnswer: " << RES << "\n";
		//	}
		//	else { // если делитель равен нулю
		//		cout << "\nError!!! Divide by "
		//			"null!!!!\n";
		//	}
		//}
		//else { // если введенный символ некорректен
		//	cout << "\nError!!! This operator isn't correct\n";
		//}

		switch (key)
		{
		case '+':
		{
			RES = A + B;
			cout << "\nAnswer: " << RES << "\n";
			break;
		}
		case '-':
		{
			RES = A - B;
			cout << "\nAnswer: " << RES << "\n";
			break;
		}
		case '*':
		{
			RES = A * B;
			cout << "\nAnswer: " << RES << "\n";
			break;
		}
		case '/':
		{
			if (B) { // если делитель не равен нулю
				RES = A / B;
				cout << "\nAnswer: " << RES << "\n";
			}
			else { // если делитель равен нулю
				cout << "\nError!!! Divide by null!!!!\n";
			}
			break;
		}
		default:
		{
			cout << "\nError!!! This operator isn't correct\n";
			break;
		}
		}

		system("pause");
	}

	return 0;
}