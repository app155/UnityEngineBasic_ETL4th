using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal class Fibonacci
    {
        public int this[int n]
        {
            get
            {
                if (n < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                if (n > _limit)
                {
                    return Get(n);
                }

                return Cache[n];
            }
        }

        public int[] Cache;
        private int _limit;

        public Fibonacci(int n)
        {
            Get(n);
        }

        public int Get(int n)
        {
            _limit = n;
            Cache = new int[n + 1];
            return F(n);
        }

        private int F(int n)
        {
            if (n <= 1)
                return n;

            if (Cache[n] != 0)
                return Cache[n];

            Cache[n] = F(n - 1) + F(n - 2);
            return Cache[n];
        }
    }
}
