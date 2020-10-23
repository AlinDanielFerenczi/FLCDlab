using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labproject
{
    class Program
    {
        static void Main(string[] args)
        {
            Scanning("test.txt");
        }

        static void TestST()
        {
            var hash = new SymbolTable(20);

            hash.Add("item 1");
            if (hash.Exists("item 1"))
                Console.WriteLine("Added");
            hash.Add("item 2");
            var key = hash.Add("item 2");
            Console.WriteLine(key);
            hash.Add("abc");
            hash.Add("bca");

            var one = hash.Exists("item 1");
            var two = hash.Exists("item 2");
            var three = hash.Exists("item 3");
            Console.WriteLine(one);
            Console.WriteLine(two);
            Console.WriteLine(three);
            hash.Remove("item 2");
            Console.ReadKey();
        }

        static void Scanning(string filePath)
        {
            var tokens = ReadTokens();
            var PIF = new Dictionary<int, string>();
            var ST = new SymbolTable(200);

            string[] textTokenized;

            using (var r = new StreamReader(filePath))
            {
                textTokenized = r.ReadToEnd().Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach(var text in textTokenized)
            {
                string copy = text;
                foreach(var token in tokens)
                {
                    if(text.Contains(token.Key))
                    {
                        copy = copy.Replace(token.Key, " ");
                    }
                }
                Console.WriteLine(copy);
            }

            Console.ReadKey();
        }

        static Dictionary<string, int> ReadTokens()
        {
            using (var r = new StreamReader("token.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            }
        }
    }
} 