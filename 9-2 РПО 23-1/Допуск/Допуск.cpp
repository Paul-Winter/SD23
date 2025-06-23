// Допуск.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
using namespace std;
int main()
{
    setlocale(LC_ALL, "");
    int a ;
    cin >> a;
    
    for (int i = 0;i < a;i++)
        
    {
    

        if (a % 2 == 0)
            cout << "привет\n";

        else if (a % 2 != 0)
            cout << "мир \n";

       
           
    } 
    
    cout << "привет мир!\n";

}

