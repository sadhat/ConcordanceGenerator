using System;
using ConcordanceGenerator.Extensions;

namespace ConcordanceGenerator
{
    /// <summary>
    /// To abstract code, extension methods created. Please find related extension method file for implementation details
    /// Author: Syed Ahmed
    /// Created On: Friday, December 18, 2015
    /// Total Hour in Development: 6
    /// </summary>
    class Program
    {
        const string Paragraph = "Given an arbitrary text document written in English, write a program that will generate a concordance, i.e. an alphabetical list of all word occurrences, labeled with word frequencies. Bonus: label each word with the sentence numbers in which each occurrence appeared.";
            
        static void Main(string[] args)
        {
            /* *****************************************************************************************************************************
             * Sequences of event occur on paragraph to produce a result. Please refer to extension methods file for implementation details.
             * Each method has comments for more information.
             *******************************************************************************************************************************/
            Paragraph
                .Tokenize()
                .SplitSentences()
                .PopulateDictionary()
                .FormattedWordCount()
                .Display(totalWordPerColumn: 17);
            

            Console.Write("\nPlease enter any key to exit...");
            Console.ReadKey();
        }
        
    }
}
