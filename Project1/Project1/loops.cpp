#include <iostream>
using namespace std;

int main()
{
	/*int i = 0;
	while (i < 10)
	{
		cout << "\n" << i++;
	}*/

	/*char a = 0;

	while (a != '0' && a != '1')
	{
		cin >> a;
		cout << "You entered " << a << '\n';
	}

	char b = 0;

	while (true) {
		if (b == 'o') {
			break;
		}
		cin >> b;
		cout << "Now you entered " << b << '\n';
	}*/

	/*while (true) {
		int u = 0;
		cout << "Enter a number between 5 and 8:";
		cin >> u;
		if (u == 5) {
			cout << "5 is a mildly lucky number\n";
		}
		else if (u == 6) {
			cout << "6 is an unlucky number\n";
		}
		else if (u == 7) {
			cout << "7 is a lucky number\n";
		}
		else if (u == 8) {
			cout << "8 is an infinity number\n";
		}
		else {
			cout << "Your number doesn't fit within range, try again\n";
		}
	}*/

	bool flag = true;
	while (flag) {
		int u = 0;
		cout << "How are you doing today? (1 - good, 2 - not so, 3 - good enough):";
		cin >> u;
		switch (u)
		{
		case 1:
			cout << "Good for you!\n";
			break;
		case 2:
			cout << "Sorry to hear that!\n";
			flag = false;
			break;
		case 3:
			cout << "Okay!\n";
			break;
		default:
			cout << "I don't understand you\n";
			break;
		}
	}



	return 0;
}