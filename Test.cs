using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_core_c_sharp
{
    public class Test
    {
        public new () {
            
        }

        static Dictionary<string, string> CreateDict3 ()
        {
            Dictionary<string, string> myDict = new Dictionary<string, string> ();

            myDict.Add ("key 1", "value 1");
            myDict.Add ("key 2", "value 2");
            myDict.Add ("key 3", "value 3");

            return myDict;
        }
    }
}