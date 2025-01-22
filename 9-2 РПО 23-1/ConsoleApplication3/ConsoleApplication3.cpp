//Задание 3. Написать программу «успеваемость».Пользователь вводит 10 оценок студента.Реализовать меню
//для пользователя
//■ Вывод оценок(вывод содержимого массива);
//■ Пересдача экзамена(пользователь вводит номер элемента массива и новую оценку);
//■ Выходит ли стипендия(стипендия выходит, если
//	средний бал не ниже 10.7).

#include <iostream>

using namespace std;

int main()
{
	setlocale(LC_ALL, "rus");

	const int n = 5;
	int a[n]{ 0,0,0,0,0 };
	int choice, number, mark, sum{ 0 };
	double sredbal;
	bool isWork = true;

	while (isWork)
	{

		cout << "Выберите действие:\n";
		cout << "1. Ввести оценки\n";
		cout << "2. Вывести оценки\n";
		cout << "3. Изменить оценку\n";
		cout << "4. Узнать, выходит ли стипендия\n";
		cout << "0. Выход\n";

		cin >> choice;

		switch (choice)
		{
		case 1:
		{
			cout << "Введите оценки: " << endl;
			for (int i = 0; i < n; ++i)
			{
				cin >> a[i];
			}
			cout << endl;
			break;
		}

		case 2:
		{
			for (int i = 0; i < n; ++i)
			{
				cout << a[i] << " ";
			}
			cout << endl;
			break;
		}

		case 3:
		{
			cout << "Введите номер оценки: ";
			cin >> number;

			cout << "Введите новую оценку: ";
			cin >> mark;

			a[number] = mark;
			break;
		}

		case 4:
		{
			for (int i = 0; i < n; ++i)
			{
				sum += a[i];
			}
			sredbal = sum / n;
			
			if (sredbal >= 10.7)
			{
				cout << "Стипендия выходит";
			}
			else
			{
				cout << "Стипендия не выходит";
			}
			break;
		}

		case 0:
		{
			cout << endl;
			isWork = false;
		}

		default:
		{
			cout << "Введите допустимое значение\n";
			break;
		}
		}
	}

	

	return 0;
}

