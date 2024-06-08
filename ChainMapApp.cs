using System;
using System.Collections.Generic;
using ChainMapLib;

namespace ChainMapApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict1 = new Dictionary<string, string> { { "a", "1" }, { "b", "2" }, { "c", "3" } };
            var dict2 = new Dictionary<string, string> { { "b", "22" }, { "c", "33" }, { "d", "44" } };
            var dict3 = new Dictionary<string, string> { { "c", "333" }, { "d", "444" }, { "e", "555" } };
            var chainMap = new ChainMap<string, string>(dict1, dict2, dict3);

            Console.WriteLine(chainMap["a"]); 
            Console.WriteLine(chainMap["b"]);
            Console.WriteLine(chainMap["c"]); 
            Console.WriteLine(chainMap["d"]);
            Console.WriteLine(chainMap["e"]);

            chainMap["a"] = "11";
            chainMap["b"] = "22";
            chainMap["c"] = "33";
            chainMap["d"] = "44";
            chainMap["e"] = "55";

            Console.WriteLine(chainMap["a"]);
            Console.WriteLine(chainMap["b"]);
            Console.WriteLine(chainMap["c"]);
            Console.WriteLine(chainMap["d"]);
            Console.WriteLine(chainMap["e"]);
            chainMap.Remove("a");
            Console.WriteLine(chainMap["a"]);
            chainMap.Add("f", "66");
            Console.WriteLine(chainMap["f"]);
            chainMap.Remove("f");

            Console.WriteLine(chainMap.ContainsKey("f"));
            chainMap.AddDictionary(new Dictionary<string, string> { { "g", "77" } }, 0);
            Console.WriteLine(chainMap["g"]);
            chainMap.RemoveDictionary(0);
            Console.WriteLine(chainMap.ContainsKey("g"));
            chainMap.ClearDictionaries();
            Console.WriteLine(chainMap.Count);
        }
    }
}
