using System;
using System.Threading;

namespace DiningPhilosophersProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many philosophers will eat today?");
            int amount = int.Parse(Console.ReadLine());
            Table table = new Table(amount);

            foreach (Philosopher philosopher in table.philosophers)
            {
                Console.WriteLine("Philosopher no. " + philosopher.Id + " has " + philosopher.LeftFork.Id + " as a left fork and " + philosopher.RightFork.Id + " as a right fork");
            }
            Console.WriteLine("____________________________________________________");
            table.StartDiner();

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;
            }

            table.EndSimulation();
        }
    }
}
