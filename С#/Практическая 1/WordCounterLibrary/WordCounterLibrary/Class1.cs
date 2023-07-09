using System.Collections.Generic;

namespace WordCounterLibrary
{
    public class WordCounter
    {
        private Dictionary<string, int> CountWords(string text)
        {
            char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }

            return wordCount;
        }
    }
}
