using LogProcessor.Abstractions;
using LogProcessor.Models;

namespace LogProcessor.Services;

public class LogProcessorService
{
    private readonly IReadOnlyList<ILogParser> _parsers;
    private readonly ILogEntryFormatter _formatter;

    public LogProcessorService(IReadOnlyList<ILogParser> parsers, ILogEntryFormatter formatter)
    {
        _parsers = parsers;
        _formatter = formatter;
    }

    public void Process(string inputPath, string outputPath, string problemsPath)
    {
        using var output = new FileWriter(outputPath);
        using var problems = new FileWriter(problemsPath);

        foreach (var line in File.ReadLines(inputPath))
        {
            LogEntry? entry = null;

            foreach (var parser in _parsers)
            {
                if (parser.TryParse(line, out entry))
                {
                    break;
                }
            }

            if (entry is not null)
            {
                output.WriteLine(_formatter.Format(entry));
            }
            else
            {
                problems.WriteLine(line);
            }
        }
    }
}