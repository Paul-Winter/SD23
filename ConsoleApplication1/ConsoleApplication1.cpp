//// ConsoleApplication1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
////
//
//#include <iostream>
//#include <ctime>
//#include <cstring>
//
//using namespace std;
//
//void fun(int**& mas /*ссылка на двум. дин.массив*/
//	, int n //кол-во строк
//	,int m /*кол - во столбцов*/)
//{
//	// вспомогательный массив, для копирования 
//	// элементов основного
//	int** b = new int* [n];
//	for (int i = 0;i < n;i++)
//	{
//		b[i] = new int[m];
//	}
//
//	//копируем элементы основного массива в вспомагательный
//	for (int i = 0;i < n;i++)
//		for (int j = 0;j < m;j++)
//			b[i][j] = mas[i][j];
//
//	//освобождаем память основного массива
//	for (int i = 0;i < n;i++)
//		delete[] mas[i];
//	delete[] mas;
//	mas = NULL;
//
//	//перераспределяем память(на 1 строку больше)
//	mas = new int* [n + 1];
//	for (int i = 0;i < (n + 1);i++)
//		mas[i] = NULL;
//	for (int i = 0;i < (n + 1);i++)
//		mas[i] =new int[m];
//
//	// копируем b в mas
//	for (int i = 0;i < n;i++)
//		for (int j = 0;j < m;j++)
//			mas[i][j] =b[i][j];
//	//заполняем последню строку в mas
//	for (int j = 0;j < m;j++)
//		mas[n][j] = rand() % 100;
//
//	//очищаем память массива b
//	for (int i = 0;i < n;i++)
//		delete[] b[i];
//	delete[] b;
//
//
//}
//
//
//
//
//int main()
//{
//	setlocale(LC_ALL, "rus");
//	srand(time(NULL));
//	int N,M;
//	cout << "Введите кол-во строк ";
//	cin >> N;
//	cout << "Введите кол-во cтобцов ";
//	cin >> M;
//	
//
//	int** mas = new int*[N];//создание двум. дин.массива
//	for (int i = 0;i < N;i++)
//	{
//		mas[i] = new int[M];
//	
//	}
//
//
//	for (int i = 0; i < N;i++)//заполнили, вывели
//	{
//		for (int j = 0;j < M;j++)
//		{
//			mas[i][j] = rand() % 20;
//			cout << mas[i][j] << " ";
//		}
//		
//		cout << endl;
//	}
//
//	cout << endl;
//	cout << endl;
//
//	fun(mas, N, M);//вызов фнк
//
//	for (int i = 0; i < N+1;i++)// вывод массива
//	{
//		for (int j = 0;j < M;j++)
//		{
//			cout << mas[i][j] << " ";
//		}
//
//		cout << endl;
//	}
//
//
//
//	for (int i = 0;i < N+1;i++) //очищение памяти
//	{
//		delete[]mas[i]; 
//	}
//	delete[]mas;
//	return 0;
//}
//
//
