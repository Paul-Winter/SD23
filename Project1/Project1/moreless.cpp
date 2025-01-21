#include <iostream>
using namespace std;

int main()
{
	srand(time(0));

	for (int i = rand() % 10; i > 0; i--)
	{
		rand();
	}

	int target = rand() % 100 + 1;
	int u = 0;

	for (int i = 0; i < 6; i++) {
		cout << "Guess a number from 1 to 100:";
		cin >> u;

		if (target > u) {
			cout << "Higher\n";
		}
		else if (target < u) {
			cout << "Lower\n";
		}
		else {
			cout << "You win!\n";
			return 0;
		}
	}
	cout << "You lose!\n";


	return 0;
}
