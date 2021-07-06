using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lesson03
{
    class Program
    {
        private const int count = 900_000_000;
        static PointClass[] pointClass = new PointClass[count];
        static PointStructD[] pointStructD = new PointStructD[count];
        static PointStructF[] pointStructF = new PointStructF[count];

        static void Main(string[] args)
        {
            //temp.runTemp();

            FillArrays();

            Console.WriteLine($"Кол-во повторений в цикле: {count}\n" +
                $"Обычный метод расчёта дистанции со ссылочным типом(float): {MyBenchmarkRunner.Start(BulkGetDistanceOnClass)}\n" +
                $"Обычный метод расчёта дистанции со значимым типом(float) : {MyBenchmarkRunner.Start(BulkGetDistanceOnStructF)}\n" +
                $"Обычный метод расчёта дистанции со значимым типом(double): {MyBenchmarkRunner.Start(BulkGetDistanceOnStructD)}\n" +
                $"Расчёт дистанции без квадратного корня со значимым типом : {MyBenchmarkRunner.Start(BulkGetFastDistanceOnStructF)}");
        }

        private static void FillArrays()
        {
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                pointClass[i] = new PointClass((float)(rand.NextDouble() * count), (float)(rand.NextDouble() * count));
                pointStructD[i] = new PointStructD(rand.NextDouble() * count, rand.NextDouble() * count);
                pointStructF[i] = new PointStructF((float)(rand.NextDouble() * count), (float)(rand.NextDouble() * count));
            }
        }

        static void BulkGetDistanceOnClass()
        {
            for (int i = 0; i < count - 2; i++)
                _ = pointClass[i].GetDistance(pointClass[i + 1]);
        }

        static void BulkGetDistanceOnStructF()
        {
            for (int i = 0; i < count - 2; i++)
                _ = pointStructF[i].GetDistance(pointStructF[i + 1]);
        }

        static void BulkGetDistanceOnStructD()
        {
            for (int i = 0; i < count - 2; i++)
                _ = pointStructD[i].GetDistance(pointStructD[i + 1]);
        }

        static void BulkGetFastDistanceOnStructF()
        {
            for (int i = 0; i < count - 2; i++)
                _ = pointStructF[i].GetFastDistance(pointStructF[i + 1]);
        }
    }
    #region Point class and structures
    class PointClass
    {
        public float X;
        public float Y;

        public PointClass(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float GetDistance(PointClass other)
            => MathF.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
    }

    struct PointStructD
    {
        public double X;
        public double Y;

        public PointStructD(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double GetDistance(PointStructD other)
            => Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
    }

    struct PointStructF
    {
        public float X;
        public float Y;

        public PointStructF(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float GetDistance(PointStructF other)
            => MathF.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));

        public float GetFastDistance(PointStructF other)
            => (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);
    }
    #endregion

    class MyBenchmarkRunner
    {
        public static long Start(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            action();

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
