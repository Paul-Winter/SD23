#include <iostream>
using namespace std;

int main()
{
	int train[20];
	int train2[] = {5, 15, 20};
	int train3[20] = {5, 15, 20};

	//int i = 2;

	//cout << train2[i];

	//train[2];

	/*
	train[0]	0000 0101
	train[1]	0000 1111
	train[2]	0001 0100
	*/


	for (int i = 0; i < 3; i++)
	{
		cout << train2[i] << '\n';
	}
	return 0;
}