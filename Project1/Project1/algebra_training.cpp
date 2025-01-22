#include <iostream>
using namespace std;

int main()
{
	srand(time(0));

	for (int i = rand() % 10; i > 0; i--)
	{
		rand();
	}

	int start = time(0);

	for (int i = 0; i < 10; i++)
	{
		int a = rand() % 8 + 2;
		int b = rand() % 8 + 2;
		int result = a * b;
		int u = 0;
		cout << a << " * " << b << " = ";
		cin >> u;
		if (result == u) {
			cout << "Correct!\n";
		}
		else {
			cout << "Incorrect. The answer is: " << result << '\n';
		}
	}
	int finish = time(0);
	cout << finish - start << " seconds";

	return 0;
}