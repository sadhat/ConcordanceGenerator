using System.Collections.Generic;
using System.Globalization;

namespace ConcordanceGenerator.Extensions
{
    /// <summary>
    /// Extension methods related to integer manipulation
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Generate numbering for display. It will create list of alphabetic numbers based on total items. 
        /// </summary>
        /// <param name="totalItems">Total items we have in a list</param>
        /// <param name="maxItemInBucket">Total items need to be in one row. Default will be 26 since we have 26 alphabets</param>
        /// <returns>List of number sequence</returns>
        public static IEnumerable<string> GenerateNumberingFor(this int totalItems, int maxItemInBucket = 26)
        {

            var totalBucket = totalItems / maxItemInBucket;
            if (totalItems % maxItemInBucket > 0)
            {
                //We have left over. So create one for left over items
                totalBucket++;
            }

            //Total buckets we need for items
            var buckets = new List<string>[totalBucket];
            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<string>();
            }

            var currentBucketIndex = 0;
            var charIndex = 0;
            for (var i = 0; i < totalItems; i++)
            {
                var currentBucket = buckets[currentBucketIndex];
                if (currentBucket.Count >= maxItemInBucket)
                {
                    currentBucket = buckets[++currentBucketIndex];
                    charIndex = 0;//reset
                }
                currentBucket.Add(((char)(97 + (charIndex++))).ToString(CultureInfo.InvariantCulture));
            }
            for (var i = 0; i < buckets.Length; i++)
            {
                foreach (var item in buckets[i])
                {
                    var value = item;
                    for (var j = 0; j < i; j++)
                    {
                        //Repeat itself, For example, we are at 2nd bucket it will product "aa" for first item in that bucket
                        value += item;
                    }
                    yield return value;
                }
            }
        }
    }
}