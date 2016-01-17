using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcordanceGenerator.Extensions
{
    /// <summary>
    /// Extension method related to list manipulation
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Populate dictionary. Dictionary key is word and value contains word count and line numbers where its found. 
        /// Example: key = "myword", value = {"2", "1,1"}. First value is count and second value is sentence indicator
        /// </summary>
        /// <param name="sentenses">Sentences to be counted for</param>
        /// <returns>Sorted dictionary containing word, word count and word occurrence in sentences</returns>
        public static SortedDictionary<string, Tuple<int, List<int>>> PopulateDictionary(this IEnumerable<string> sentenses)
        {
            var dictionary = new SortedDictionary<string, Tuple<int, List<int>>>();
            var list = sentenses.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var splitted = list[i].Split(string.Empty.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in splitted)
                {
                    if (!dictionary.ContainsKey(word))
                    {
                        dictionary.Add(word, new Tuple<int, List<int>>(1, new List<int> { i + 1 }));
                    }
                    else
                    {
                        var item = dictionary[word];
                        item.Item2.Add(i + 1);
                        var newItem = new Tuple<int, List<int>>(item.Item1 + 1, item.Item2);
                        dictionary[word] = newItem;
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Create vertical list
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="capacity">Capacity per column</param>
        /// <returns></returns>
        public static List<string>[] CreateVerticalList(this IEnumerable<string> lines, int capacity)
        {
            var items = lines.ToList();
            var length = items.Count;
            if (capacity > length)
            {
                capacity = length;
            }
            var totalColumns = length / capacity;
            if (length % capacity > 0)
            {
                totalColumns++;
            }
            var buckets = new List<string>[totalColumns];

            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<string>();
            }

            var currentBucketIndex = 0;
            foreach (var item in items)
            {
                var currentBucket = buckets[currentBucketIndex];
                if (currentBucket.Count >= capacity)
                {
                    currentBucketIndex++;
                    currentBucket = buckets[currentBucketIndex];
                }
                currentBucket.Add(item);
            }

            return buckets;

        }

        /// <summary>
        /// Display vertical list
        /// </summary>
        /// <param name="buckets">Buckets containing sentences</param>
        /// <param name="totalWordPerColumn">Total word per column</param>
        public static void DisplayVerticalList(this List<string>[] buckets, int totalWordPerColumn)
        {
            for (int i = 0; i < totalWordPerColumn; i++)
            {
                for (int j = 0; j < buckets.Length; j++)
                {
                    if (buckets[j].Count > i)
                        Console.Write(buckets[j][i] + "\t");
                }

                Console.WriteLine();
            }
        }
        
    }
}