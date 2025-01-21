#pragma once
#include<iostream>
#include<ctime>
using namespace std;

void Set(int* arr, int size)
{ 
	srand(NULL);
	for (int i = 0;i < size;i++)
	{
		arr[i] = rand() % 100;
	}
}

void Show(int* arr, int size)
{
	for (int i = 0;i < size;i++)
	{
		cout << arr[i] << " ";
	}
}

int Max(int* arr, int size)
{
	int max = 0;
	for (int i = 0;i < size;i++)
	{
		if (arr[i] > max)
			max = arr[i];
	}
	return max;
}

int Min(int* arr, int size)
{
	int min = 100;
	for (int i = 0;i < size;i++)
	{
		if (arr[i] < min)
			min = arr[i];
	}
	return min;
}

