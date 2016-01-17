using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConcordanceGenerator.Extensions
{
    /// <summary>
    /// Extension methods related to dictionary manipulation
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Provide formatted word count and sentence indicator. Once again key is word and value is formatted text indicating word count and occurrence in sentences
        /// Example: key = "myword", value = "{3:1,1,2}"
        /// </summary>
        /// <param name="sortedList">Sorted list</param>
        /// <returns>List of key value pair containing formatter word count</returns>
        public static IEnumerable<KeyValuePair<string, string>> FormattedWordCount(this SortedDictionary<string, Tuple<int, List<int>>> sortedList)
        {
            foreach (var word in sortedList)
            {
                var list = word.Value.Item2.Select(c => c.ToString(CultureInfo.InvariantCulture));
                yield return new KeyValuePair<string, string>(word.Key.ToLower(), string.Format(@"{{{0}:{1}}}", word.Value.Item1, string.Join(",", list)));
            }
        }

        /// <summary>
        /// High order function to display formatted output
        /// </summary>
        /// <param name="dictionary">Dictionary containing word count to display</param>
        /// <param name="totalWordPerColumn">Total word display per column</param>
        public static void Display(this IEnumerable<KeyValuePair<string, string>> dictionary, int totalWordPerColumn = 20)
        {
            var sortedDictionary = dictionary.ToList();
            var numbers = sortedDictionary.Count.GenerateNumberingFor().ToList();
            var lines = new List<string>();
            for (var i = 0; i < sortedDictionary.Count(); i++)
            {
                var kvp = sortedDictionary[i];
                string value = string.Format("{0}. {1}\t\t{2}", numbers[i],
                    kvp.Key.RemoveSpecialCharacterAndAddWhitespace(sortedDictionary.LogestKeyLength()),
                    kvp.Value);
                lines.Add(value);
                //action(value);
            }

            lines.CreateVerticalList(totalWordPerColumn).DisplayVerticalList(totalWordPerColumn);
        }

        /// <summary>
        /// Find longest word length in a given dictionary
        /// </summary>
        /// <param name="dictionary">Key value list</param>
        /// <returns>Longest length of word in the dictionary</returns>
        private static int LogestKeyLength(this IEnumerable<KeyValuePair<string, string>> dictionary)
        {
            return dictionary.OrderByDescending(s => s.Key.Length).First().Key.Length;

        }
    }


}