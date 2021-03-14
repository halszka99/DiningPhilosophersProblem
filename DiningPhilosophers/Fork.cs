using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DiningPhilosophersProblem
{
    class Fork
    {
        public Fork(int Id)
        {
            this.Id = Id;
            IsAvailable = true;
        }

        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public Mutex mutex = new Mutex(); 
    }
}
