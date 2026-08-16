using FluentAssertions;
using LogProcessor.Abstractions;
using LogProcessor.Parsers;
using LogProcessor.Services;
using Xunit;

namespace LogProcessor.Tests;

public class LogProcessorServiceTests
{
    [Fact]
    public void Process_ValidAndInvalidLines_WritesToCorrectFiles()
    {
        var inputPath = Path.GetTempFileName();
        var outputPath = Path.GetTempFileName();
        var problemsPath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(inputPath,
                "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'\n" +
                "невалидная строка\n" +
                "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'");

            IReadOnlyList<ILogParser> parsers =
            [
                new LogFormat1Parser(),
                new LogFormat2Parser()
            ];

            var service = new LogProcessorService(parsers, new LogEntryFormatter());

            service.Process(inputPath, outputPath, problemsPath);

            var outputLines = File.ReadAllLines(outputPath);
            var problemLines = File.ReadAllLines(problemsPath);

            outputLines.Should().HaveCount(2);
            problemLines.Should().HaveCount(1);
            problemLines[0].Should().Be("невалидная строка");
        }
        finally
        {
            File.Delete(inputPath);
            File.Delete(outputPath);
            File.Delete(problemsPath);
        }
    }
}