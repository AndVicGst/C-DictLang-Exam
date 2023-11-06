# C-DictLang-Exam
>Класс – обработчик чтения и сохранения словаря в файл

**class DictionaryFileHandler**

>Методы:

>Запись в файл

**public static void Write(string path, KeyValuePair<string, List<string>> pair)** 

>Чтение из файла

**public static SortedDictionary<string, List<string>> Read (string path)**

>Класс – обработчик действий со словарем

**public class DictionaryHandler**

>переменная хранит путь к словарю

**private string _path;**

>Методы:

>конструктор

**public DictionaryHandler(string path)**

>Добавление слова и перевода в файл

**public void SetNewValues (KeyValuePair<string, List<string>> pair)**

>Ввод слова и перевода для добавления в файл словаря

**public void SetValues()**

>Поиск слова в словаре

**public KeyValuePair<string, List<string>> SearchWord(string word)** 

>Ввод слова для поиска

**public string GetWord()**

>Вывод на экран слова и перевода

**public void PrintPair(KeyValuePair<string, List<string>> pair)**

>Поиск и замена слова - по ключу

**public void ChangeKey(string path, KeyValuePair<string, List<string>> pair)**

>Поиск и замена перевода - по значению

**public void ChangeValue(string path, KeyValuePair<string, List<string>> pair)**

>Удаление слова и его перевода из словаря

**public void DeleteKey(string path, KeyValuePair<string, List<string>> pair)**

>Удаление перевода слова из словаря (кроме последнего)

**public void DeleteValues(string path, KeyValuePair<string, List<string>> pair)**
