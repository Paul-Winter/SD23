#include <iostream>
using namespace std;

int main()
{
	const int SIZE = 5;
	int grades[SIZE];
	grades[0] = 5;
	grades[1] = 4;
	grades[2] = 3;
	grades[3] = 5;
	grades[4] = 4;

	/*for (int i = 0; i < SIZE; i++)
	{
		cout << "Student " << i + 1 << ": " << grades[i] << '\n';
	}*/

	while (true)
	{
		cout << "Enter a student's index from 1 to " << SIZE << ':';
		int u;
		cin >> u;
		if ((u < 1) || (u > SIZE)) {
			cout << "Invalid index\n";
			continue;
		}
		cout << "Grade of student " << u << " is: " << grades[u - 1] << '\n';
	}

	return 0;
}