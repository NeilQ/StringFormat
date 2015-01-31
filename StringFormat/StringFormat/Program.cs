using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StringFormat.Format("{0},{1},{2}", "a", "b", "c"));
            Console.WriteLine(StringFormat.Format("{0},{1},{2}", "a", "b"));
            Console.WriteLine(StringFormat.Format("{0},{1},{2}", "a", "b", "c", "d"));
            Console.WriteLine(StringFormat.Format("{{0}},{1},{2}", "a", "b", "c"));
            Console.ReadKey();
        }
    }
}
