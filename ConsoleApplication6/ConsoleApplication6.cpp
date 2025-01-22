using namespace std;
char b[100] = {};
int Count, a;
char File[] = "F:/докум.txt";
FILE* F_tel;
void main()
{
	sl;
	srand(time(0));
	char b[100] = {}, * m = 0, * j;
	int l = 0, a = 0, i = 0;
	j = b;
	if ((F_tel = fopen("F:/докум.txt", "rt")) == NULL)
	{
		puts("Открыть файл не удалось\n");
		exit(1);
	}
	getc(F_tel);//цикл для чтения значений из файла; выполнение цикла
	прервется,
		//когда достигнем конца файла, в этом случае F.eof() вернет истину.
		while (!feof(F_tel))
		{
			m = fgets(b, 100, F_tel);
			a++;
		}
	cout << а;\\это номер последней строки как её удалить ? и рез записать в
		другой файл
		sp;
}