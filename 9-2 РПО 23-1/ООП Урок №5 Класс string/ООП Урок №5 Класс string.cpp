#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

struct Entry
{
    string word;
    int count;
};

vector<Entry> getDictionary(string text);
vector<string> getWords(string text);
bool sortByWords(const string& word1, const string& word2);

int main()
{
    setlocale(LC_ALL, "");

    char ch{ 'a' };
    //char8_t ch8{ u8'a' };
    char16_t ch16{ u'a' };
    char32_t ch32{ U'a' };
    wchar_t chw{ L'a' };
    cout << ch << " " << (char)ch16 << " " << (char)ch32 << " " << (char)chw << endl;

    string str1{ "Hello, World!" };
    string str2(15, 'a');
    string str3 = "Привет, Мир!";
    cout << str1 << " | " << str2 << " | " << str3 << endl;    

    // Александрова -   латинские строковые символы, спецсимволы (~!@#$%^&*()_+)
    // Антонов      -   кириллические заглавные символы, цифры (0-9)
    // Духина       -   латиские строковые и кириллические строковые символы
    // Землянский   -   спецсиволы и цифры
    // Золин        -   кириллица заглавные и спецсимволы
    // Красицкий    -   латинница строковые и цифры
    // Кубанов      -   спецсимволы и цифры
    // Лушников     -   цифры и кириллица строковые
    // Мамонтова    -   цифры и спецсимволы
    // Метелицин    -   спецсимволы и латинница заглавные
    // Назарян      -   спецсимволы и кириллица строковые
    // Чавычалов    -   латинница заглавные и цифры
    // Юнусов       -   кириллица строковые и спецсимволы

    // Александрова -   демонстрация работы методов доступа к символам строки
    // Антонов      -   демонстрация работы методов-итераторов
    // Духина       -   демонстрация работы методов поиска в строке
    // Землянский   -   демонстрация работы методов редактирования строки
    // Золин        -   демонстрация работы методов работы со вместимостью строки
    // Красицкий    -   демонстрация работы методов сервисных функций
    // Кубанов      -   демонстрация работы методов доступа к символам строки
    // Лушников     -   демонстрация работы методов-итераторов
    // Мамонтова    -   демонстрация работы методов поиска в строке
    // Метелицин    -   демонстрация работы методов редактирования строки
    // Назарян      -   демонстрация работы методов работы со вместимостью строки
    // Чавычалов    -   демонстрация работы методов доступа к символам строки
    // Юнусов       -   демонстрация работы методов сервисных функций

    char cText[] { 'a','b','c','\0','d','e','f' };
    string sText{'a','b','c','\0','d','e','f'};
    cout << "char array string |" << cText << "| end array" << endl;
    cout << "text string |" << sText << "| end text" << endl;
    cout << "text size: " << sText.size() << endl;
    int length = sText.length();
    cout << "text length: " << length << endl;

    cout << "Пробельные символы: \' \', \'\\t\', \'\\n\', \'\\v\', \'\\r\', \'\\f\'" << endl << endl;

    string s1;
    string s2;
    string s3;

    s1 = "abracadabra";
    s2 = { 'x','y','z' };
    s3 = s1;

    cout << " " << s1 << " " << s2 << " " << s3 << endl;
    cout << " " << (s1 == s2) << " " << (s1 != s2) << " " << (s1 < s2) << " " << (s1 >= s2) << endl;
    cout << " " << s1 + s2 << endl;
    s2 += s1;
    cout << " " << s2 << endl;

    cout << "________________________________________________________________________________________" << endl << endl;

    string text{ R"(
    Криптография - наука о защите информации (невозможности прочтения информации третьим лицам) с использованием математических методов.
История этой науки насчитывает примерно 4 тысяч лет.
И с каждым поколением методы шифрования только совершенствовались, точно так же как и методы взлома информации.
Шифрование данных на данный момент представляет собой одно из тех направлений, в котором проходит бурное развитие, так многие называют современную эру информационной, где ценность информации как никогда высока.
Проблема безопасности передачи личных и секретных данных очень актуальна.
    С криптографией всегда связаны такие понятия как:
    *   открытый текст - исходные данные, передаваемые без использования криптографии;
    *   закрытый текст - зашифрованные данные, полученные после применения криптосистемы;
    *   ключ - параметр шифра, определяющий выбор конкретного преобразования данного текста.
То есть открытый (исходный) текст закрывается (зашифровывается) с помощью ключа. 
        )"
    };

    cout << text << endl;
    cout << "Создание словаря:" << endl << endl;
    vector<string> words = getWords(text);

    for (size_t i = 0; i < words.size(); i++)
    {
        cout << words[i] << endl;
    }

    cout << endl;

    vector<Entry> dictionary = getDictionary(text);

    for (size_t i = 0; i < dictionary.size(); i++)
    {
        cout << dictionary[i].word << " : " << dictionary[i].count << endl;
    }

    cout << endl;

    return 0;
}

bool sortByWords(const string& word1, const string& word2)
{
    return word1 < word2;
}

vector<string> getWords(string text)
{
    vector<string> words;
    int pos = 0;

    while (pos < text.size())
    {
        while (pos < text.size() && !isalpha(text[pos]))
        {
            pos++;
        }
        if (pos == text.size())
        {
            break;
        }

        int wordStart = pos;

        while (pos < text.size() && isalpha(text[pos]))
        {
            pos++;
        }
        int wordLength = pos - wordStart;
        words.push_back(text.substr(wordStart, wordLength));
    }
    return words;
}

vector<Entry> getDictionary(string text)
{
    vector<Entry> entries;

    vector<string> words = getWords(text);
    sort(words.begin(), words.begin() + words.size(), sortByWords);
    int i = 0;

    while (i < words.size())
    {
        Entry entry{ words[i], 0 };
        while (i < words.size() && words[i] == entry.word)
        {
            entry.count++;
            i++;
        }
        entries.push_back(entry);
    }

    return entries;
}