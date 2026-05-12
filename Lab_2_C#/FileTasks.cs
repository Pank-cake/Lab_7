using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lab7
{
    public struct Toy
    {
        private string _name;
        private int _price;
        private int _minAge;
        private int _maxAge;

        public string Name
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                _name = value; 
            }
        }

        public int Price
        {
            get 
            { 
                return _price; 
            }
            set 
            { 
                _price = value; 
            }
        }

        public int MinAge
        {
            get 
            { 
                return _minAge; 
            }
            set 
            { 
                _minAge = value; 
            }
        }

        public int MaxAge
        {
            get 
            { 
                return _maxAge; 
            }
            set 
            { 
                _maxAge = value; 
            }
        }
    }

    public static class FileTasks
    {
        private static Random _random = new Random();

        public static void CreateTask1File(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < 10; i++)
                {
                    writer.WriteLine(_random.Next(-100, 101));
                }
            }
        }

        public static void SolveTask1()
        {
            string path = "task1.txt";
            CreateTask1File(path);

            string[] lines = File.ReadAllLines(path);
            int min = int.Parse(lines[0]);
            int max = int.Parse(lines[0]);

            foreach (string line in lines)
            {
                int number = int.Parse(line);
                if (number < min) min = number;
                if (number > max) max = number;
            }

            Console.WriteLine("Содержимое файла: " + string.Join(", ", lines));
            Console.WriteLine("Результат Min + Max: " + (min + max));
        }

        public static void CreateTask2File(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(_random.Next(1, 50) + " " + _random.Next(1, 50));
                writer.WriteLine(_random.Next(1, 50) + " " + _random.Next(1, 50));
            }
        }

        public static void SolveTask2()
        {
            string path = "task2.txt";
            CreateTask2File(path);

            string[] lines = File.ReadAllLines(path);
            int evenSum = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    int number = int.Parse(part);
                    if (number % 2 == 0) evenSum += number;
                }
            }
            Console.WriteLine("Файл:\n" + File.ReadAllText(path));
            Console.WriteLine("Сумма четных чисел: " + evenSum);
        }

        public static void SolveTask3()
        {
            string inputPath = "task3_input.txt";
            string outputPath = "task3_output.txt";

            Console.WriteLine("Введите текст (пустая строка для завершения):");
            List<string> userLines = new List<string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;
                userLines.Add(input);
            }

            File.WriteAllLines(inputPath, userLines);
            List<string> firstCharacters = new List<string>();

            foreach (string line in File.ReadAllLines(inputPath))
            {
                if (line.Length > 0)
                {
                    firstCharacters.Add(line[0].ToString());
                }
            }

            File.WriteAllLines(outputPath, firstCharacters);
            Console.WriteLine("Первые символы сохранены в " + outputPath);
        }

        public static void CreateTask4File(string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < 10; i++)
                {
                    writer.Write(_random.Next(1, 101));
                }
            }
        }

        public static void SolveTask4()
        {
            string inputPath = "task4.bin";
            string outputPath = "task4_result.bin";
            CreateTask4File(inputPath);

            Console.Write("Исходные данные: ");
            using (BinaryReader reader = new BinaryReader(File.OpenRead(inputPath)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Console.Write(reader.ReadInt32() + " ");
                }
            }

            int k = InputValidator.ReadInteger("\nВведите число k: ");

            using (BinaryReader reader = new BinaryReader(File.OpenRead(inputPath)))
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    if (number % k != 0) writer.Write(number);
                }
            }

            Console.Write("Результат фильтрации: ");
            using (BinaryReader reader = new BinaryReader(File.OpenRead(outputPath)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Console.Write(reader.ReadInt32() + " ");
                }
            }
            Console.WriteLine();
        }

        public static void SolveTask5()
        {
            string path = "toys.xml";
            List<Toy> toysList = new List<Toy>();

            Toy doll = new Toy(); doll.Name = "Кукла"; doll.Price = 1500; doll.MinAge = 2; doll.MaxAge = 7;
            Toy ball = new Toy(); ball.Name = "Мяч"; ball.Price = 500; ball.MinAge = 1; ball.MaxAge = 15;
            Toy car = new Toy(); car.Name = "Машинка"; car.Price = 200; car.MinAge = 1; car.MaxAge = 15;
            toysList.Add(doll); toysList.Add(ball);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, toysList);
            }

            Console.WriteLine("Игрушки для ребенка 3-х лет (кроме мячей):");
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                List<Toy> deserializedToys = (List<Toy>)serializer.Deserialize(stream);
                foreach (Toy toy in deserializedToys)
                {
                    if (toy.Name.ToLower() != "мяч" && toy.MinAge <= 3 && toy.MaxAge >= 3)
                    {
                        Console.WriteLine(toy.Name + " — " + toy.Price + " руб.");
                    }
                }
            }
        }
    }
}
