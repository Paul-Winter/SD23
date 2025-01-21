// ConsoleApplication1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

//Задание 2. Написать имитацию кассового аппарата для
//магазина, торгующего новогодними товарами.Кассир
//должен выбрать товар из списка, ввести его количество,
//затем выбрать следующий товар.По завершению ввода
//вывести на экран всю сумму покупки.Предусмотреть
//наличие скидки.В списке товаров должно быть не меньше
//4 - х товаров, должна отображаться их цена.Предусмотреть
//неправильно вводимые данные.
//■ Реализовать возможность обслуживания нескольких
//клиентов подряд;
//■ Хранение общей выручки магазина;
//■ Ограничить количество товара в магазине

#include <iostream>
using namespace std;

int main()
{
	setlocale(LC_ALL, "rus");
	
	int total = 0, a=10, b=10, c=10, d=10;
	bool continueService = true;
	while (continueService) {
		int totalCoast = 0;
		bool shopping = true;
		char choice;
		cout << "Добро пожаловать в наш магазин, выберете товар из списка" << endl;
		cout << "A. Елочная игрушка - 100 рублей " << "остаток: " << a << endl;
		cout << "B. Гирлянда - 200 рублей " << "остаток: " << b << endl;
		cout << "C. Подарочная упаковка - 250 рублей " << "остаток: " << c << endl;
		cout << "D. Салют - 300 рублей " << "остаток: " << d << endl;
		cout << "Введите 0 для завершения" << endl;
		while (shopping) {
			cout << "Введите ваш выбор товара ";
			cin >> choice;
			if (choice == '0') {
				shopping = false;
				break;
			}
			int quantity;
			cout << "Введите кол-во товаров ";
			cin >> quantity;
			if (quantity <= 0) 
			{
				cout << "Введите верное значение" << endl;
				continue;
			}
			switch (choice)
			{
			case 'A':
				if (a>=quantity) {
					totalCoast += quantity * 100;
					a -= quantity;
				}
				else
				{ 
					cout << "Не достаточно товара на складе, доступно: " << a << endl;

				}
				break;
			case 'B':
				if (b >= quantity) {
					totalCoast += quantity * 200;
					b -= quantity;
				}
				else
				{
					cout << "Не достаточно товара на складе, доступно: " << b << endl;
				}
				break;
			case 'C':
				if (c >= quantity) {
					totalCoast += quantity * 250;
					c -= quantity;
				}
				else
				{
					cout << "Не достаточно товара на складе, доступно: " << c << endl;
				}
				break;
			case 'D':
				if (d >= quantity) {
					totalCoast += quantity * 300;
					d -= quantity;
				}
				else
				{
					cout << "Не достаточно товара на скалде, доступно: " << d << endl;
				}
				break;

			default:
				cout << "Введите верное значение " << endl;
				break;
			}
			
		}
		if (totalCoast >= 500) {
			cout << "Вам предоставляется скидка в размере 10% " << endl;
			totalCoast = totalCoast * 90 / 100;
		}
		total += totalCoast;
			char nextCustomer;
			cout << "Обслужить нового покупателя? " << " Y - Да " << " N - Нет " << endl;
			cin >> nextCustomer;
				if (nextCustomer == 'N') {
					continueService = false;
				}
	}   
	cout << "Общая выручка магазина " << total << endl;
}

