using System.Collections.Concurrent;

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

        public Dictionary<string, int> CountWordsMultiThreaded(string text)
        {
            text = text.Replace("\"", "");
            // Создаем параллельный словарь для безопасной работы с счетчиком слов из нескольких потоков
            ConcurrentDictionary<string, int> wordCount = new ConcurrentDictionary<string, int>();
 
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Parallel.ForEach(words, word =>
            {
                wordCount.AddOrUpdate(word, 1, (key, count) => count + 1);
            });

            // Преобразуем параллельный словарь в наш обычный словарь
            Dictionary<string, int> result = new Dictionary<string, int>(wordCount);

            return result;
        }
    }
}
