// ООП Урок №6 Динамические структуры данных.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.

#include <iostream>
#include <string.h>
#include <time.h>

using namespace std;

class Stack
{
    // нижняя и верхняя границы
    enum { EMPTY = -1, FULL = 20 };

    // указатель на вершину
    int top;

    // массив для хранения данных
    char st[FULL + 1];

public:
    // конструктор
    Stack();

    // добавление элемента
    void Push(char c);

    // извлечение элемента
    char Pop();

    // очистка стека
    void Clear();

    // проверка существования элементов в стеке
    bool IsEmpty();

    // проверка на переполнение стека
    bool IsFull();

    // количество элементов в стеке
    int GetCount();
};
// Лушников
Stack::Stack()
{
    top = EMPTY;
}
// Кубанов
void Stack::Clear()
{
    top = EMPTY;
}
// Назарян
bool Stack::IsEmpty()
{
    return top == EMPTY;
    
}
// Юнусов
bool Stack::IsFull()
{
    return top == FULL;
}
// Метелицин
int Stack::GetCount()
{
    return top + 1;
}
// Золин
void Stack::Push(char c)
{
    // если в стеке есть место, то увеличиваем указатель
    // на вершину стека и вставляем новый элемент
    if (!IsFull())
    {
        st[++top] = c;
    }
}
// Красицкий
char Stack::Pop()
{
    // если в стеке есть элементы, то возвращаем
    // верхний и уменьшаем указатель на вершину стека
    if (!IsEmpty()) 
    {
        return st[top--];
    }
    else 
    { 
        return 0; 
    }
}

class Queue
{
    // очередь
    int* wait;

    // максимальный размер очереди
    int maxQueueLength;

    // текущий размер очереди
    int queueLength;

public:
    // конструктор
    Queue(int m);

    // деструктор
    ~Queue();

    // добавление элемента
    void Push(int c);

    // извлечение элемента
    int Pop();

    // очистка очереди
    void Clear();

    // проверка существования элементов в очереди
    bool IsEmpty();

    // проверка на переполнение очереди
    bool IsFull();

    // количество элементов в очереди
    int GetCount();

    // демонстрация очереди
    void Show();
};

Queue::Queue(int m)
{
    // получаем размер очереди
    maxQueueLength = m;
    // создаём очередь
    wait = new int[maxQueueLength];
    // изначально очередь пуста
    queueLength = 0;
}

Queue::~Queue()
{
    delete[] wait;
}

void Queue::Clear()
{
    queueLength = 0;
}

bool Queue::IsEmpty()
{
    return queueLength == 0;
}

bool Queue::IsFull()
{
    return queueLength == maxQueueLength;
}

int Queue::GetCount()
{
    return queueLength;
}

void Queue::Push(int c)
{
    // если есть свободное место, то увеличиваем количество значений
    // и добавляем новый элемент
    if (!IsFull())
    {
        wait[queueLength++] = c;
    }
}

int Queue::Pop()
{
    // если есть элементы, то возвращаем тот,
    // который вошёл первым и сдвигаем очередь
    if (!IsEmpty())
    {
        // запомнить первого
        int temp = wait[0];

        // сдвигаем элементы в очереди
        for (size_t i = 1; i < queueLength; i++)
        {
            wait[i - 1] = wait[i];
        }

        // уменьшаем количество
        queueLength--;

        // возвращаем первого
        return temp;
    }
    else
    {
        return -1;
    }
}

void Queue::Show()
{
    cout << "_______________________________________________________________________" << endl;
    for (size_t i = 0; i < queueLength; i++)    
    {
        cout << wait[i] << " ";
    }
    cout << "_______________________________________________________________________" << endl;
}

int main()
{
    setlocale(LC_ALL, "");

    srand(time(0));
    Stack st;
    char c;
    while (!st.IsFull())
    {
        c = rand() % 4 + 2;
        st.Push(c);
    }
    while (!st.IsEmpty())
    {
        cout << st.Pop() << " ";
    }
    cout << endl << endl;

    return 0;
}