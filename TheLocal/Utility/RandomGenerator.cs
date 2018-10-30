using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLocal.Utility {
    public class RandomGenerator {
        const string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()`~-_=+|]}[{;:'/?.>,<";

        private Random m_random;

        public RandomGenerator(int seed) {
            m_random = new Random(seed);
        }

        public string GenerateString(int length) {
            return new string(Enumerable.Repeat(CHARS, length)
                             .Select(s => s[m_random.Next(s.Length)])
                             .ToArray());
        }

    }
}
