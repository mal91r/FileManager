using System;
using System.IO;
using System.Text;

namespace PeerGrade3
{
    class Program
    {
        /// <summary>
        /// Точка запуска программы.
        /// Вызывается метод Start.
        /// </summary>
        static void Main()
        {
            if (button.Key != ConsoleKey.Escape)
            {
                Start();
            }
            Console.WriteLine("Спасибо за пользование файловым менеджером! Надеюсь, всё прошло гладко!");
        }

        /// <summary>
        /// Метод-инструкция приветствует пользователя и помогает грамотно начать работу с приложением.
        /// </summary>
        static void Start()
        {
            Console.WriteLine("Добро пожаловать в файловый менеджер! Перед началом использования рекомендую ознакомиться с файлом README.txt,\n" +
                "хотя и без этого грамотное использование приложения должно быть интуитивно понятно.\n" +
                "Горячие клавиши:\n" +
                "1. стрелки вверх/вниз - управление выбором нужной дериктории/нужного файла.\n" +
                "2. стрелка вправо - переход из текущей директории в ее поддерикторию или переход к операциям, \nприменимым к выбранному файлу.\n" +
                "3. стрелка влево - переход к предыдущей(родительской) дериктории или к списку дисков\n(в зависимость от уровня углубленности).\n" +
                "4. С - просмотр полного пути к текущей директории.\n" +
                "5. F - создание и запись файла.\n" +
                "6. Escape - завершение работы приложения(если запущена одна из операций\n(например, ожидается ввод чего-либо из консоли), \nто может потребоваться несколько нажатий для полного завершения).\n" +
                "7. H - помощь, ознакомление с горячими клавишами.\n");
            Console.WriteLine("Для запуска нажмите любой символ!");
            Console.ReadKey();
            Menu();
        }

        /// <summary>
        /// Помощь, иструкции по работе с приложением.
        /// </summary>
        static void Helper()
        {
            Console.Clear();
            Console.WriteLine("Горячие клавиши:\n" +
                "1. стрелки вверх/вниз - управление выбором нужной дериктории/нужного файла.\n" +
                "2. стрелка вправо - переход из текущей директории в ее поддерикторию или переход к операциям,\n применимым к выбранному файлу.\n" +
                "3. стрелка влево - переход к предыдущей(родительской) дериктории или к списку дисков\n(в зависимость от уровня углубленности).\n" +
                "4. С - просмотр полного пути к текущей директории.\n" +
                "5. F - создание и запись файла.\n" +
                "6. Escape - завершение работы приложения(если запущена одна из операций\n(например, ожидается ввод чего-либо из консоли), \nто может потребоваться несколько нажатий для полного завершения.\n" +
                "7. H - помощь, ознакомление с горячими клавишами.\n");
            Console.WriteLine("Для запуска нажмите любой символ!\n");
        }

        /// <summary>
        /// Основное меню.
        /// Выбор способа работы с приложением.
        /// </summary>
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine(" 1. Ввод пути\n" +
                " 2. Проводник");
            int number = Move(1);
            switch (number)
            {
                case 0:
                    InputPath();
                    break;
                case 1:
                    Drives();
                    break;
            }
            Console.Clear();
        }


