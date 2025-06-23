#pragma once

#include <iostream>

using namespace std;

class DynArray
{
	friend ostream& operator<<(ostream&, const DynArray&);
	friend istream& operator>>(istream&, DynArray&);

public:
	explicit DynArray(int = 10);
	DynArray(const DynArray&);
	~DynArray();
	int length() const;

	const DynArray& operator=(const DynArray&);
	bool operator==(const DynArray&) const;
	bool operator!=(const DynArray& a) const
	{
		return !(*this == a);
	}
	int& operator[](int);
	int operator[](int) const;

private:
	int size;
	int* dynArr;
};
