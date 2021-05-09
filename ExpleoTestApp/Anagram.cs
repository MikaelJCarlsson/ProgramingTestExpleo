using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpleoTestApp
{
    public static class Anagram
    {
        
        public static bool IsAnagram(string test, string original)
        {
            //För att lösa detta så vill jag jämföra två strängar med varandra, det lättaste sättet jag kom på är att sortera strängarna, 
            //Eftersom gemener och versaler sorteras olika pga deras ASCII värden så gör jag strängarna till gemener,
            //Sedan sorterar jag dem, de sorteras på varje char så jag bygger ihop dem med String.Concat för att kunna jämföra de ihopbygga strängarna
            return String.Concat(test.ToLower().OrderBy(c => c)).Trim() == String.Concat(original.ToLower().OrderBy(c => c)).Trim();
        }

        public static bool IsAnagramWithoutLinq(string test, string original)
        {
            var a = test.ToLower().ToCharArray();
            var b = original.ToLower().ToCharArray();

            Array.Sort(a);
            Array.Sort(b);

            string word1 = new string(a);
            string word2 = new string(b);

            return word1.Trim() == word2.Trim();

        }
    }
}
