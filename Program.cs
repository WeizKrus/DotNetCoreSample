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

            // foreach (var valuePair in DictionaryFactory.CreateDict1 ())
            // {
            //     Console.WriteLine ($"{{ {valuePair.Key}: {valuePair.Value} }}");
            // }

            var dict1 = DictionaryFactory.CreateDict1();
            var dict2 = DictionaryFactory.CreateDict2();
            var diff = DictionaryDiff(dict1, dict2);
            string originalValue = string.Empty;
            string currentValue = string.Empty;

            foreach (var dif in diff) {
                originalValue = string.IsNullOrWhiteSpace(dif.oldValue) ? "[null]" : dif.oldValue;
                currentValue = string.IsNullOrWhiteSpace(dif.newValue) ? "[null]" : dif.newValue;

                Console.WriteLine($"{dif.key}, {originalValue}, {currentValue}");
            }
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