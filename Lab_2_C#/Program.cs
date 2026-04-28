using lab7;
using System;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Задания 1-5:");
                Console.WriteLine("  1. Сумма max и min");
                Console.WriteLine("  2. Сумма чётных");
                Console.WriteLine("  3. Первые символы строк");
                Console.WriteLine("  4. Исключить кратные k");
                Console.WriteLine("  5. Игрушки для 3 лет (XML)");
                Console.WriteLine();
                Console.WriteLine("Задания 6-10:");
                Console.WriteLine("  6. Перевернуть List");
                Console.WriteLine("  7. Вставка в LinkedList");
                Console.WriteLine("  8. Дискотеки (HashSet)");
                Console.WriteLine("  9. Символы в чётных словах");
                Console.WriteLine("  10. Абитуриенты");
                Console.WriteLine();
                Console.WriteLine("  0. Выход");

                int choice = InputValidator.ReadIntInRange("\nВыбор: ", 0, 10);

                if (choice == 0) break;

                Console.Clear();

                switch (choice)
                {
                    case 1: FileTasks.SolveTask1(); break;
                    case 2: FileTasks.SolveTask2(); break;
                    case 3: FileTasks.SolveTask3(); break;
                    case 4: FileTasks.SolveTask4(); break;
                    case 5: FileTasks.SolveTask5(); break;
                    case 6: CollectionTasks.SolveTask6(); break;
                    case 7: CollectionTasks.SolveTask7(); break;
                    case 8: CollectionTasks.SolveTask8(); break;
                    case 9: CollectionTasks.SolveTask9(); break;
                    case 10: CollectionTasks.SolveTask10(); break;
                }
            }
        }
    }
}