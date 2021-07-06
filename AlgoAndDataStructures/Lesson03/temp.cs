using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lesson03
{
    public class temp
    {
        public static void runTemp()
        {
            Console.WriteLine(Test(() => SlowSQRT()));
            Console.WriteLine(Test(() => FastSQRT()));
            Console.WriteLine("---");
            Console.WriteLine(Test(() => F()));
            Console.WriteLine(Test(() => S()));
        }

        #region MyRegion

        public static long Test(Action func)
        {
            const int count = 90_000_000;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                func();
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        public static double SlowSQRT()
        {
            double z = 999;
            if (z == 0) return 0;
            return Math.Sqrt(z);
        }

        public static float FastSQRT()
        {
            float z = 999;
            if (z == 0) return 0;
            FloatIntUnion u;
            u.i = 0;
            u.f = z;
            u.i -= 1 << 23; /* Subtract 2^m. */
            u.i >>= 1; /* Divide by 2. */
            u.i += 1 << 29; /* Add ((b + 1) / 2) * 2^m. */
            return u.f;
        }

        public static double F()
        {
            int x = 2, y = 3;
            return Math.Sqrt(x * x + y * y);
        }

        public static double S()
        {
            int x = 2, y = 3;
            return x * x + y * y;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct FloatIntUnion
        {
            [FieldOffset(0)]
            public int i;

            [FieldOffset(0)]
            public float f;
        }
        #endregion
    }
}
