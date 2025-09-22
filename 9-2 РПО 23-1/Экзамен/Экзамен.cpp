#include <iostream>
#include <stdlib.h>
#include <time.h>
using namespace std;

int main()
{
    setlocale(LC_ALL, "");
    cout << "Номер планеты:";
    int p;
    cin >> p;
    
    if (p == 4)
    {
        cout << "Марс: Фобос, Деймос";
    }

    else if (p == 3)
    {
        cout << "Земля: Луна";
    }

    else if (p == 2)
    {
        cout << "Венера: -";
    }
    else if (p == 1)
    {
        cout << "Меркурий: -";
    }
    else
    {
        cout << "Планеты нет в списке";
    }
    return 0;
}

        

    
        
    

    

    

    


