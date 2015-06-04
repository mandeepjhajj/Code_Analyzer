using System;

namespace Test
{
    public class EnumTest
    {
        enum Days { Sun, Mon, Tue, Wed, Thu, Fri, Sat };

        static void Main()
        {
            int x = (int)Days.Sun;
            
            Console.WriteLine("Sun = {0}", x);
            Console.WriteLine("Fri = {0}", y);
        }

        public delegate void ProgressReporter(int percentComplete);

    }
}

namespace Test1
{
    class A
    {
        Test.EnumTest obj = new Test.EnumTest();
        
    }
}