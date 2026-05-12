using System;
using System.Collections.Generic;
using System.IO;

namespace Lab7
{
    public static class CollectionTasks
    {
        public static void SolveTask6()
        {
            string input = InputValidator.ReadString("Введите целые числа через пробел: ");
            List<int> numbers = new List<int>();
            foreach (string part in input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(part, out int n)) numbers.Add(n);
            }

            int count = numbers.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int temp = numbers[i];
                numbers[i] = numbers[count - 1 - i];
                numbers[count - 1 - i] = temp;
            }
            Console.WriteLine("Результат переворота: " + string.Join(" ", numbers));
        }

        public static void SolveTask7()
        {
            string input = InputValidator.ReadString("Введите числа для LinkedList через пробел: ");
            LinkedList<double> list = new LinkedList<double>();
            foreach (string part in input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (double.TryParse(part, out double d)) list.AddLast(d);
            }

            double targetValue = InputValidator.ReadDouble("Перед и после какого числа вставить (E): ");
            double newValue = InputValidator.ReadDouble("Какое число вставить (F): ");

            LinkedListNode<double> currentNode = list.First;
            while (currentNode != null)
            {
                LinkedListNode<double> nextNode = currentNode.Next;
                if (currentNode.Value == targetValue)
                {
                    list.AddBefore(currentNode, newValue);
                    list.AddAfter(currentNode, newValue);
                }
                currentNode = nextNode;
            }
            Console.WriteLine("Результат: " + string.Join(" ", list));
        }

        public static void SolveTask8()
        {
            Console.WriteLine("Задание 8. Посещение дискотек (HashSet)");

            string discoInput = InputValidator.ReadString("Введите названия всех дискотек города через пробел: ");
            HashSet<string> allDiscos = new HashSet<string>(discoInput.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            int studentsCount = InputValidator.ReadInteger("Введите количество студентов в группе: ");

            List<HashSet<string>> studentVisits = new List<HashSet<string>>();

            for (int i = 0; i < studentsCount; i++)
            {
                string visits = InputValidator.ReadString($"Какие дискотеки посетил студент {i + 1} (через пробел): ");
                HashSet<string> visitedByStudent = new HashSet<string>(visits.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                studentVisits.Add(visitedByStudent);
            }

            if (studentVisits.Count == 0) return;

            HashSet<string> visitedByAll = new HashSet<string>(studentVisits[0]);
            foreach (HashSet<string> currentSet in studentVisits)
            {
                visitedByAll.IntersectWith(currentSet);
            }

            HashSet<string> visitedBySome = new HashSet<string>();
            foreach (HashSet<string> currentSet in studentVisits)
            {
                visitedBySome.UnionWith(currentSet);
            }

            HashSet<string> visitedByNone = new HashSet<string>(allDiscos);
            visitedByNone.ExceptWith(visitedBySome);

            Console.WriteLine("\nРезультаты:");
            Console.WriteLine("1. Ходили все студенты: " + 
                    (visitedByAll.Count > 0 ? string.Join(", ", visitedByAll) : "таких нет"));
            Console.WriteLine("2. Ходили некоторые (хотя бы один): " + 
                    (visitedBySome.Count > 0 ? string.Join(", ", visitedBySome) : "таких нет"));
            Console.WriteLine("3. Не ходил никто из студентов: " + 
                    (visitedByNone.Count > 0 ? string.Join(", ", visitedByNone) : "таких нет"));
        }

        public static void SolveTask9()
        {
            string text = InputValidator.ReadString("Введите предложение: ");
            string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<char> uniqueLetters = new HashSet<char>();

            for (int i = 0; i < words.Length; i++)
            {
                if ((i + 1) % 2 == 0) // Четные слова
                {
                    foreach (char c in words[i].ToLower())
                    {
                        if (char.IsLetter(c)) uniqueLetters.Add(c);
                    }
                }
            }

            List<char> result = new List<char>(uniqueLetters);
            result.Sort();
            Console.WriteLine("Буквы из четных слов: " + string.Join(" ", result));
        }

        public static void SolveTask10()
        {
            string path = "applicants.txt";
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, new[] { "4", "Романов Вельямин 48 39 55", "Иванов Иван 30 30 85" });
            }

            string[] lines = File.ReadAllLines(path);
            if (lines.Length == 0) return;

            int n = int.Parse(lines[0]);
            SortedDictionary<string, string> admittedList = new SortedDictionary<string, string>();

            for (int i = 1; i <= n && i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 5)
                {
                    int b1 = int.Parse(parts[2]);
                    int b2 = int.Parse(parts[3]);
                    int b3 = int.Parse(parts[4]);

                    if (b1 >= 30 && b2 >= 30 && b3 >= 30 && (b1 + b2 + b3) >= 140)
                    {
                        admittedList.Add(parts[0] + " " + parts[1], "");
                    }
                }
            }

            Console.WriteLine("Допущены к экзаменам:");
            foreach (var entry in admittedList)
            {
                Console.WriteLine(entry.Key);
            }
        }
    }
}
