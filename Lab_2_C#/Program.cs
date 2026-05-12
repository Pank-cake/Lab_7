using System;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Задание 1 | 2. Задание 2 | 3. Задание 3");
                Console.WriteLine("4. Задание 4 | 5. Задание 5 | 6. Задание 6");
                Console.WriteLine("7. Задание 7 | 8. Задание 8 | 9. Задание 9");
                Console.WriteLine("10. Задание 10 | 0. Выход");

                int choice = InputValidator.ReadInteger("\nВыберите номер задачи: ");

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
                    case 0: isRunning = false; break;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                    Console.ReadKey();
                }
            }
        }
    }
}
