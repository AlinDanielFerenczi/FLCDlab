using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace labproject
{
    class Scanner
    {
        static List<Tuple<int, string>> PIF = new List<Tuple<int, string>>();
        static SymbolTable ST = new SymbolTable(200);

        static void Main(string[] args)
        {
            Scanning("test.txt");
            Console.ReadKey();
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

        public static void Scanning(string filePath)
        {

            var textTokenized = new List<KeyValuePair<int, string>>();

            using (var r = new StreamReader(filePath))
            {
                for(var lineNumber = 1; r.Peek() != -1; lineNumber++)
                {
                    textTokenized.Add(new KeyValuePair<int, string>(lineNumber, r.ReadLine()));
                }
            }

            FilterTokens(textTokenized);

            PrintContent();
        }

        public static Dictionary<string, int> ReadTokens()
        {
            using var r = new StreamReader("token.json");
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        }

        public static void FilterTokens(List<KeyValuePair<int,string>> textTokenized)
        {
            var tokens = ReadTokens();

            foreach (var text in textTokenized)
            {
                var copy = (IEnumerable<char>)text.Value.ToCharArray();

                while (copy.Count() > 0)
                {
                    var token = string.Empty;

                    while(
                        !(
                            tokens.ContainsKey(token) || 
                            copy.Count() == 0 || 
                            copy.First() == ' ' || 
                            tokens.ContainsKey(copy.First().ToString())
                        ) || 
                        (
                            copy.Count() > 0 &&
                            tokens.ContainsKey(token + copy.First()
                        ))
                    )
                    {
                        token += copy.First();
                        copy = copy.Skip(1);
                    }

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        if (!tokens.ContainsKey(token) && !DetectSymbol(token))
                        {
                            Console.WriteLine("Errot at line {0} for token {1}", text.Key, token);
                            throw new Exception("Lexical error!");
                        }
                        PIF.Add(new Tuple<int, string>(DetectSymbol(token) ? ST.Add(token) : 0, token));
                    }
                    if(copy.Count() == 0)
                        continue;

                    var first = copy.First().ToString();

                    copy = copy.Skip(1);

                    if (string.IsNullOrWhiteSpace(first))
                        continue;

                    if(tokens.ContainsKey(first))
                        PIF.Add(new Tuple<int, string>(0, first));
                }
            }
        }

        public static bool DetectSymbol(string token)
        {
            return IsConstant(token) || IsIdentifier(token);
        }

        public static bool IsIdentifier(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z]([a-zA-Z]|\d)*$");
        }

        public static bool IsConstant(string text)
        {
            return Regex.IsMatch(text, @"^((\""[^\""]*\"")|([1-9]\d{0,})|0)$");
        }

        public static void GenPIF(int index, string token)
        {
            PIF.Add(new Tuple<int, string>(index, token));
        }

        public static void PrintContent()
        {
            using (var r = new StreamWriter("ST.json"))
            {
                r.Write(ST.ToString());
            }
            using (var r = new StreamWriter("PIF.json"))
            {
                r.Write(JsonConvert.SerializeObject(PIF));
            }
        }
    }
} 