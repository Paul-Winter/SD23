#include <iostream>
#include <windows.h>
using namespace std;

int main()
{
	int floor = 1;
	int target = 1;

	while (1) {
		if (floor == target) {
			cout << "Current floor: " << floor << '\n'
				 << "Where to:";
			cin >> target;
		}
		if (floor < target) {
			floor++;
		}
		else if (floor > target) {
			floor--;
		}
		cout << floor << '\n';
		Sleep(300);
	}
	return 0;
}