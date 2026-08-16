using LogProcessor.Abstractions;
using LogProcessor.Parsers;
using LogProcessor.Services;

namespace LogProcessor;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: LogProcessor <input.log> <output.log> <problems.txt>");
            return 1;
        }

        var inputPath = args[0];
        var outputPath = args[1];
        var problemsPath = args[2];

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Входной файл не найден: {inputPath}");
            return 2;
        }

        try
        {
            IReadOnlyList<ILogParser> parsers =
            [
                new LogFormat1Parser(),
                new LogFormat2Parser()
            ];

            ILogEntryFormatter formatter = new LogEntryFormatter();
            var processor = new LogProcessorService(parsers, formatter);

            processor.Process(inputPath, outputPath, problemsPath);

            Console.WriteLine("Обработка завершена.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обработки: {ex.Message}");
            return 3;
        }
    }
}