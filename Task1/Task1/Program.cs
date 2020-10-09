using System;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            var random = new Random();
            int answer = random.Next(0, 100);
            Console.WriteLine("Привет! Хочешь поиграть в игру? Отгадай число от 0 до 99. У тебя 6 попыток.");
            int counter = 0;
            var isSolved = false;
            while (counter < 6)
            {
                Console.WriteLine("Введи свое предположение:");
                var str = Console.ReadLine();
                if (int.TryParse(str, out var inputNumber))
                {
                    if (inputNumber > answer)
                    {
                        Console.WriteLine("Неверно! Загаданное число меньше введенного.");
                    }
                    if (inputNumber < answer)
                    {
                        Console.WriteLine("Неверно! Загаданное число больше введенного.");
                    }
                    else if (inputNumber == answer)
                    {
                        Console.WriteLine("Верно! Вы выиграли!");
                        isSolved = true;
                        break;
                    }
                    counter++;
                }
                else
                {
                    if (str != null && str[0] == 'z')
                    {
                        Console.WriteLine("До свидания!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Извините, но вы ввели неподходящие парамметры! До свидания!");
                    }
                } 
            }
            if (!isSolved)
            {
                Console.WriteLine("Вы проиграли. Удачи в следующей попытке!");
            }
        }
    }
}