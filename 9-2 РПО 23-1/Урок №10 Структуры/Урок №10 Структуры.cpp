// Урок №10 Структуры.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

using namespace std;

struct date
{
    //bool isWeekend;
    int day;
    int month;
    int year;
    //int weekday;
    //char day_name[10];
    //char month_name[10];
};

struct time_s
{
    int hours;
    int minutes;
    int seconds;
    //int milliseconds;
};

struct timestamp
{
    time_s time;
    date date;
};

struct address
{
    char city[20];
    char street[30];
    int house;
    char liter;
};

struct tovar
{
    char tovar_name[100];
    double price;
    int count;
};

struct check
{
    char mall_name[10];
    address address;
    tovar tovar;
    double total_price;
    int total_count;
    int sale;
    timestamp date_time;
};

struct
{
    int x;
    int* y;
} *p;

struct test
{
    //int i;
    char ch;
};

int main()
{
    setlocale(LC_ALL, "");

    //date today = { false, 16, 12, 2024, 1, "Monday", "December" };
    std::cout << "________________________MY CHECK________________________" << endl;
    //cout << "Weekend? - " << today.isWeekend << endl;
    //cout << "Date: " << today.day << "." << today.month << "." << today.year << endl;
    //cout << "Weekday №" << today.weekday << "\t" << today.day_name << "\t" << today.month_name << endl;

    timestamp ts = { 13, 30, 33, 19, 12, 2024 };
    tovar tov = {"Cognac", 2115.00, 1};
    int sale = 15;
    address addr = { "Сыктывкар", "Сысолькое шоссе", 27, NULL };
    check check1 = { "LENTA", addr, tov, (tov.count * tov.price) - (tov.count * tov.price / sale), tov.count, sale, ts};
    std::cout << check1.mall_name << endl;
    std::cout << check1.address.city << "\n" << "_____________________ПРОДАЖА_ТОВАРА_____________________" << endl;
    std::cout << tov.tovar_name << "\t" << tov.count << "\t" << tov.price << endl;
    std::cout << "========================================================" << endl;
    std::cout << "ЦЕНА: " << check1.total_price << endl;
    std::cout << ts.date.day << "." << ts.date.month << "." << ts.date.year << ":" << ts.time.hours << "." << ts.time.minutes << endl;

    std::cout << "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" << endl << endl;
    //std::cout << (++p)->x;      // увеличение р до доступа к х
    //std::cout << p++->x;        // увеличение р после доступа к х
    //std::cout << *p->y;         // выбор значения на который указывает у
    //std::cout << *p->y++;       // увеличение у после обработки того, на что он указывает
    //std::cout << *p++->y;       // увеличение р после выборки того, на что указывает у
    //std::cout << (*(*p).y)++;   // увеличение того, на что указывает у

    int a;
    char c;
    double d;
    int* p;

    std::cout << "sizeof(a) = " << sizeof(a) << "\n";
    std::cout << "sizeof(c) = " << sizeof(c) << "\n";
    std::cout << "sizeof(d) = " << sizeof(d) << "\n";
    std::cout << "sizeof(p) = " << sizeof(p) << "\n";

    test test;

    std::cout << "sizeof(test) = " << sizeof(test) << endl;

    return 0;
}