using System;
using System.Collections.Generic;
using System.IO;

namespace lab7
{
    public static class CollectionTasks
    {
        public static void SolveTask6()
        {
            Console.Clear();
            Console.WriteLine("Задание 6. Перевернуть List\n");

            Console.WriteLine("Введите целые числа (пустая строка — конец ввода):");
            List<int> list = new List<int>();

            int index = 1;
            while (true)
            {
                bool isEmpty;
                int number = InputValidator.ReadIntWithEmpty($"Элемент {index}: ", out isEmpty);
                if (isEmpty) break;

                list.Add(number);
                index++;
            }

            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                Console.Write("\nНажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nИсходный: " + string.Join(", ", list));

            int count = list.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int temp = list[i];
                list[i] = list[count - 1 - i];
                list[count - 1 - i] = temp;
            }

            Console.WriteLine("Перевёрнутый: " + string.Join(", ", list));
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public static void SolveTask7()
        {
            Console.Clear();
            Console.WriteLine("Задание 7. Вставка F слева и справа от E (LinkedList)\n");

            Console.WriteLine("Введите целые числа (пустая строка — конец ввода):");
            LinkedList<int> list = new LinkedList<int>();

            int index = 1;
            while (true)
            {
                bool isEmpty;
                int number = InputValidator.ReadIntWithEmpty($"Элемент {index}: ", out isEmpty);
                if (isEmpty) break;

                list.AddLast(number);
                index++;
            }

            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                Console.Write("\nНажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            Console.Write("\nИсходный: ");
            PrintLinkedList(list);

            int e = InputValidator.ReadInt("Введите E: ");
            int f = InputValidator.ReadInt("Введите F: ");

            LinkedListNode<int> node = list.First;
            while (node != null)
            {
                if (node.Value == e)
                {
                    list.AddBefore(node, f);
                    list.AddAfter(node, f);
                    node = node.Next;
                }
                node = node.Next;
            }

            Console.Write("Результат: ");
            PrintLinkedList(list);
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void PrintLinkedList(LinkedList<int> list)
        {
            LinkedListNode<int> node = list.First;
            while (node != null)
            {
                Console.Write(node.Value);
                node = node.Next;
                if (node != null) Console.Write(" - ");
            }
            Console.WriteLine();
        }

        public static void SolveTask8()
        {
            Console.Clear();
            Console.WriteLine("Задание 8. Дискотеки (HashSet)\n");

            int n = InputValidator.ReadIntPositive("Количество студентов: ");

            HashSet<string>[] students = new HashSet<string>[n];

            for (int i = 0; i < n; i++)
            {
                string input = InputValidator.ReadString($"Дискотеки студента {i + 1} (через запятую): ");
                string[] discos = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                students[i] = new HashSet<string>();
                for (int j = 0; j < discos.Length; j++)
                {
                    string trimmed = discos[j].Trim();
                    if (trimmed.Length > 0) students[i].Add(trimmed);
                }
            }

            HashSet<string> all = new HashSet<string>(students[0]);
            for (int i = 1; i < n; i++)
                all.IntersectWith(students[i]);

            HashSet<string> some = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                foreach (string disco in students[i])
                    some.Add(disco);
            }

            HashSet<string> none = new HashSet<string>();
            foreach (string disco in some)
            {
                bool visited = false;
                for (int i = 0; i < n; i++)
                {
                    if (students[i].Contains(disco)) { visited = true; break; }
                }
                if (!visited) none.Add(disco);
            }

            Console.WriteLine("\nПосещали все: " + (all.Count > 0 ? string.Join(", ", all) : "нет"));
            Console.WriteLine("Посещали некоторые: " + (some.Count > 0 ? string.Join(", ", some) : "нет"));
            Console.WriteLine("Не посещал никто: " + (none.Count > 0 ? string.Join(", ", none) : "нет"));
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public static void SolveTask9()
        {
            Console.Clear();
            Console.WriteLine("Задание 9. Символы в чётных словах\n");

            string filePath = "task9.txt";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "мама мыла раму быстро и аккуратно");
                Console.WriteLine($"Создан файл '{filePath}' с примером.\n");
            }
            else Console.WriteLine($"Чтение из файла '{filePath}'.\n");

            string text = File.ReadAllText(filePath);
            Console.WriteLine("Текст: " + text);

            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("\nСлова:");
            for (int i = 0; i < words.Length; i++)
                Console.WriteLine($"  {i + 1}: {words[i]}");

            HashSet<char> evenChars = new HashSet<char>();
            for (int i = 1; i < words.Length; i += 2)
            {
                string word = words[i];
                for (int j = 0; j < word.Length; j++)
                {
                    char c = word[j];
                    if (char.IsLetter(c)) evenChars.Add(char.ToLower(c));
                }
            }

            List<char> sorted = new List<char>(evenChars);
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    if (sorted[i] > sorted[j])
                    {
                        char temp = sorted[i];
                        sorted[i] = sorted[j];
                        sorted[j] = temp;
                    }
                }
            }

