namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");
            string outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt");

            try
            {
                string content = File.ReadAllText(inputFile);

                // Разделительные символы для токенизации текста
                char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t' };

                // Токенизация текста на слова
                string[] words = content.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                // Подсчет количества употреблений каждого слова
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

                // Сортировка слов по количеству употреблений в порядке убывания
                var sortedWords = wordCount.OrderByDescending(w => w.Value);

                // Запись результата в выходной файл
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (var word in sortedWords)
                    {
                        writer.WriteLine($"{word.Key}\t{word.Value}");
                    }
                }

                Console.WriteLine("Результат записан в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
