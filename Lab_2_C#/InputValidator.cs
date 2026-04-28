using System;

namespace lab7
{
    public static class InputValidator
    {
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: введите число.");
                    continue;
                }

                int result;
                if (int.TryParse(input, out result))
                    return result;

                Console.WriteLine("Ошибка: введите целое число.");
            }
        }

        public static int ReadIntNonZero(string prompt)
        {
            while (true)
            {
                int result = ReadInt(prompt);

                if (result != 0)
                    return result;

                Console.WriteLine("Ошибка: число не должно быть нулём.");
            }
        }

        public static int ReadIntPositive(string prompt)
        {
            while (true)
            {
                int result = ReadInt(prompt);

                if (result > 0)
                    return result;

                Console.WriteLine("Ошибка: число должно быть положительным.");
            }
        }

        public static int ReadIntNonNegative(string prompt)
        {
            while (true)
            {
                int result = ReadInt(prompt);

                if (result >= 0)
                    return result;

                Console.WriteLine("Ошибка: число не должно быть отрицательным.");
            }
        }

        public static int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                int result = ReadInt(prompt);

                if (result >= min && result <= max)
                    return result;

                Console.WriteLine($"Ошибка: число должно быть от {min} до {max}.");
            }
        }

        public static int ReadIntGreaterOrEqual(string prompt, int minValue)
        {
            while (true)
            {
                int result = ReadInt(prompt);

                if (result >= minValue)
                    return result;

                Console.WriteLine($"Ошибка: число должно быть не меньше {minValue}.");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: введите число.");
                    continue;
                }

                decimal result;
                if (decimal.TryParse(input, out result))
                    return result;

                Console.WriteLine("Ошибка: введите десятичное число.");
            }
        }

        public static decimal ReadDecimalPositive(string prompt)
        {
            while (true)
            {
                decimal result = ReadDecimal(prompt);

                if (result > 0)
                    return result;

                Console.WriteLine("Ошибка: число должно быть положительным.");
            }
        }

        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Ошибка: строка не должна быть пустой.");
            }
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static string ReadNonEmptyLine(string prompt)
        {
            while (true)
            {
                string result = ReadString(prompt);

                if (!string.IsNullOrWhiteSpace(result))
                    return result;

                Console.WriteLine("Ошибка: введите хотя бы одну непустую строку.");
            }
        }

        public static int ReadIntWithEmpty(string prompt, out bool isEmpty)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    isEmpty = true;
                    return 0;
                }

                int result;
                if (int.TryParse(input, out result))
                {
                    isEmpty = false;
                    return result;
                }

                Console.WriteLine("Ошибка: введите целое число или пустую строку.");
            }
        }

        public static string ReadLineWithEmpty(string prompt, out bool isEmpty)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                isEmpty = true;
                return string.Empty;
            }

            isEmpty = false;
            return input;
        }
    }
}