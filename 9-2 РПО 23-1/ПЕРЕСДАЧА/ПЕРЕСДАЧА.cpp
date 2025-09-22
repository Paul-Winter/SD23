#include <iostream>
#include<string>

using namespace std;


int main()
{
	setlocale(LC_ALL, "");
	int number;
	cout << "Ввидите целое число ";

	cin >> number;

	cout << "Постфексный дикримент" << endl;
	cout << "Исходное значения" << number << endl;
	cout << "постфексный дискримент{number--}" << number-- << endl;
	cout << "значения после постфакторного дискримента: " << number << endl;

	number++;

	cout << "\nПрефексный дискримент:" << number << endl;
	cout << "Префексный дискремент (--number):" << --number << endl;



	return 0;

}
 
