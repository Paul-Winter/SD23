// Урок №4 Циклы.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

int main()
{
    setlocale(LC_ALL, "");

#pragma region Цикл с предусловием while

    //int counter = 0;
    //int students;
    //cout << "Введите количество студентов в группе:\t";
    //cin >> students;

    //while (counter < students)
    //{
    //    counter++;
    //    cout << "Проверяется ДЗ студента №" << counter << endl;
    //}

    //cout << "Поздравляю! Все ДЗ проверены!" << endl;

#pragma endregion

#pragma region Цикл с предусловием do...while

    /*float a;
    float b;
    float result;
    char operation;
    bool yetAnother = true;

    do
    {
        std::cout << "\n\t\t\tВозможные операции:";
        std::cout << "\n + - если хотите сложить";
        std::cout << "\n - - если хотите вычесть";
        std::cout << "\n * - если хотите умножить";
        std::cout << "\n / - если хотите разделить";
        std::cout << "\n другое - если хотите выйти из программы\n";
        std::cout << "\nПожалуйста, введите знак операции:\t";
        std::cin >> operation;
        std::cout << std::endl;
        std::cout << "Пожалуйста, введите первое число:\t";
        cin >> a;
        std::cout << std::endl;
        std::cout << "Пожалуйста, введите второе число:\t";
        cin >> b;

        switch (operation)
        {
        case '+': result = a + b;
            break;
        case '-': result = a - b;
            break;
        case '*': result = a * b;
            break;
        case '/': result = a / b;
            break;
        default: yetAnother = false; std::cout << "\n\nСпасибо, что воспользовались нашей программой! До свидания!\n\n";
            break;
        }
        std::cout << "\nОтвет: a " << operation << " b = " << result << "\n\n";
    } while (yetAnother);*/

#pragma endregion

#pragma region Цикл с постусловием for

    // FizzBuzz
    //cout << "FizzBuzz program" << endl;

    //// которая выводит числа
    //for (int i = 1; i <= 100; i++)
    //{
    //    // если число кратно и 3 и 5, то вместо него выводится "FizzBuzz"
    //    if (i % 15 == 0)
    //    {
    //        cout << "FizzBuzz" << endl;
    //    }
    //    // если число кратно 3, то вместо него в строку выводится "Fizz"
    //    else if (i % 3 == 0)
    //    {
    //        cout << "Fizz" << endl;
    //    }
    //    // если число кратно 5, то вместо него в строку выводится "Buzz"
    //    else if (i % 5 == 0)
    //    {
    //        cout << "Buzz" << endl;
    //    }
    //    // от 1 до 100 каждое в новой строке
    //    else
    //    {
    //        cout << i << endl;
    //    }
    //}

#pragma endregion

#pragma region Вложенные конструкции

    //Вложенные циклы
    int size;
    cout << "Введите размер стены:\t";
    cin >> size;
    cout << endl;
    for (int i = 1; i < size; i++)
    {
        for (int j = 1; j < size; j++)
        {
            if (i + j >= size && i >= j)
                cout << "|===|";
            else
                cout << "     ";
        }
        cout << endl;
    }
    cout << endl;
    for (int i = 1; i < size; i++)
    {
        for (int j = 1; j < size; j++)
        {
            cout << "|###|";
        }
        cout << endl;
    }

#pragma endregion

    return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
