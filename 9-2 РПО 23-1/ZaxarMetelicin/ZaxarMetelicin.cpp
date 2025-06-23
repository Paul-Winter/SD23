// C++ Код из github.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

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

        while (pos < text.size() && !isalpha(text[pos]) && !(text[pos] & 0x80))
        {
            pos++;
        }
        if (pos == text.size())
        {
            break;
        }

        int wordStart = pos;


        while (pos < text.size() && (isalpha(text[pos]) || (text[pos] & 0x80)))
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
    sort(words.begin(), words.end(), sortByWords);
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
