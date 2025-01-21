#include <iostream>
using namespace std;

#include <io.h>
#include <fcntl.h>


int main() {
	_setmode(_fileno(stdout), _O_WTEXT);
	_setmode(_fileno(stdin), _O_WTEXT);

	wcout << L"привет";
	wchar_t s;
	wcin >> s;
	wcout << s;


	return 0;
}