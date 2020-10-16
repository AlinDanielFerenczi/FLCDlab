using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labproject
{
    class Program
    {
        static void Main(string[] args)
        {
            var hash = new SymbolTable(20);

            hash.Add("item 1");
            if (hash.Exists("item 1"))
                Console.WriteLine("Added");
            hash.Add("item 2");
            var key = hash.Add("item 2");
            Console.WriteLine(key);
            hash.Add("sadsadsadsad");

            string one = hash.Find("item 1");
            string two = hash.Find("item 2");
            string three = hash.Find("item 3");
            Console.WriteLine(one);
            Console.WriteLine(two);
            Console.WriteLine(three);
            hash.Remove("item 2");
            Console.ReadKey();
        }
    }
}
