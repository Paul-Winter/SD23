#include <iostream>

using namespace std;

int main()
{
	int Age = 34;
	int age;
	const double pi = 3.1415926;

	age = 17;

	cout << Age;
	cout << "\n";
	cout << pi;
	cout << "\n";
	cout << "variable age = " << age << "\n";
	cout << "constant pi = " << pi << "\n";

	//1. объ€вление и инициализаци€ переменных и констант
	int DaysIn_2000Year = 366;
	int HourInDay = 24;
	int HoursIn_2000Year;

	//2. подсчЄт результатов
	HoursIn_2000Year = DaysIn_2000Year * HourInDay;

	//3. вывод результата на экран
	cout << "\n";
	cout << "\t\tIn 2000 year " << HoursIn_2000Year << " hours\n";
	return 0;
}