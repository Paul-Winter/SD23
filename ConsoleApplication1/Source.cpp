//#include <iostream>
//#include <ctime>
//#include <cstring>
//
//void Add(int**& mas, int n, int m, int k)
//{   
//	//создание нового массива дл€ копировани€ эл. из mas
//	int** b = new int* [n];
//	for (int i = 0;i < n;i++)
//	{
//		b[i]= new int [m];
//	}
//
//	//копирование эл из mas в b
//	for (int i = 0;i < n;i++)
//		for (int j = 0;j < m;j++)
//			b[i][j] = mas[i][j];
//
//	//освобождаем пам€ть в массиве mas
//	for (int i = 0;i < n;i++)
//		delete[]mas[i];
//	delete[]mas;
//	mas = NULL;
//
//	mas=new int* [n + 1];
//	for (int i = 0;i < (n + 1);i++)
//		mas[i] = NULL;
//
//	for (int i = 0;i < (n + 1);i++)
//		mas[i] = new int[m];
//
//	
//	for (int i = 0;i < k;i++)
//		for (int j = 0;j < m;j++)
//			mas[i][j] = b[i][j];
//
//	for (int j = 0;j < m;j++)
//		mas[k][j] = rand() % 100;
//
//
//	for (int i = k;i < n;i++)
//		for (int j = 0;j < m;j++)
//			mas[i+1][j] = b[i][j];
//
//
//	//очищаем пам€ть из под массива b
//	for (int i = 0;i < n;i++)
//		delete[]b[i];
//	delete[]b;
//}
//
//int main()
//{
//	using namespace std;
//	setlocale(LC_ALL, "rus");
//		srand(time(NULL));
//		int N,M,K;
//		cout << "¬ведите кол-во строк ";
//		cin >> N;
//		cout << "¬ведите кол-во cтобцов ";
//		cin >> M;
//		cout << "¬ведите позицию дл€ вставки ";
//		cin >> K;
//
//		int** mas = new int* [N];
//		for (int i = 0;i < N;i++)
//		{
//			mas[i] = new int[M];
//		}
//
//		for (int i = 0;i < N;i++)
//		{
//			for (int j = 0;j < M;j++)
//			{
//				mas[i][j] = rand() % 22;
//				cout << mas[i][j] << "\t";
//			}
//			cout << endl;
//		}
//
//		Add(mas, N, M, K);
//
//		cout << endl;
//		cout << endl;
//
//		for (int i = 0;i < (N+1);i++)
//		{
//			for (int j = 0;j < M;j++)
//			{
//				cout << mas[i][j] << "\t";
//			}
//			cout << endl;
//		}
//
//		for (int i = 0;i < (N+1);i++)
//			delete[]mas[i];
//		delete[]mas;
//
//}