using System;
using System.Diagnostics;
using static System.Math;

/// <summary>
///The prime factors of 13195 are 5, 7, 13 and 29.
///What is the largest prime factor of the number 600851475143 ?
/// </summary>

namespace The_Project_Euler
{
    internal class Program
    {
        public static bool IsPrime(long number)//O(sqrt(number))
        {
            for (var i = 2; i <= (int)Sqrt(number); i++)
                if (number % i == 0)
                    return false;
            return number >= 2;
        }

        public static bool IsPrimeFerma(long number)//O(100 * log N * log N)
        {
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var randNum = (random.Next() % (number - 2)) + 2;

                if (FindGcf(randNum, number) != 1)
                    return false;
                if (Pow(randNum, number - 1, number) != 1)
                    return false;
            }
            return number >= 2;
        }

        public static long Mul(long a, long b, long m)
        {
            if (b == 1)
                return a;
            if (b % 2 != 0) return (Mul(a, b - 1, m) + a) % m;

            var t = Mul(a, b / 2, m);
            return (2 * t) % m;
        }

        public static long FindGcf(long a, long b)
        {
            return b == 0 ? a : FindGcf(b, a % b);
        }

        public static long Pow(long a, long b, long m)
        {
            if (b == 0)
                return 1;
            if (b % 2 == 0)
            {
                var t = Pow(a, b / 2, m);
                return Mul(t, t, m) % m;
            }
            return (Mul(Pow(a, b - 1, m), a, m)) % m;
        }

        public static long GetPrimeNumDivision(long num)
        {
            long i;

            for (i = 2; !(num % i == 0 && IsPrimeFerma(num / i)); i++)
            { }
            return num / i;
        }

        private static void Main()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"The biggest simple divider: {GetPrimeNumDivision(0x8BE589EAC7)}");
            stopwatch.Stop();

            Console.WriteLine($"Elapsed Milliseconds: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
