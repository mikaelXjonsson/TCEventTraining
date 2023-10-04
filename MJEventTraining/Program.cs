using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJEventTraining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Counter test = new Counter();

            // Likt som TC kallade metoden i andra klassen för sändare (Publisher?)
            // så eftersom detta är lyssnaren enligt TC så blir det Subscriber?
            test.CounterDone += Test_CounterDone;

            test.CountToThousand();
            Console.ReadLine();
        }

        private static void Test_CounterDone(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }

    public class Counter
    {
        public event EventHandler<string> CounterDone;

        public void CountToThousand()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine(i);
                if (i == 99)
                {
                    // Koden fungerar även utan ? men då kommer den att kasta ett exception om 
                    // CounterDone är null. Nu tillåts den vara null och hoppar bara över det här
                    // om så är fallet
                    // EFtersom det här är sändaren enligt TC så är det alltså Publisher?
                    CounterDone?.Invoke(this, "You have reach 99");
                }
            }
        }
    }
}
