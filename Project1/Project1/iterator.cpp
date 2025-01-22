#include <iostream>
using namespace std;

int main()
{
	int i = 0;
	while (i < 10) {
		int j = 9 - i;
		while (j > 0) {
			cout << ' ';
			j--;
		}
		j = 0;
		while (j < i) {
			cout << "**";
			j++;
		}
		cout << "*\n";
		i++;
	}
	
	/*
    -
   ---
  -----
 -------
---------
*/

	return 0;
}