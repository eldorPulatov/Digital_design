using System.Reflection;

namespace WordCounterApp
{
    class Program
    {
        static void Main()
        {
            ReflectionExample();

        }

        private static void ReflectionExample()
        {
            string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");
            string outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt");

            try
            {
                string content = File.ReadAllText(inputFile);

                Assembly assembly = Assembly.LoadFrom("WordCounterLibrary.dll");
                Type wordCounterType = assembly.GetType("WordCounterLibrary.WordCounter");
                object wordCounterInstance = Activator.CreateInstance(wordCounterType);

                MethodInfo countWordsMethod = wordCounterType.GetMethod("CountWords", BindingFlags.Instance | BindingFlags.NonPublic);
                Dictionary<string, int> wordCount = (Dictionary<string, int>)countWordsMethod.Invoke(wordCounterInstance, new object[] { content });

                var sortedWords = wordCount.OrderByDescending(w => w.Value);

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
