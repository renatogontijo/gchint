using System;
using System.Diagnostics;

namespace hchint
{
    class RunExample
    {
        public void Noise()
        {
            string nums = "123456790";
            int sum = 0;

            for (int i = 0; i < nums.Length; i++)
                sum += int.Parse(nums[i].ToString());
        }

        public void NoiseOp()
        {
            string nums = "123456790";
            int sum = 0;

            for (int i = 0; i < nums.Length; i++)
                sum += (nums[i] - '0');
        }
    }


    static class Program
    {
        private delegate void Func();

        static void Main(string[] args)
        {
            Execute(ExecNonOp);
            Execute(ExecOp1);
            Execute(ExecOp2);

            Console.WriteLine("Ended!");
        }

        private static void Execute(Func f)
        {
            Console.WriteLine(string.Format("Executing {0}...", f.Method.Name));

            Stopwatch sw = new Stopwatch();
            int g0 = GC.CollectionCount(0);
            int g1 = GC.CollectionCount(1);
            int g2 = GC.CollectionCount(2);

            sw.Start();

            f();

            sw.Stop();

            Console.WriteLine(string.Format("G2: {0}", (GC.CollectionCount(2) - g2)));
            Console.WriteLine(string.Format("G1: {0}", (GC.CollectionCount(1) - g1)));
            Console.WriteLine(string.Format("G0: {0}", (GC.CollectionCount(0) - g0)));
            Console.WriteLine(string.Format("Elapsed Time: {0} ms", sw.ElapsedMilliseconds));
            Console.WriteLine();
        }

        private static void ExecNonOp()
        {
            RunExample[] runObj = new RunExample[2_000_000];

            for(int i = 0; i < 2_000_000; i++)
            {
                runObj[i] = new RunExample();
                runObj[i].Noise();
            }
        }

        private static void ExecOp1()
        {
            for(int i = 0; i < 2_000_000; i++)
            {
                RunExample runObj = new RunExample();
                runObj.Noise();
            }
        }

        private static void ExecOp2()
        {
            for(int i = 0; i < 2_000_000; i++)
            {
                RunExample runObj = new RunExample();
                runObj.NoiseOp();
            }
        }
    }
}