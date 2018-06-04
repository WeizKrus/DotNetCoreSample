using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_core_c_sharp
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine ("Hello World!");

            foreach (var valuePair in CreateDict1 ())
            {
                Console.WriteLine ($"{{ {valuePair.Key}: {valuePair.Value} }}");
            }

            var dict1 = CreateDict1();
            var dict2 = CreateDict2();
            var diff = DictionaryDiff(dict1, dict2);

            foreach (var dif in diff) {
                Console.WriteLine($"{dif.key}, {dif.oldValue}, {dif.newValue}");
            }
        }



        static Dictionary<string, string> CreateDict1 ()
        {
            Dictionary<string, string> myDict = new Dictionary<string, string> ();

            myDict.Add ("key 1", "value 1");
            myDict.Add ("key 2", "value 2");
            myDict.Add ("key 3", "value 3");

            return myDict;
        }

        static Dictionary<string, string> CreateDict2 ()
        {
            Dictionary<string, string> myDict = new Dictionary<string, string> ();

            myDict.Add ("key 1", null);
            myDict.Add ("key 2", "value 2");
            myDict.Add ("key 3", "value 4");

            return myDict;
        }

        static List<(string key, string oldValue, string newValue)> DictionaryDiff(Dictionary<string, string> d1, Dictionary<string, string> d2)
        {
            List<(string, string, string)> diffResult = new List<(string, string, string)>();
            
            try
            {
                foreach (var valuePair in d1)
                {
                    string newValue = string.Empty;
                    string oldValue = string.Empty;
                    if (d2.TryGetValue(valuePair.Key, out newValue)) {
                        if (d1.TryGetValue(valuePair.Key, out oldValue))
                        {
                            // if (oldValue == null)
                            // {
                            //     oldValue = string.Empty;
                            // }

                            // if (newValue == null)
                            // {
                            //     newValue = string.Empty;
                            // }

                            if (oldValue != newValue)
                            {
                                diffResult.Add((valuePair.Key, oldValue, newValue));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return diffResult;
        }
    }
}