using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

/// <summary> 
//If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.
//The sum of these multiples is 23.
//Find the sum of all the multiples of 3 or 5 below 1000.
/// </summary>

namespace The_Project_Euler
{
    public delegate int Sum(int limit);

    internal class Program
    {
        public static int GetSumUseCycle(int limit)
        {
            var sum = 0;

            for (var i = 3; i < limit; i++)
                if (i % 3 == 0 || i % 5 == 0)
                    sum += i;
            return sum;
        }

        public static int GetSumUseLinq(int limit) => Enumerable.Range(1, limit - 1).Where(x => x % 3 == 0 || x % 5 == 0).Sum();


        public static double SumProgression(int limit, int difference)
        {
            var countMultiplesOf = limit / difference;

            return ((double)(2 * difference + (countMultiplesOf - 1) * difference) / 2) * countMultiplesOf;
        }

        public static int GetSumUseProgression(int limit)
        {
            limit -= 1;

            return (int)(SumProgression(limit, 3) + SumProgression(limit, 5) - SumProgression(limit, 15));
        }

        public static void ShowResFunc(int limit, params Sum[] functions)
        {
            var stopWatch = new Stopwatch();
            foreach (var func in functions)
            {
                stopWatch.Start();
                Console.Write($"{func.GetMethodInfo().Name,-20} Sum: {func.Invoke(limit)}");
                stopWatch.Stop();

                Console.WriteLine($" Ticks: {stopWatch.ElapsedTicks}");
                stopWatch.Reset();
            }
        }

        private static void Main()
        {
            Console.Write("Enter limit: ");

            var limitStr = Console.ReadLine();

            if (int.TryParse(limitStr, out var result))
                ShowResFunc(result, GetSumUseCycle, GetSumUseLinq, GetSumUseProgression);
            else
                Console.WriteLine("Enter correct limit");        
        }
    }
}
