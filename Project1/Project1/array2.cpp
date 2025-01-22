#include <iostream>
using namespace std;

int main()
{
	// сделать массив неотрицательных целых чисел "поезд" - train
	// размер - 6 (вывести в отдельную константу)
	// записать базовые значения - 1, 13, 24, 41, 32, 19
	// найти и вывести сумму всех пассажиров
	// найти и вывести среднее арифметическое всех пассажиров


	const int SIZE = 6;
	unsigned train[SIZE] = { 1, 13, 24, 41, 32, 19 };
	
	int sum = 0, min = train[0], max = train[0];

	for (int i = 0; i < SIZE; i++)
	{
		sum += train[i];

		if (train[i] < min) {
			min = train[i];
		}
		if (train[i] > max) {
			max = train[i];
		}
	}

	cout << "SUM: " << sum << " AVG: " << sum / SIZE
		<< "\nMIN: " << min << " MAX: " << max;

	return 0;
}