        /// <summary>
        /// Ввод пути вручную.
        /// </summary>
        static void InputPath()
        {
            Console.Clear();
            Console.Write("Введите путь: ");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                Conductor(path);
            }
            else
            {
                Console.WriteLine("Директория не существует. Для продолжения нажмите любой символ.");
                Console.ReadKey();
                Menu();
            }


        }

        /// <summary>
        /// Просмотр списка дисков компьютера.
        /// </summary>
        static void Drives()
        {
            Console.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine(" " + drives[i].Name);
            }
            Conductor(drives[Move(drives.Length - 1)].Name);
        }

        /// <summary>
        /// "Проводник" для работы с приложением.
        /// Он вызывает все основные методы, проверяет файл на существование и обрабатывает исключения.
        /// </summary>
        /// <param name="path"></param>
        static void Conductor(string path)
        {
            Console.Clear();

            if (Directory.Exists(path))
            {
                try
                {
                    Commandor(path);
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Попытка обратиться к закрытой или несуществующей директории. Для продолжения нажмите любой символ.");
                    Console.ReadKey();
                    Left(path);
                }
            }
            else
            {
                Console.WriteLine("Директория не существует. Для продолжения нажмите любой символ.");
                Catcher(path);
            }
        }

        /// <summary>
        /// Создание кнопки.
        /// </summary>
        static ConsoleKeyInfo button;

        /// <summary>
        /// Обработка ввода с клавиатуры и вызов соответствующих операций.
        /// </summary>
        /// <param name="path"></param>
        static void Commandor(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            if(files.Length + dirs.Length == 0)
            {
                Console.WriteLine("Директория пуста!");
                Console.ReadKey();
                Left(path);
            }
            int line = Move(Out(path) - 1);
            if (button.Key == ConsoleKey.RightArrow)
            {
                Right(line, dirs, files);
            }
            if (button.Key == ConsoleKey.LeftArrow)
            {
                Left(path);
            }
            if (button.Key == ConsoleKey.C)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                Console.Clear();
                Console.WriteLine(dirInfo.FullName);
                Catcher(path);
            }
            if (button.Key == ConsoleKey.F)
            {
                FileCreate(path);
                Catcher(path);
            }
            if (button.Key == ConsoleKey.H)
            {
                Helper();
                Catcher(path);
            }
        }

        /// <summary>
        /// Метод, отвечающий за создание файла.
        /// </summary>
        /// <param name="path"></param>
        static void FileCreate(string path)
        {
            Console.Clear();
            string text = "";
            Console.WriteLine(" 1. Записать текст в создаваемый файл.\n" +
                " 2. Оставить файл пустым");
            path += @"\text.txt";
            switch (Move(1))
            {
                case 0:
                    Console.Clear();
                    Console.Write("Введите текст: ");
                    text = Console.ReadLine();
                    break;
                case 1:
                    break;
            }
            Console.Clear();
            Console.WriteLine("Для выбора желаемой кодировки нажмите любой символ.");
            Console.ReadKey();
            Console.Clear();
            //Ниже представлены варианты кодировки, которые я предлагаю пользователю.
            Console.WriteLine(" 1. UTF8\n" +
                " 2. ASCII\n" +
                " 3. Unicode\n" +
                " 4. UTF32\n" +
                " 5. Default");
            int number = Move(4);
            FileSwitcher(number, path, text);
            Console.Clear();
            Console.WriteLine("Создание файла прошло успешно!");
        }

        /// <summary>
        /// Декомпозиция операции switch метода FileCreate.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="path"></param>
        /// <param name="text"></param>
        static void FileSwitcher(int number, string path, string text)
        {
            switch (number)
            {
                case 0:
                    File.WriteAllText(path, text, Encoding.UTF8);
                    break;
                case 1:
                    File.WriteAllText(path, text, Encoding.ASCII);
                    break;
                case 2:
                    File.WriteAllText(path, text, Encoding.Unicode);
                    break;
                case 3:
                    File.WriteAllText(path, text, Encoding.UTF32);
                    break;
                default:
                    File.WriteAllText(path, text, Encoding.Default);
                    break;
            }
        }

        /// <summary>
        /// Метод отвечает за движение курсора по консоли и обработку действий пользователя.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        static int Move(int l)
        {
            //Ставим курсор на начальную позицию.
            int x = 0, y = 0;
            Console.SetCursorPosition(x, y);
            button = Console.ReadKey();

            Console.Write("►");
            while (button.Key != ConsoleKey.Escape & button.Key != ConsoleKey.RightArrow & button.Key != ConsoleKey.LeftArrow & button.Key != ConsoleKey.C & button.Key != ConsoleKey.F & button.Key != ConsoleKey.H)
            {
                Console.SetCursorPosition(x, y);
                if (button.Key == ConsoleKey.UpArrow & y > 0)
                {
                    Console.Write(" ");
                    Console.SetCursorPosition(0, --y);
                    Console.Write("►");
                    Console.SetCursorPosition(x, y);
                }
                if (button.Key == ConsoleKey.DownArrow & y < l)
                {
                    Console.Write(" ");
                    Console.SetCursorPosition(0, ++y);
                    Console.Write("►");
                    Console.SetCursorPosition(x, y);
                }
                button = Console.ReadKey();
            }
            if (button.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Environment.Exit(0);
            }
            return y;
        }

        /// <summary>
        /// Вывод всех файлов и поддиректорий, входящих в данную директорию.
        /// Метод возвращает суммарное количество папок и файлов.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static int Out(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);
                Console.WriteLine($" [{dirInfo.Name}]");
            }
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);

                Console.WriteLine($" {fileInfo.Name}");
            }
            return dirs.Length + files.Length;
        }

        /// <summary>
        /// Метод, вызываемый в случае ошибки, возвращает программу "проводнику".
        /// </summary>
        /// <param name="path"></param>
        static void Catcher(string path)
        {
            Console.ReadKey();
            Conductor(path);
        }

        /// <summary>
        /// Метод обрабатывает нажатие стрелки влево.
        /// </summary>
        /// <param name="path"></param>
        static void Left(string path)
        {
            if (Directory.GetParent(path) == null)
            {
                Drives();
            }
            else
            {
                Conductor(Directory.GetParent(path).FullName);
            }
        }

        /// <summary>
        /// Метод обрабатывает нажатие стрелки вправо.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="dirs"></param>
        /// <param name="files"></param>
        static void Right(int line, string[] dirs, string[] files)
        {
            if (line < dirs.Length)
            {
                Conductor(dirs[line]);
            }
            else
            {
                Console.Clear();
                FileOperations(files[line - dirs.Length]);
            }
        }

        /// <summary>
        /// Метод отвечает за применение к файлам операций.
        /// </summary>
        /// <param name="path"></param>
        static void FileOperations(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            //Для нетекстовых файлов.
            if (fileInfo.Extension != ".txt")
            {
                Console.Clear();
                Console.WriteLine($" 1. Copy\n" +
                    $" 2. Delete\n" +
                    $" 3. Replace\n");
                int number = Move(2);
                OperationSwitcher1(path, number);
                Left(path);
            }
            //Для текстовых файлов.
            else
            {
                Console.WriteLine($" 1. Copy\n" +
                    $" 2. Delete\n" +
                    $" 3. Replace\n" +
                    $" 4. Read\n" +
                    $" 5. Сoncatenation");
                int number = Move(4);
                OperationSwitcher2(number, path);
                Left(path);
            }

        }

        /// <summary>
        /// Декомпозиция операции switch метода FileOperations.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="number"></param>
        static void OperationSwitcher1(string path, int number)
        {
            switch (number)
            {
                case 0:
                    Copy(path);
                    break;
                case 1:
                    Delete(path);
                    break;
                case 2:
                    Replace(path);
                    break;
            }
        }

        /// <summary>
        /// Декомпозиция операции switch метода FileOperations.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="path"></param>
        static void OperationSwitcher2(int number, string path)
        {
            switch (number)
            {
                case 0:
                    Copy(path);
                    break;
                case 1:
                    Delete(path);
                    break;
                case 2:
                    Replace(path);
                    break;
                case 3:
                    Read(path);
                    break;
                case 4:
                    Concatenation(path);
                    break;
            }
        }

        /// <summary>
        /// Реализация конкатенации двух и более файлов.
        /// </summary>
        /// <param name="path"></param>
        static void Concatenation(string path)
        {
            Console.Clear();
            string text = File.ReadAllText(path, Encoding.UTF8);
            text += "\n";

            string path1 = "";
            bool b = true;
            while (b)
            {
                Console.Clear();
                Console.Write("Введите путь к файлу, который необходимо конкатинировать: ");
                path1 = Console.ReadLine();
                TextGeneration(path, path1, ref text);

                Console.WriteLine("Для продолжения нажмите любую кнопку..");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine(" 1. Добавить еще файл\n" +
                    " 2. Провести конкатинацию");
                int number = Move(1);
                 b = ConcatenationSwitcher(number, b);
            }
            Console.Clear();
            Console.WriteLine(text);
            Console.ReadKey();
        }


        /// <summary>
        /// В этом методе создается текст для конкатинации файлов.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="path1"></param>
        /// <param name="text"></param>
        static void TextGeneration(string path, string path1, ref string text)
        {
            if (File.Exists(path1))
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Extension == ".txt")
                {
                    text += File.ReadAllText(path1, Encoding.UTF8);
                    text += "\n";
                }
                else
                {
                    Console.WriteLine("Неподходящее разрешение.");
                }
            }
            else
            {
                Console.WriteLine("Не существует файл по заданному пути.");
            }
        }

        /// <summary>
        /// Декомпозиция операции switch метода Concatenation.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static bool ConcatenationSwitcher(int number, bool b)
        {
            switch (number)
            {
                case 0:
                    break;
                case 1:
                    b = false;
                    break;
            }
            return b;
        }

        /// <summary>
        /// Чтения из файла в нужной кодировке.
        /// </summary>
        /// <param name="path"></param>
        static void Read(string path)
        {
            Console.Clear();
            Console.WriteLine(" 1. UTF8\n" +
                " 2. ASCII\n" +
                " 3. Unicode\n" +
                " 4. UTF32\n" +
                " 5. Default");
            string text;
            int number = Move(4);
            switch (number)
            {
                case 0:
                    text = File.ReadAllText(path, Encoding.UTF8);
                    break;
                case 1:
                    text = File.ReadAllText(path, Encoding.ASCII);
                    break;
                case 2:
                    text = File.ReadAllText(path, Encoding.Unicode);
                    break;
                case 3:
                    text = File.ReadAllText(path, Encoding.UTF32);
                    break;
                default:
                    text = File.ReadAllText(path, Encoding.Default);
                    break;
            }
            Console.Clear();
            Console.WriteLine(text);
            Console.ReadKey();
        }

        /// <summary>
        /// Копирование файла.
        /// </summary>
        /// <param name="path"></param>
        static void Copy(string path)
        {

            Console.Clear();
            FileInfo fileInf = new FileInfo(path);

            Console.WriteLine(" 1. Ввести путь самостоятельно\n" +
                " 2. Использовать текущую дерикторию");
            int number = Move(1);
            string newPath = "";
            Console.Clear();
            switch (number)
            {
                case 0:

                    Console.Write("Введите адрес дериктории, в которую надо скопировать файл: ");
                    newPath = Console.ReadLine();
                    break;
                case 1:
                    newPath = Directory.GetCurrentDirectory();
                    break;
            }

            newPath += @$"\{fileInf.Name}";
            fileInf.CopyTo(newPath, true);
            Console.Clear();
            Console.WriteLine("Копирование завершено. Для продолжения нажмите любую кнопку.");
            Console.ReadKey();
        }


        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="path"></param>
        static void Delete(string path)
        {
            Console.Clear();
            FileInfo fileInf = new FileInfo(path);
            fileInf.Delete();
            Console.Clear();
            Console.WriteLine("Удаление завершено. Для продолжения нажмите любую кнопку.");
            Console.ReadKey();
        }

        /// <summary>
        /// Перемещение файла.
        /// </summary>
        /// <param name="path"></param>
        static void Replace(string path)
        {
            Console.Clear();
            FileInfo fileInf = new FileInfo(path);

            Console.WriteLine(" 1. Ввести путь самостоятельно\n" +
                " 2. Использовать текущую дерикторию");
            int number = Move(1);
            string newPath = "";
            Console.Clear();
            switch (number)
            {
                case 0:

                    Console.Write("Введите адрес дериктории, в которую надо скопировать файл: ");
                    newPath = Console.ReadLine();
                    break;
                case 1:
                    newPath = Directory.GetCurrentDirectory();
                    break;
            }

            newPath += @$"\{fileInf.Name}";
            fileInf.MoveTo(newPath, true);
            Console.Clear();
            Console.WriteLine("Перемещение завершено. Для продолжения нажмите любую кнопку.");
            Console.ReadKey();
        }
    }
}
