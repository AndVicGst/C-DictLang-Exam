using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        class DictionaryFileHandler
        {
            //записываем слово и перевод в файл
            public static void Write(string path, KeyValuePair<string, List<string>> pair) 
            {
                //конструктор Streamwriter передает путь и добавляет запись в конец файла (true)
                using (StreamWriter sw = new(path, true))
                {
                        sw.WriteLine($"{pair.Key}:{String.Join(',',pair.Value)}");
                }          
            }
            //загружаем из файла в словарь
            public static SortedDictionary<string, List<string>> Read (string path)
            {
                if (File.Exists(path))
                {
                    string[] text;
                    //если в словаре пустая строка игнорируем ее
                    text = File.ReadAllLines(path).Where(str => !string.IsNullOrWhiteSpace(str)).ToArray();                
                    SortedDictionary<string, List<string>> dict = new();
                    foreach (string item in text)
                        {
                            List<string> list = new List<string>(item.Split(':', StringSplitOptions.RemoveEmptyEntries));
                            dict.Add(list[0], list[1].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
                        }
                     return dict;
                }
                return new SortedDictionary<string, List<string>>();
            }
        }

        public class DictionaryHandler
        {
            private string _path;
            public DictionaryHandler(string path)
            {
                _path = path;
            }
            //Добавили слово и перевод в файл 
            public void SetNewValues (KeyValuePair<string, List<string>> pair)
            {
                DictionaryFileHandler.Write(_path, pair);
            }
            //вводим слово и перевод для добавления в файл словаря
            public void SetValues()
            {
                Console.Write("Введите новое слово: ");
                string key = Console.ReadLine() ?? "";
                Console.Write("Введите его перевод через пробел: ");
                List<string> values = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                SetNewValues(new KeyValuePair<string, List<string>>(key, values));
                repeatEnter($"Слово: {key} и его перевод успешно добавлено в словарь!");
            }

            //поиск слова в словаре по ключу
            public KeyValuePair<string, List<string>> SearchWord(string word) 
            {
                SortedDictionary<string, List<string>> dict = DictionaryFileHandler.Read(_path);
                if (dict.Count != 0)
                {
                    foreach (string dictWord in dict.Keys)
                    {
                        if (dictWord.ToLower().Contains(word.ToLower()))
                            return new KeyValuePair<string, List<string>>(dictWord, dict[dictWord]);
                    }
                }
                return new KeyValuePair<string, List<string>>();
            }

            //ввод слова для поиска
            public string GetWord()
            {                              
                string word;
                while (true)
                {
                    Console.Write("Введите слово: ");
                    word = Console.ReadLine() ?? "";
                    if (word.Length < 3)
                    {
                        Console.WriteLine("Слово должно содержать минимум 3 буквы.");
                        continue;
                    }
                    else break;
                }
                return word;
            }
            public void PrintPair(KeyValuePair<string, List<string>> pair)
            {
                Console.WriteLine($"Найдено совпадение: {pair.Key}: перевод - {String.Join(", ", pair.Value)}");
                using StreamWriter sw = new StreamWriter("findWord.txt", true);
                {
                        sw.WriteLine($"Слово - {pair.Key} : перевод - {String.Join(',', pair.Value)}");
                }
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
            }

            //поиск и замена слова - по ключу
            public void ChangeKey(string path, KeyValuePair<string, List<string>> pair)
            {
                string word;
                Console.WriteLine($"Найдено совпадение: {pair.Key}: перевод -{String.Join(", ", pair.Value)}");
                while (true)
                {
                    Console.Write("Введите новое слово: ");
                    word = Console.ReadLine() ?? "";
                    if (word.Length < 3)
                    {
                        Console.WriteLine("Слово должно содержать минимум 3 буквы.");
                        continue;
                    }
                    else break;
                }
                SortedDictionary<string, List<string>> dict = DictionaryFileHandler.Read(_path);
                dict.Remove(pair.Key);
                dict.Add(word, pair.Value);
                using StreamWriter sw = new StreamWriter(path, false);
                {
                    foreach(var item in dict)
                    {
                        sw.WriteLine($"{item.Key}:{String.Join(',',item.Value)}");
                    }
                }
                repeatEnter($"Слово: {pair.Key} успешно заменено на слово: {word}!");

            }

            //поиск и замена перевода - по значению
            public void ChangeValue(string path, KeyValuePair<string, List<string>> pair)
            {
                string word;
                List<string> valuesList = new();
                Console.WriteLine($"Найдено совпадение: {pair.Key}: перевод - {String.Join(", ",pair.Value)}");
                while (true)
                {
                    Console.Write("Введите новый перевод через пробел: ");
                    valuesList = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (valuesList.Count == 0)
                    {
                        Console.WriteLine("Некорректный ввод.");
                        continue;
                    }
                    else break;
                }
                SortedDictionary<string, List<string>> dict = DictionaryFileHandler.Read(_path);
                dict.Remove(pair.Key);
                dict.Add(pair.Key, valuesList);
                using StreamWriter sw = new StreamWriter(path, false);
                {
                    foreach (var item in dict)
                    {
                        sw.WriteLine($"{item.Key}:{String.Join(',',item.Value)}");
                    }
                }
                repeatEnter($"Перевод: {pair.Key} успешно заменен на: {String.Join(", ",valuesList)}!");

            }

            //удаляем слово и его перевод из словаря
            public void DeleteKey(string path, KeyValuePair<string, List<string>> pair)
            {
                Console.WriteLine($"Найдено совпадение: {pair.Key}: перевод - {String.Join(", ",pair.Value)}");
                while (true)
                {
                    Console.Write("Подтвердите действие:\n[1] - удалить слово\n" + "[0] - отмена\n: ");
                    int i;
                    if (!int.TryParse(Console.ReadLine(), out i)) continue;
                    if (i == 1) break;
                    if (i == 0) return;
                }
                SortedDictionary<string, List<string>> dict = DictionaryFileHandler.Read(_path);
                dict.Remove(pair.Key);
                using StreamWriter sw = new StreamWriter(path, false);
                {
                    foreach (var item in dict)
                    {
                        sw.WriteLine($"{item.Key}:{String.Join(',',item.Value)}");
                    }
                }
                repeatEnter($"Слово: {pair.Key} и его перевод успешно удалено!");
            }

            //удаляем перевод слова из словаря
            public void DeleteValues(string path, KeyValuePair<string, List<string>> pair)
            {
                Console.WriteLine($"Найдено совпадение: {pair.Key}: перевод - {String.Join(", ",pair.Value)}");
                while (true)
                {
                    Console.Write("Подтвердите действие:\n[1] - удалить перевод\n" + "[0] - отмена\n: ");
                    int i;
                    if (!int.TryParse(Console.ReadLine(), out i)) continue;
                    if (i == 1) break;
                    if (i == 0) return;
                }
                SortedDictionary<string, List<string>> dict = DictionaryFileHandler.Read(_path);
                if (pair.Value.Count == 1)
                {
                    repeatEnter($"Перевод {pair.Key} состоит из одного слова и не может быть удален!");
                    return;
                }
                while (pair.Value.Count > 1)
                {
                    pair.Value.RemoveAt(0);
                }
                dict.Remove(pair.Key);
                dict.Add(pair.Key, pair.Value); 
                using StreamWriter sw = new StreamWriter(path, false);
                {
                    foreach (var item in dict)
                    {
                        sw.WriteLine($"{item.Key}:{String.Join(',',item.Value)}");
                    }
                }
                repeatEnter($"Перевод слова: {pair.Key} успешно удален!");
            }
        }

        class Menu
        {
            private const string PATH = "dicts.txt";
            private List<string> _dicts = new();
            private void _saveDictsNames()
            {
                using (StreamWriter sw = new StreamWriter(PATH))
                    foreach(string dict in _dicts)
                        sw.WriteLine(dict);
            }

            private void _readDictsNames()
            {
                if (File.Exists(PATH))
                    _dicts = File.ReadAllLines(PATH).ToList();  
            }

            private void GetMenu()
            {
                int i = 0;
                foreach (string dict in _dicts) 
                    Console.WriteLine($"[{++i}] - {dict}");
            }
            public void mainMenu()
            {
               _readDictsNames();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Привет, пользовтаель!\n");
                    Console.Write("Выбери действие:\n" + "[1] - создать словарь\n" + "[2] - открыть словарь\n" + "[0] - выход\n" + ": ");
                    int menu;
                    if (!int.TryParse(Console.ReadLine(), out menu)) continue;
                    Console.Clear();
                    if (menu == 0) break;
                    if (menu == 1) 
                        CreateMenu();
                    if (menu == 2)
                        DictsMenu();
                }
                Console.WriteLine("До новых встреч!");
            }
            private void CreateMenu()
            {
                Console.Write("Введите название словаря: ");
                string name = Console.ReadLine() ?? "";
                if (name == "")
                {
                    repeatEnter("Ошибка ввода названия словаря.");
                    return;
                }
                else
                {
                    foreach(string item in _dicts)
                    {
                        if (item == name)
                        {
                            repeatEnter("Такой словарь уже существует!");
                            return;
                        }
                    }
                    //добавили название словаря в список и потом в файл
                    _dicts.Add(name);
                    _dicts.Sort();
                    _saveDictsNames();
                    repeatEnter($"Словарь {name} создан!");
                    //создали пустой файл словаря
                    using (StreamWriter sw = new StreamWriter(name + ".txt"));
                }
            }
            private void DictsMenu()
            {
                Console.Clear();
                if (_dicts.Count == 0) 
                {
                    repeatEnter("Словарей нет. Создайте словарь.");
                    return; 
                }
                Console.WriteLine("Выберите словарь: ");
                GetMenu();
                int i = 0;
                bool check = false;
                while (!check)
                {
                    if (!int.TryParse(Console.ReadLine(), out i) || i < 1 || i > _dicts.Count) 
                    {  
                        Console.Write("Неверный индекс. Повторите ввод: ");
                        continue;
                    }
                    check = true;
                }
                DictMenu($"{ _dicts[i-1]}.txt", _dicts[i-1]);
            }
            private void DictMenu(string path, string nameDict)
            {
                DictionaryHandler dict = new(path);
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Выбран словарь {nameDict}.");
                    Console.Write("[1] - поиск перевода\n" + "[2] - добавление перевода\n" + "[3] - заменить слово\n"+
                                  "[4] - заменить перевод слова\n" + "[5] - удалить слово\n" + "[6] - удалить перевод слова\n" + "[0] - выход\n: ");
                    int i;
                    if (!int.TryParse(Console.ReadLine(), out i)) continue;
                    if (i == 0) break;
                    if (i == 1) //ищем слово
                    {
                        string word = dict.GetWord();
                        var pair = dict.SearchWord(word);
                        if (pair.Key == null)
                        {
                            repeatEnter($"Слово {word.ToUpper()} в словаре {nameDict.ToUpper()} не найдено.");
                            continue;
                        }
                        dict.PrintPair(pair);
                    }
                    if (i == 2)  //добавляем слово и перевод в словарь
                    {
                        dict.SetValues(); 
                    }
                    if (i == 3) //заменить слово, перевод старый остается
                    {
                        string word = dict.GetWord(); //ищем слово
                        var pair = dict.SearchWord(word);
                        if (pair.Key == null)
                        {
                            repeatEnter($"Слово {word.ToUpper()} в словаре {nameDict.ToUpper()} не найдено.");
                            continue;
                        }
                        dict.ChangeKey(path, pair);
                    }
                    if (i == 4) //поиск и замена перевода - вводим перевод, находим слово и его перевод
                    {
                        string word = dict.GetWord(); //ищем слово 
                        var pair = dict.SearchWord(word);
                        if (pair.Key == null)
                        {
                            repeatEnter($"Слово {word.ToUpper()} в словаре {nameDict.ToUpper()} не найдено.");
                            continue;
                        }
                        dict.ChangeValue(path, pair);
                    }
                    if (i == 5) //удаляем слово и его перевод
                    {
                        string word = dict.GetWord(); //ищем слово 
                        var pair = dict.SearchWord(word);
                        if (pair.Key == null)
                        {
                            repeatEnter($"Слово {word.ToUpper()} в словаре {nameDict.ToUpper()} не найдено.");
                            continue;
                        }
                        dict.DeleteKey(path, pair);
                    }
                    if (i == 6) //удаляем перевод слова
                    {
                        string word = dict.GetWord(); //ищем слово 
                        var pair = dict.SearchWord(word);
                        if (pair.Key == null)
                        {
                            repeatEnter($"Слово {word.ToUpper()} в словаре {nameDict.ToUpper()} не найдено.");
                            continue;
                        }
                        dict.DeleteValues(path, pair);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Menu menu = new();
            menu.mainMenu();

        }

        static void repeatEnter(string str)
        {
            Console.WriteLine(str);
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
} 