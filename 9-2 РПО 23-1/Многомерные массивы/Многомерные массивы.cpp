// 1) конспект
// 2) дан двумерный массив размерностью 3х4.
//    Необходимо найти количество элементов значение которых равно нулю.
// 3) Дана квадратная матрица порядка n (n строк, n столбцов).
//    Найти наибольшее из значений элементов, расположенных в закрашенных частях матрицы
//    (согласно варианта)
// 4) Все массивы заполняются случайным образом.
//  Александрова - 10
//  Антонов - 4
//  Духина - 8
//  Землянский - 2
//  Золин - 1
//  Красицкий - 6
//  Кубанов - 5
//  Лушников - 7
//  Мамонтова - 8
//  Метелицин - 9
//  Назарян - 3
//  Чавычалов - 10
//  Юнусов - 2

#include <iostream>
#include <stdlib.h>
#include <time.h>
using namespace std;

int main()
{
    setlocale(LC_ALL, "");
    srand(time(NULL));
    
    const int row = 3;
    const int col = 4;

    int array[row][col];
    int yusuf = 0;
    for (int i = 0; i < row; i++)
    {
        for (int j = 0; j < col; j++)
        {
            array[i][j] = rand() % (5 - (-5)) + (-5);
            if (array[i][j] == 0) {
                yusuf++;
                
            }
            cout << array[i][j] << " ";

        }
        cout << endl;
    }
    cout << "всего нулей в массиве " << yusuf << endl;
    
    const int n = 5;
    int array1[n][n];
    int max_num = array[0][0];

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            array1[i][j] = rand() % (5 - (-5)) + (-5);
            cout << array1[i][j] << "\t";
        }
        cout << endl;
    }

    for (int i = 0; i < n; i++)
    {
        for (int j = n; j < 0; j--)
        {
            {
                if (i + j < 6 && max_num < array1[i][j])
                {
                    max_num = array1[i][j];
                    cout << max_num;
                }
            }
        }
    }

    cout << "Наибольшая переменная равна " << max_num;
    return 0;
}
