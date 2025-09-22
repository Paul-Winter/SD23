#include <iostream>

using namespace std;

template <class T>
void sort(T array[], size_t size);

void swap(int* x, int* y);

template <class T>
void display(T array[], size_t size);

template <class T>
class Array
{
    static const size_t size{ 5 };
    T array[size];
public:
    Array()
    {
        for (size_t i = 0; i < size; i++)
        {
            array[i] = T();
        }
    }

    int getSize() const
    {
        return size;
    }

    T getItem(size_t index) const
    {
        if (index >= 0 && index < size)
        {
            return array[index];
        }
        else
        {
            cout << "Index is out of range!" << endl;
            exit(1);
        }
    }

    void setItem(size_t index, T value)
    {
        if (index >= 0 && index < size)
        {
            array[index] = value;
        }
        else
        {
            cout << "Index is out of range!" << endl;
            exit(1);
        }
    }

    void display()
    {
        for (size_t i = 0; i < size; i++)
        {
            cout << array[i] << " ";
        }
        cout << endl;
    }

    void sort()
    {
        for (size_t i = size - 1; i > 0; i--)
        {
            for (size_t j = 0; j < i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    swap(array[j], array[j + 1]);
                }
            }
        }
    }
};

template <typename T1, typename T2>
struct Pair
{
    T1 first;
    T2 second;
    Pair() : first(T1()), second(T2()) {}
    Pair(const T1& first, const T2& second) : first{first}, second{second} {}

    bool operator==(const Pair& pair)
    {
        return this->first == pair.first && this->second == pair.second;
    }
    bool operator!=(const Pair& pair)
    {
        return !(*this == pair);
    }

    void display()
    {
        cout << "(" << first << "," << second << ")";
    }
};

int main()
{
    setlocale(LC_ALL, "");

    cout << "____________________________________________________Generic_function____________________________________________________" << endl;

    int intArray[]{ 1, 3, 5, 8, 2, 7, -3, -2, 4, -1 };
    size_t sizeI = sizeof(intArray) / sizeof(int);
    cout << "Array int before sort" << endl;
    display(intArray, sizeI);
    sort(intArray, sizeI);
    cout << "Array int after sort" << endl;
    display(intArray, sizeI);
    cout << endl;

    double doubleArray[]{ 7.8, -0.1, 1.2, 3.4, 5.6, 2.3, 4.5, 0.1, 8.9, 6.7 };
    size_t sizeD = sizeof(doubleArray) / sizeof(double);
    cout << "Array double before sort" << endl;
    display<double>(doubleArray, sizeD);
    sort<double>(doubleArray, sizeD);
    cout << "Array double after sort" << endl;
    display<double>(doubleArray, sizeD);
    cout << endl;

    char charArray[]{ 't', 'o', 'f', 's', 'h', 'e', 'w', 'p', 'a', 'c', 'b', 'y', 'x'};
    size_t sizeC = sizeof(charArray) / sizeof(char);
    cout << "Array char before sort" << endl;
    display<char>(charArray, sizeC);
    sort<char>(charArray, sizeC);
    cout << "Array char after sort" << endl;
    display<char>(charArray, sizeC);
    cout << endl;

    string stringArray[]{ "Metelitcin", "Nazaryan", "Yunusov", "Antonov", "Zolin", "Kubanov", "Dukhina", "Alexandrova", "Chavychalov", "Krasitckiy", "Zemlyanskiy", "Mamontova", "Lushnikov"};
    size_t sizeS = sizeof(stringArray) / sizeof(string);
    cout << "Array string before sort" << endl;
    display<string>(stringArray, sizeS);
    sort<string>(stringArray, sizeS);
    cout << "Array string after sort" << endl;
    display<string>(stringArray, sizeS);
    cout << endl;

    string stringArray2[]{ "Метелицин", "Назарян", "Юнусов", "Антонов", "Золин", "Кубанов", "Духина", "Александрова", "Чавычалов", "Красицкий", "Землянский", "Мамонтова", "Лушников" };
    size_t sizeS2 = sizeof(stringArray2) / sizeof(string);
    cout << "Array string before sort" << endl;
    display<string>(stringArray2, sizeS2);
    sort<string>(stringArray2, sizeS2);
    cout << "Array string after sort" << endl;
    display<string>(stringArray2, sizeS2);
    cout << endl << endl;
    
    cout << "____________________________________________________Generic_class____________________________________________________" << endl;
    
    Array<int> arrayInt;
    cout << "arrayInt initialization" << endl;
    arrayInt.display();
    int sizeInt = arrayInt.getSize();
    for (size_t i = sizeInt; i > 0; i--)
    {
        arrayInt.setItem(sizeInt - i, i + 10);
    }
    arrayInt.display();
    cout << "arrayInt sorting" << endl;
    arrayInt.sort();
    arrayInt.display();
    cout << endl;

    Array<string> arrayString;
    cout << "arrayString initialization" << endl;
    arrayString.display();
    int sizeString = arrayString.getSize();
    arrayString.setItem(0, "two");
    arrayString.setItem(1, "seven");
    arrayString.setItem(2, "one");
    arrayString.setItem(3, "four");
    arrayString.setItem(4, "zero");
    arrayString.display();
    cout << "arrayString sorting" << endl;
    arrayString.sort();
    arrayString.display();
    cout << endl << endl;

    Pair<int, int> point1(0, 0);
    Pair<int, int> point2(4, 5);
    point1.display();
    cout << " ";
    point2.display();
    cout << " ";
    cout << (point1 == point2 ? "equals" : "not equals") << endl;
    Pair<int, int> point3;
    point3.display();
    cout << " ";
    cout << (point1 == point3 ? "equals" : "not equals") << endl;

    Pair<string, string> student("Ivanov", "Ivan");
    student.display();
    cout << endl;

    Pair<Pair<string, string>, int> mark2(student, 5);
    mark2.first.display();
    cout << " : " << mark2.second << endl;

    return 0;
}

template <class T>
void sort(T array[], size_t size)
{
    for (size_t i = size - 1; i > 0; i--)
    {
        for (size_t j = 0; j < i; j++)
        {
            if (array[j] > array[j + 1])
            {
                swap(array[j], array[j + 1]);
            }
        }
    }
}

void swap(int* x, int* y)
{
    int temp = *x;
    *x = *y;
    *y = temp;
}

template <class T>
void display(T array[], size_t size)
{
    for (size_t i = 0; i < size; i++)
    {
        cout << array[i] << " ";
    }
    cout << endl;
}