            Console.WriteLine("\nБуквы в чётных словах: " + new string(sorted.ToArray()));
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void FillTextFileTask10(string filePath)
        {
            Console.WriteLine("Ввод данных абитуриентов.");
            Console.WriteLine("Формат: Фамилия Имя Балл1 Балл2 Балл3");
            Console.WriteLine("Пустая строка — конец ввода.\n");

            List<string> lines = new List<string>();
            int index = 1;

            string firstLine = InputValidator.ReadNonEmptyLine($"Абитуриент {index}: ");
            string[] firstParts = firstLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            while (firstParts.Length != 5 ||
                   !int.TryParse(firstParts[2], out int b1) ||
                   !int.TryParse(firstParts[3], out int b2) ||
                   !int.TryParse(firstParts[4], out int b3))
            {
                Console.WriteLine("  Ошибка: нужен формат: Фамилия Имя Б1 Б2 Б3");
                firstLine = InputValidator.ReadNonEmptyLine($"Абитуриент {index}: ");
                firstParts = firstLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            lines.Add(firstLine);
            index++;

            while (true)
            {
                Console.Write($"Абитуриент {index}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) break;

                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 5 &&
                    int.TryParse(parts[2], out int bb1) &&
                    int.TryParse(parts[3], out int bb2) &&
                    int.TryParse(parts[4], out int bb3))
                {
                    lines.Add(input);
                    index++;
                }
                else
                {
                    Console.WriteLine("  Неверный формат. Нужно: Фамилия Имя Б1 Б2 Б3");
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines.Count; i++)
                    writer.WriteLine(lines[i]);
            }

            Console.WriteLine($"\nСохранено {lines.Count} абитуриентов в '{filePath}'.\n");
        }

        public static void SolveTask10()
        {
            Console.Clear();
            Console.WriteLine("Задание 10. Абитуриенты\n");

            string filePath = "task10.txt";
            FillTextFileTask10(filePath);

            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("Данные из файла:");
            for (int i = 0; i < lines.Length; i++)
                Console.WriteLine("  " + lines[i]);

            Console.WriteLine("\nУсловия: каждый предмет >= 30, сумма >= 140.\n");

            SortedDictionary<string, string> admitted = new SortedDictionary<string, string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 5) continue;

                string surname = parts[0];
                string name = parts[1];
                int b1 = int.Parse(parts[2]);
                int b2 = int.Parse(parts[3]);
                int b3 = int.Parse(parts[4]);
                int sum = b1 + b2 + b3;

                Console.Write($"{surname} {name}: сумма={sum}");

                if (b1 >= 30 && b2 >= 30 && b3 >= 30 && sum >= 140)
                {
                    string key = surname + " " + name;
                    admitted[key] = key;
                    Console.WriteLine(" -> ДОПУЩЕН");
                }
                else
                {
                    Console.Write(" -> НЕ допущен");
                    if (b1 < 30) Console.Write($" (балл1={b1})");
                    if (b2 < 30) Console.Write($" (балл2={b2})");
                    if (b3 < 30) Console.Write($" (балл3={b3})");
                    if (sum < 140) Console.Write($" (сумма={sum})");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nДопущенные (по алфавиту):");
            if (admitted.Count > 0)
            {
                foreach (string student in admitted.Keys)
                    Console.WriteLine("  " + student);
            }
            else Console.WriteLine("  нет");

            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}