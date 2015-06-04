using System;

namespace Test
{
    public class ClassA
    {
        public void function1()
        {
            System.Console.WriteLine("Hello, World!");
        }
    }

    public class ClassB : ClassA
    {
        public struct CoOrds
        {
            public int x, y;

            public void function2(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }

        CoOrds coords1;
    }

    public class ClassC
    {
       public void function3()
        {
            ClassA a = new ClassA();
        }
    }

    public class ClassD
    {

        void main(ClassA a, ClassB b)
        {
            { }
            if(true)
                Console.Write("Braceless sscope");
        }

    }
}
