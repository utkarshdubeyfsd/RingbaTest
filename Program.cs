using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ringba_test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Task 1: Download the file
            //Download the file from URL using webclient
            using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/utkarshdubeyfsd/RingbaTest/master/output.txt", "output.txt");
            }

            // Open the file to read from.
            string readText = File.ReadAllText("output.txt");
            #endregion

            #region Task 2: How many of each letter are in the file
            //Count Total character in a Text file.
            var TotalChar = readText.Length;
            Console.WriteLine("# Count of Total Character: " + TotalChar);
            Console.WriteLine();

            //Count the occurrance of each character
            Console.WriteLine("# Count the Occurrance of each Character:");
            Program p = new Program();
            Dictionary<char, int> dict = new Dictionary<char, int>();
            dict = p.getCount(readText);
            foreach (KeyValuePair<char, int> pair in dict)
            {
                Console.WriteLine(pair.Key.ToString() + "  =  " + pair.Value.ToString());
            }
            #endregion

            #region Task 3: The most common word and the number of times it has been seen.
            Console.WriteLine();

            StringBuilder builder = new StringBuilder();
            foreach (char c in readText)
            {
                //adding 'space' from where capital letter going to start
                if (Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
                builder.Append(c);
            }
            string TextwithSpace = builder.ToString();

            var Value = TextwithSpace.Split(' ');  // Split the string using 'Space'

            Dictionary<string, int> RepeatedWordCount = new Dictionary<string, int>();
            for (int i = 0; i < Value.Length; i++) //loop the splited string  
            {
                if (RepeatedWordCount.ContainsKey(Value[i])) // Check if word already exist in dictionary then update the count  
                {
                    int value = RepeatedWordCount[Value[i]];
                    RepeatedWordCount[Value[i]] = value + 1;
                }
                else
                {
                    RepeatedWordCount.Add(Value[i], 1);  // if a string is repeated and not added in dictionary then here we are adding it
                }
            }

            string mostCommonWord = ""; int occurrences = 0;
            foreach (var pair in RepeatedWordCount)
            {
                if (pair.Value > occurrences)
                {
                    occurrences = pair.Value;
                    mostCommonWord = pair.Key;
                }
            }

            Console.WriteLine("# The most common word: " + mostCommonWord + " and the number of times it has been seen: " + occurrences);
            #endregion

            #region Task 4: How many letters are capitalized in the file
            Console.WriteLine();
            Console.WriteLine("Count of Capitalized letters in the file: " + Value.Length);
            #endregion

            #region Task 5: The most common 2 character prefix and the number of occurrences in the text file

            //retrieve words whose length is more than 2 and then only get it's first 2 character
            List<string> wordCollection = new List<string>();
            foreach (string val in Value)
            {
                if (val.Length > 2)
                {
                    wordCollection.Add(val.Substring(0, 2));
                }
            }

            var prefix = p.getPrefixCount(wordCollection);

            string mostCommonPrefix = "";
            foreach (var pair in prefix)
            {
                if (pair.Value > occurrences)
                {
                    occurrences = pair.Value;
                    mostCommonPrefix = pair.Key;
                }
            }

            Console.WriteLine();
            Console.WriteLine("# The most common prefix: " + mostCommonPrefix + " and the number of times it has been seen: " + occurrences);

            #endregion
        }

        /// <summary>
        /// find the occurrences of each letter
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Dictionary<char, int> getCount(string name)
        {
            return name.GroupBy(x => x).ToDictionary(gr => gr.Key, gr => gr.Count());
        }

        /// <summary>
        /// find the occurrences of each prefix
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Dictionary<string, int> getPrefixCount(List<string> values)
        {
            var result = new Dictionary<string, int>();
            foreach (string value in values)
            {
                if (result.TryGetValue(value, out int count))
                {
                    // Increase existing value.
                    result[value] = count + 1;
                }
                else
                {
                    // New value, set to 1.
                    result.Add(value, 1);
                }
            }
            // Return the dictionary.
            return result;
        }
    }
}
