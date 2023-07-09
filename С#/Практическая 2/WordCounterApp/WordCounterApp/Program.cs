using System.Diagnostics;
using System.Reflection;
using WordCounterLibrary;

namespace WordCounterApp
{
    class Program
    {
        static void Main()
        {
            string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");
            string outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt");
            string timeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "time.txt");

            try
            {
                string content = File.ReadAllText(inputFile);

                Assembly assembly = Assembly.LoadFrom("WordCounterLibrary.dll");
                Type wordCounterType = assembly.GetType("WordCounterLibrary.WordCounter");
                object wordCounterInstance = Activator.CreateInstance(wordCounterType);
                MethodInfo countWordsMethodPrivate = wordCounterType.GetMethod("CountWords", BindingFlags.Instance | BindingFlags.NonPublic);

                // Измерение времени приватного метода
                Stopwatch privateMethodStopwatch = Stopwatch.StartNew();
                Dictionary<string, int> privateMethodResult = (Dictionary<string, int>)countWordsMethodPrivate.Invoke(wordCounterInstance, new object[] { content });
                privateMethodStopwatch.Stop();
                TimeSpan privateMethodTime = privateMethodStopwatch.Elapsed;

                WordCounter wordCounter = new WordCounter();

                // Измерение времени публичного метода
                Stopwatch publicMethodStopwatch = Stopwatch.StartNew();
                Dictionary<string, int> publicMethodResult = wordCounter.CountWordsMultiThreaded(content);
                publicMethodStopwatch.Stop();
                TimeSpan publicMethodTime = publicMethodStopwatch.Elapsed;


                // Запись результатов в файл output.txt (отсортировано по убыванию)
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    writer.WriteLine("Результаты подсчета слов:");
                    WriteWordCountDescending(writer, publicMethodResult);
                }

                // Запись результатов времени выполнения в файл time.txt
                using (StreamWriter writer = new StreamWriter(timeFile))
                {
                    writer.WriteLine("Время выполнения приватного метода: " + privateMethodTime);
                    writer.WriteLine("Время выполнения публичного метода: " + publicMethodTime);
                }

                Console.WriteLine("Результаты записаны в файлы.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteWordCountDescending(StreamWriter writer, Dictionary<string, int> wordCount)
        {
            foreach (var pair in wordCount.OrderByDescending(w => w.Value))
            {
                writer.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
