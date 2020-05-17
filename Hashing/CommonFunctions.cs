using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    /// <summary>
    /// One of the Collision Handling Technique utilized in Hashing
    /// </summary>
    public static class OpenAddressing
    {
        /// <summary>
        /// Hi(x) = (Hash(x) + i) % HashtableSize
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iteration"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int LinearProbling(int key, int iteration, int size) => (CommonUtility.HashFunc1(key, size) + iteration) % size;

        /// <summary>
        /// Hi(x) = (Hash(x) + i raised to power 2) % HashtableSize
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iteration"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int QuadraticProbling(int key, int iteration, int size) => (CommonUtility.HashFunc1(key, size) + (int)Math.Pow(iteration, 2)) % size;

        /// <summary>
        /// Hi(x) = (Hash1(x) + i * Hash2(x)) % HashtableSize
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iteration"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int DoubleHashing(int key, int iteration, int size) => (CommonUtility.HashFunc1(key, size) + iteration * CommonUtility.HashFunc2(key, size)) % size;
    }

    public static class CommonUtility
    {
        public static int HashFunc1(int key, int size) => key % size;

        public static int HashFunc2(int key, int size)
        {
            int primeNo = FindPrime(size);
            return (primeNo - key % primeNo);
        }

        public static int FindPrime(int num)
        {
            if (num < 2)
                return num;

            int prime = 1;
            // loop to iterate thru all integers smaller than num to find larget prime no
            for (prime = num - 1; prime > 1; prime--)
            {
                bool isPrime = true;
                // loop to check given number is prime or not
                for (int i = (int)Math.Sqrt(prime); i > 1; i--)
                {
                    if (prime % i == 0)
                    { isPrime = false; break; }
                }
                // break outer loop if prime found
                if (isPrime)
                    break;
            }
            return prime;
        }
    }
}
