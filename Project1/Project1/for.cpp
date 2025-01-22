#include <iostream>
#include <windows.h>
using namespace std;

int main()
{
	/*for (int i = 0; i < 10; i++)
	{
		cout << i << '\n';
	}*/


	for (int i = 0; i < 10; i++)
	{
		for (int j = 9 - i; j > 0; j--)
		{
			cout << ' ';
		}
		for (int j = 0; j < i; j++)
		{
			cout << '*' << '*';
		}
		cout << '*' << '\n';
	}

	return 0;
}