using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DiningPhilosophersProblem
{
    enum PhilosopherState { Eating, Thinking }
    class Philosopher
    {
        public int Id { get; set; }
        public PhilosopherState PhilosopherState { get; set; }
        public Fork LeftFork { get; set; }
        public Fork RightFork { get; set; }
        public int Meals { get; set; }
        public bool IsDining { get; set; }
        Random random = new Random();
        public Philosopher(int id)
        {
            Id = id;
            PhilosopherState = PhilosopherState.Thinking;
            Meals = 0;
            IsDining = true;
        }
        public bool TakeLeftFork()
        {
            LeftFork.mutex.WaitOne();
            if (LeftFork.IsAvailable)
            {
                LeftFork.IsAvailable = false;
                LeftFork.mutex.ReleaseMutex();
                return true;
            }
            LeftFork.mutex.ReleaseMutex();
            return false;
        }
        public bool TakeRightFork()
        {
            RightFork.mutex.WaitOne();
            if (RightFork.IsAvailable)
            {
                RightFork.IsAvailable = false;
                RightFork.mutex.ReleaseMutex();
                return true;
            }
            RightFork.mutex.ReleaseMutex();
            return false;
        }
        public void PutLeftFork()
        {
            LeftFork.mutex.WaitOne();
            LeftFork.IsAvailable = true;
            LeftFork.mutex.ReleaseMutex();
        }
        public void PutRightFork()
        {
            RightFork.mutex.WaitOne();
            RightFork.IsAvailable = true;
            RightFork.mutex.ReleaseMutex();
        }
        public void Eat()
        {
            Meals++;
            PhilosopherState = PhilosopherState.Eating;
            Thread.Sleep(new TimeSpan(0, 0, 0, random.Next(2, 4), random.Next(0, 1000)));
            //Thread.Sleep(new TimeSpan(0, 0, 2));
            PhilosopherState = PhilosopherState.Thinking;
            PutLeftFork();
            PutRightFork();
            Thread.Sleep(new TimeSpan(0, 0, 0, random.Next(2, 4), random.Next(0, 1000)));
            //Thread.Sleep(new TimeSpan(0, 0, 2));
        }
        public void EndMeal()
        {
            IsDining = false;
            PhilosopherState = PhilosopherState.Thinking;
        }
        public string ShowState()
        {
            if (PhilosopherState == PhilosopherState.Eating)
                return "Philosopher no. " + Id + " is eating. ";
            else
                return "Philosopher no. " + Id + " is thinking. ";
        }
        public void Act(List<Fork> forks)
        {
            while (IsDining)
            {
                if (LeftFork.Id < RightFork.Id)
                {
                    if (TakeLeftFork())
                    {
                        if (TakeRightFork())
                        {
                            Eat();
                        }
                        else
                        {
                            PutLeftFork();
                        }
                    }
                }
                else
                {
                    if (TakeRightFork())
                    {
                        if (TakeLeftFork())
                        {
                            Eat();
                        }
                        else
                        {
                            PutRightFork();
                        }
                    }
                }

                //List<Fork> freeForks = new List<Fork>(); 
                //foreach (Fork fork in forks)
                //{
                //    if (fork.IsAvailable)
                //        freeForks.Add(fork); 
                //}
                //if (freeForks.Count > 1)
                //{
                //    LeftFork = freeForks[0];
                //    if (TakeLeftFork())
                //    {
                //        RightFork = freeForks[1];
                //        if (TakeRightFork())
                //        {
                //            Eat(); 
                //        }
                //        else 
                //        {
                //            PutLeftFork(); 
                //        }
                //    }

                //}

            }
        }
    }
}
