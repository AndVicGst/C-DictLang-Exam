# Экзамен - приложение "Словари"

## class DictionaryFileHandler - Класс – обработчик чтения и сохранения словаря в файл

>Методы:

>Запись в файл:  
**public static void Write(string path, KeyValuePair<string, List<string>> pair)** 

>Чтение из файла:  
**public static SortedDictionary<string, List<string>> Read (string path)**


## public class DictionaryHandler - Класс – обработчик действий со словарем

>переменная хранит путь к словарю:  
**private string _path;**

>Методы:

>конструктор:  
**public DictionaryHandler(string path)**

>Добавление слова и перевода в файл:  
**public void SetNewValues (KeyValuePair<string, List<string>> pair)**

>Ввод слова и перевода для добавления в файл словаря:  
**public void SetValues()**

>Поиск слова в словаре:  
**public KeyValuePair<string, List<string>> SearchWord(string word)** 

>Ввод слова для поиска:  
**public string GetWord()**

>Вывод на экран найденного слова и перевода, сохранение их в файл findWord.txt:  
**public void PrintPair(KeyValuePair<string, List<string>> pair)**

>Поиск и замена слова - по ключу:  
**public void ChangeKey(string path, KeyValuePair<string, List<string>> pair)**

>Поиск и замена перевода - по значению:  
**public void ChangeValue(string path, KeyValuePair<string, List<string>> pair)**

>Удаление слова и его перевода из словаря:  
**public void DeleteKey(string path, KeyValuePair<string, List<string>> pair)**

>Удаление перевода слова из словаря (кроме последнего):  
**public void DeleteValues(string path, KeyValuePair<string, List<string>> pair)**
      
## class Menu - Класс – обработчик создания меню 
  
>переменная хранит путь к файлу с названиями словарей:  
**private const string PATH = "dicts.txt";**

>Список хранит названия словарей:  
**private List<string> _dicts = new();**

>Сохраняет название словаря в файл dicts.txt:  
**private void _saveDictsNames()**

>Читает названия словарей из файла dicts.txt:  
**private void _readDictsNames()**

>Выводит меню из названий словарей на экран:  
**private void GetMenu()**

>Выводит основное меню на экран:  
**public void mainMenu()**

>Выводит на экран меню создания словаря:  
**private void CreateMenu()**

>Выводит на экран меню выбора словарей:  
**private void DictsMenu()**

>Выводит на экран меню действий со словарем:  
**private void DictMenu(string path, string nameDict)**

### static void Main(string[] args) - Запускаем приложение

>Выводит на экран строку определённого вида:  
**static void repeatEnter(string str)**



















