// Программа, иллюстрирущая применение событий, на примере чтения нажатой клавиши
using System;
using System.IO;

namespace EventKeyDown
{
    // Производный класс от EventArgs, который не содержит полей для передачи информации обработчику
    // Используем наследование и добавляем поле для хранения нажатой клавиши.
    class MyEventArgs : EventArgs
    {
        public char ch;
    }

    class KeyEvent
    {
        // Создадим событие KeyDown, используя встроенный обобщенный делегат EventHandler
        public event EventHandler<MyEventArgs> KeyDown;

        public void OnKeyDown(char ch)
        {
            MyEventArgs c_event = new MyEventArgs();

            if (KeyDown != null)
            {
                c_event.ch = ch;
                KeyDown(this, c_event);
            }
        }
    }
    class myFile 
    {
        private string nameOfFile;

        public string name
        {
            get { return nameOfFile; }
           //set { nameOfFile = value; }
        }
        public myFile(string _name)
        {
            DateTime _currentDT = DateTime.Now;
            this.nameOfFile = _name;
            var sw = new StreamWriter(_name, true);
            sw.WriteLine(_currentDT.ToString());
            sw.Close();
        }
        public void WriteToFile(string _text)
        {
            var sw = new StreamWriter(this.nameOfFile, true);
            sw.WriteLine(_text);
            sw.Close();
        }
    }
    class Program
    {
        static void SaveLoge(object sender, MyEventArgs e)
        {
            string _name = "Log.txt";
            var logFiloe = new myFile(_name);
            logFiloe.WriteToFile(e.ch.ToString());
        }
        static void Main()
        {
            KeyEvent evnt = new KeyEvent();
            evnt.KeyDown += (sender, e) => // Формирует событие обрабатывающее нажатую клавишу
            {
                if ((e.ch == 'C') | (e.ch == 'c'))
                {
                    MyColor(true);
                }
                else
                {
                    if ((e.ch == 'E') | (e.ch == 'e'))
                    {
                        Console.WriteLine("\nРабота программы завершена...");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("\nТакая команда не найдена!");
                    }
                }
            };
            evnt.KeyDown += SaveLoge;
            ConsoleMenu();
            char ch;
            do
            {
                Console.Write("Введите комманду: ");
                ConsoleKeyInfo key; //System.ConsoleKeyInfo описывает нажатую клавишу консоли. Создаём объект key.
                key = Console.ReadKey();
                ch = key.KeyChar; // Свойство KeyChar возвращает символ Юникода, представленный ConsoleKeyInfo
                evnt.OnKeyDown(key.KeyChar);
            }
            while (ch != 'E');
        }

        // Вспомогательные методы

        static void ConsoleMenu() // Меню для управления
        {
            CC(ConsoleColor.Green);
            Console.WriteLine("**********************************\n\n Выбираем цвет текста и цвет фона"
                + "\n**********************************\n");
            Command("C", "Поменять цвет текста");
            Command("E", "Выход");
            Console.WriteLine();
        }
        static void CC(ConsoleColor color) // Назначить цвет текста
        {
            Console.ForegroundColor = color;
        }

        static void Command(string s1, string s2) // Формирование строк меню
        {
            CC(ConsoleColor.White);
            Console.Write(s1);
            CC(ConsoleColor.Green);
            Console.Write(" -> " + s2 + "\n");
        }

        static void MyColor(bool IsChanged) // Задаём цвет текста
        {
            Console.Write("\nВведите цвет: (1 - красный, 2 - желтый, 3 - синий, 4 - пурпурный, 5 - голубой, 6 - тёмнокрасный) ");
            string _color = Console.ReadLine();
            if (_color == "1")
            {
                if (IsChanged) Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                if (_color == "2")
                {
                    if (IsChanged) Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    if (_color == "3")
                    {
                        if (IsChanged) Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        if (_color == "4")
                        {
                            if (IsChanged) Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                        else
                        {
                            if (_color == "5")
                            {
                                if (IsChanged) Console.ForegroundColor = ConsoleColor.Cyan;
                            }
                            else
                            {
                                if (_color == "6")
                                {
                                    if (IsChanged) Console.ForegroundColor = ConsoleColor.DarkRed;
                                }
                                else
                                {
                                    Console.WriteLine("Не удалось определить цвет");
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Цвет изменён.");
        }
    }
}