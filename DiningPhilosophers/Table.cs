using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace DiningPhilosophersProblem
{
    class Table
    {
        public List<Philosopher> philosophers = new List<Philosopher>();
        public List<Fork> forks = new List<Fork>();
        public bool Dining { get; set; }
        public List<Thread> philosopherThreads = new List<Thread>();
        public Thread statusThread;
        public Table(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                forks.Add(new Fork(i));
                philosophers.Add(new Philosopher(i));
            }

            for (int i = 0; i < amount; i++)
            {
                philosophers[i].LeftFork = forks[i];
                philosophers[i].RightFork = forks[(amount - 1 + i) % amount];
            }
            Dining = true;
        }
        public void ShowStates()
        {
            while (Dining)
            {
                Thread.Sleep(new TimeSpan(0, 0, 2));
                foreach (Philosopher philosopher in philosophers)
                {
                    Console.WriteLine(philosopher.ShowState());
                }
                Console.WriteLine("____________________________________________________");
            }
        }

        internal void EndSimulation()
        {
            Dining = false;
            foreach (Philosopher philosopher in philosophers)
            {
                philosopher.EndMeal();
            }

            statusThread.Join();
            foreach (Thread philosopherThread in philosopherThreads)
            {
                philosopherThread.Join();
            }

            Console.WriteLine("\n END OF DINER \n");
            foreach (Philosopher philosopher in philosophers)
            {
                Console.WriteLine("Philosopher no. " + philosopher.Id + " had " + philosopher.Meals + " meals.");
            }
            Console.WriteLine("");
        }

        public void StartDiner()
        {
            foreach (Philosopher philosopher in philosophers)
            {
                Thread philosopherThread = new Thread(() => philosopher.Act(forks));
                philosopherThread.Start();
                philosopherThreads.Add(philosopherThread);
            }

            statusThread = new Thread(new ThreadStart(ShowStates));
            statusThread.Start();
        }
    }
}
