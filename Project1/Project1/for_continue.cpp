#include <iostream>
using namespace std;

int main()
{
	// !(i % 3) - fizz
	// !(i % 5) - buzz
	// !(i % 3) && !(i % 5) - fizzbuzz

	for (int i = 0; i < 100; i++)
	{
		if (!(i % 3) || !(i % 5)) {
			if (!(i % 3)) {
				cout << "fizz";
			}
			if (!(i % 5)) {
				cout << "buzz";
			}
			cout << '\n';
			continue;
		}
		
		cout << i << '\n';
	}

	return 0;
}
