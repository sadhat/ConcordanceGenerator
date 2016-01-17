using System;
using System.Collections.Generic;
namespace ConcordanceGenerator.Extensions
{
    /// <summary>
    /// Extension methods related to string manipulation
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Get a tokenized string. Modifies paragraph and add special character to mark as a line terminator.
        /// </summary>
        /// <param name="paragraph">Paragraph to be tokenized</param>
        /// <returns>String after tokenized</returns>
        public static string Tokenize(this string paragraph)
        {
            var array = paragraph.ToCharArray();
            if (IsPunctuation(array[array.Length - 1]))
            {
                array[array.Length - 1] = '`';
            }
            for (var i = 0; i < array.Length; i++)
            {
                if (!IsPunctuation(array[i])) continue;

                if (i == array.Length - 2)
                {
                    array[i] = '`';
                }
                if (i >= array.Length - 1) continue;

                if (IsLineTerminator(array[i - 1], Char.IsWhiteSpace(array[i + 1]) ? array[i + 2] : array[i + 1]))
                {
                    array[i] = '`';
                }

            }
            return new string(array);
        }

        /// <summary>
        /// Remove special characters at the end and add necessary whitespace for format correctly
        /// </summary>
        /// <param name="word">Word to be formatted</param>
        /// <param name="longestWordLength">Longest length of the word to pad whitespace</param>
        /// <returns>Modified string after removing special character and adding whitespace</returns>
        public static string RemoveSpecialCharacterAndAddWhitespace(this string word, int longestWordLength)
        {
            var currentWordLength = word.Length;
            var lastCharacter = word[currentWordLength - 1];
            var modifiedWord = word;
            switch (lastCharacter)
            {
                case ',':
                case ':':
                    modifiedWord = word.Substring(0, currentWordLength - 1);
                    break;
            }
            currentWordLength = modifiedWord.Length;
            modifiedWord = modifiedWord.PadRight(longestWordLength - currentWordLength);
            return modifiedWord;
        }

        /// <summary>
        /// Split sentences from the paragraph
        /// </summary>
        /// <param name="paragraph">Paragraph to be splitted</param>
        /// <returns>Array of lines representing sentences</returns>
        public static IEnumerable<string> SplitSentences(this string paragraph)
        {
            return paragraph.Split("`".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Character is a punctuation
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <returns>Whether punctuation</returns>
        static bool IsPunctuation(char c)
        {
            return Char.IsPunctuation(c); //c == '.' || c == '?' || c == '!';
        }

        /// <summary>
        /// Check current character is line terminator
        /// </summary>
        /// <param name="previous">Previous character of current character</param>
        /// <param name="next">Next character of current character</param>
        /// <returns></returns>
        static bool IsLineTerminator(char previous, char next)
        {
            //Very simple rule.
            //  If previous character is lower case and next character is upper case of a punctuation, we will consider as line terminator
            return Char.IsLower(previous) && Char.IsUpper(next);
        }
    }
}