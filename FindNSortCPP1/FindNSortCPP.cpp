// FindNSortCPP.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

const int INT_ARRAY_SIZE = 8;
int findIndexOfInt(int x, int integers[]);
void intBubbleSort(int integers[], int size);


int main()
{
    // TODO
    // Добавить ввод элементов массива ints и задание направления сортировки пользователем\
    // через консоль
    int ints[] = {1, 20, 42, -80, 0, 10, 20, 5};
    /* int x = 1000;
    try
    {
        std::cout << findIndexOfInt(x, ints) << "\n";
    }
    catch (const std::exception& ex)
    {
        std::cout << ex.what() << std::endl;
    } */
    intBubbleSort(ints, INT_ARRAY_SIZE);
    for (int i = 0; i < INT_ARRAY_SIZE; i++)
    {
        std::cout << ints[i] << " ";
    }
}

/* Возвращает индекс первого попавшегося в массиве числа, равного заданному */
int findIndexOfInt(int x, int integers[])
{
    int i = 0;
    while (i < INT_ARRAY_SIZE)
    {
        if (integers[i] == x)
        {
            return i;
        }
        i++;
    }
    throw std::exception("Item not found!");
}

// TODO
// Используя пример из документации
// https://docs.microsoft.com/ru-ru/cpp/cpp/enumerations-cpp?view=msvc-170
// создать тип-перечисление "Order",
// допустимыми значениями которого будут ASC и DESC;
// используя это перечисление, добавить в функцию intBubbleSort параметр,
// при помощи которого будет задаваться направление сортировки
void intBubbleSort(int integers[], int size)
{
    bool sorted = false;
    do
    {
        sorted = true;
        for (int i = 0; i < size - 1; i++)
        {
            if (integers[i] < integers[i + 1])
            {
                int temp = integers[i];
                integers[i] = integers[i + 1];
                integers[i + 1] = temp;
                sorted = false;
            }
        }
    } while (!sorted);
}
