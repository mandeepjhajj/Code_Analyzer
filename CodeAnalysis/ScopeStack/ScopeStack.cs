using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCS
{
    public class ScopeStack<E>
    {
        List<E> stack_ = new List<E>();
        E lastPopped_;

        //----< push element onto stack >------------------------------------

        public void push(E elem)
        {
            stack_.Add(elem);
        }
        //----< pop element off of stack >-----------------------------------

        public E pop()
        {
            int len = stack_.Count;
            if (len == 0)
                throw new Exception("empty scope stack");
            E elem = stack_[len - 1];
            stack_.RemoveAt(len - 1);
            lastPopped_ = elem;
            return elem;
        }
        //----< remove all elements from stack >-----------------------------

        public void clear()
        {
            stack_.Clear();
        }
        //----< index into stack contents >----------------------------------

        public E this[int i]
        {
            get
            {
                if (i < 0 || stack_.Count <= i)
                    throw new Exception("scope stack index out of range");
                return stack_[i];
            }
            set
            {
                if (i < 0 || stack_.Count <= i)
                    throw new Exception("scope stack index out of range");
                stack_[i] = value;
            }
        }
        //----< number of elements on stack property >-----------------------

        public int count
        {
            get { return stack_.Count; }
        }
        //----< get lastPopped >---------------------------------------------

        public E lastPopped()
        {
            return lastPopped_;
        }
        //----< display using element ToString() method() >------------------

        public void display()
        {
            for (int i = 0; i < count; ++i)
            {
                Console.Write("\n  {0}", stack_[i].ToString());
            }
        }
    }

    class Test
    {
        public struct Elem
        {
            public string type;
            public string name;
            public int place;
            public void make(string tp, string nm, int pl)
            {
                type = tp;
                name = nm;
                place = pl;
            }
            public override string ToString()
            {
                StringBuilder temp = new StringBuilder();
                temp.Append("{");
                temp.Append(String.Format("{0,-10}", type)).Append(" : ");
                temp.Append(String.Format("{0,-10}", name)).Append(" : ");
                temp.Append(String.Format("{0,-5}", place.ToString()));
                temp.Append("}");
                return temp.ToString();
            }
        };

        static void Main(string[] args)
        {
        }
    
#if(TEST_SCOPESTACK)
    static void Main()
    {
      Console.Write("\n  Test ScopeStack");
      Console.Write("\n =================\n");

      ScopeStack<Elem> mystack = new ScopeStack<Elem>();
      Test.Elem e;
      e.type = "namespace";
      e.name = "foobar";
      e.place = 14;
      mystack.push(e);
      e.make("class", "feebar", 21);
      mystack.push(e);
      e.make("function", "doTest", 44);
      mystack.push(e);
      e.make("control", "for", 56);
      mystack.push(e);

      mystack.display();
      Console.WriteLine();

      Elem test = mystack.lastPopped();
      Console.Write("\n  last popped:\n  {0}\n", test);

      e = mystack.pop();
      Console.Write("\n  popped:\n  {0}", e);
      e = mystack.pop();
      Console.Write("\n  popped:\n  {0}\n", e);
      Console.Write("\n  last popped:\n  {0}\n", mystack.lastPopped());

      mystack.display();

      Console.Write("\n\n");
    }
#endif
    }
}
