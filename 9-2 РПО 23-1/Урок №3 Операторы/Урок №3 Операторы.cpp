// Урок №3 Операторы.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
using namespace std;

int main()
{
    setlocale(LC_ALL, "");
    
#pragma region Операторы

    //const float PI = 3.1415926;
    //float radius;
    //float circum;
    //float area;

    //cout << "Добро пожаловать в программу рассчитывающую окружности!\n\n";
    //cout << "Введите радиус окружности: ";
    //cin >> radius;
    //cout << "\n\n";

    //area = PI * radius * radius;
    //circum = PI * (radius * 2);

    //cout << "Площадь окружности: " << area << "\n\n";
    //cout << "Длина окружности: " << circum << "\n\n";
    //cout << "Спасибо! До свидания!\n";

#pragma endregion

#pragma region Преобразования типов

    //// сужающее преобразование
    //int x = 23.5;
    //cout << x;

    //cout << "\n";

    //// расширяющее преобразование
    //unsigned int y = 300000000;
    //cout << y;

    //cout << "\n";

    //// неявное преобразование
    //float a = 23.5;
    //cout << a;

    //cout << "\n";

    //// явное преобразование
    //double b = 37.4;
    //int z = (int)b;
    //cout << b << "***" << z;

#pragma endregion

#pragma region Унифицированная инициализация

    // списковая инициализация
    //int a = { 11 };
    //int b{ 33 };

    //int x = { 2 };
    //cout << x;

    //char ch = { 65 };
    //cout << ch;

    //double y = { 333 };
    //cout << y;

#pragma endregion

#pragma region Логические операторы

    // операторы сравнения
    // <  >   <=  >=
    //cout << (5 > 3);
    //cout << (3 < 2);

    // операторы равенства
    // ==   !=

    // логические операторы
    // &&   ||   !
    //int n;
    //cout << "Введите число: ";
    //cin >> n;
    //cout << "\n";
    //cout << ((n < 1) || (n > 10));
    //cout << "\n";
    //cout << "Если Вы видите \"0\", то ваше число входит в диапазон от 1 до 10\n";
    //cout << "Если Вы видите \"1\", то ваше число не входит в диапазон от 1 до 10\n";

    //cout << !(5 == 3);
    //cout << !(3 != 2);

#pragma endregion

#pragma region Операторы ветвления if else

    // конструкция логического выбора
    //int A, B;
    //cout << "Введите первое число ";
    //cin >> A;
    //cout << "\n";
    //cout << "Введите второе число ";
    //cin >> B;
    //cout << "\n";

    //int max = (B > A) ? B : A;
    //int min = (B < A) ? B : A;

    //cout << "\n";
    //cout << "Maximum is " << max << "\n";
    //cout << "Minimum is " << min << "\n";
    
    //(B != 0) ? cout << "Результат A / B = " << A / B << "\n" : cout << "Нельзя делить на ноль!\n";
    // 
    //if(B == 0)
    //{
    //    cout << "Нельзя делить на ноль!\n";
    //}
    //else
    //{
    //    cout << "Результат A / B = " << A / B << "\n";
    //}

    //int summa;
    //cout << "Enter summa: ";
    //cin >> summa;

    //if (summa > 1000)
    //{
    //    cout << "You have 25% discount!\n";
    //    cout << "You must pay " << summa - summa / 100 * 25;
    //}
    //else if (summa > 500)
    //{
    //    cout << "You have 10% discount!\n";
    //    cout << "You must pay " << summa - summa / 10;
    //}
    //else if (summa > 100)
    //{
    //    cout << "You have 5% discount!\n";
    //    cout << "You must pay " << summa - summa / 100 * 5;
    //}

#pragma endregion

#pragma region Структура множественного выбора

    //float a;
    //float b;
    //float result;
    //char operation;

    //cout << "Пожалуйста, введите первое число:\t";
    //cin >> a;

    //cout << "\nПожалуйста, введите знак операции:\n";
    //cout << "\n + - если хотите сложить";
    //cout << "\n - - если хотите вычесть";
    //cout << "\n * - если хотите умножить";
    //cout << "\n / - если хотите разделить\n";
    //cin >> operation;

    //cout << "Пожалуйста, введите второе число:\n";
    //cin >> b;

    //switch (operation)
    //{
    //case '+': result = a + b;
    //    break;
    //case '-': result = a - b;
    //    break;
    //case '*': result = a * b;
    //    break;
    //case '/': result = a / b;
    //    break;
    //default: cout << "\n\nОшибка ввода!\n\n";
    //        break;
    //}
    //cout << "\nОтвет: a " << operation << " b = " << result << "\n\n";

#pragma endregion

#pragma region Перечисления

    //const int USA = 1;
    //const int France = 33;
    //const int Russia = 7;
    //const int Italy = 39;
    //const int Australia = 61;
    
    enum countries
    {
        USA = 1,
        France = 33,
        Russia = 7,
        Italy = 39,
        Australia = 61
    };
    
    enum coins { penny = 1, nickel = 5, dime = 10, quarter = 25, half = 50, dollar_coin = 100 };
    
    int coin;
    cout << "Please, enter a value of american coin" << endl;
    cin >> coin;
    switch (coin)
    {
    case penny: cout << "penny = 1 cent" << endl; break;
    case nickel: cout << "nickel = 5 cent" << endl; break;
    case quarter: cout << "quarter = 25 cent or 5 nickel" << endl; break;
    case dime: cout << "dime = 10 cent or 2 nickel" << endl; break;
    case half: cout << "half = 50 cent or 10 nickel or 2 quarter" << endl; break;
    case dollar_coin: cout << "dollar = 100 cent or 20 nickel or 4 quarter or 2 half" << endl; break;
    default:
        cout << "error input" << endl;
        break;
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
