using FluentAssertions;
using LogProcessor.Models;
using LogProcessor.Services;
using Xunit;

namespace LogProcessor.Tests;

public class LogEntryFormatterTests
{
    private readonly LogEntryFormatter _formatter = new();

    [Fact]
    public void Format_ReturnsTabSeparatedString()
    {
        var entry = new LogEntry
        {
            Date = "10-03-2025",
            Time = "15:14:49.523",
            Level = "INFO",
            Method = "DEFAULT",
            Message = "Версия программы: '3.4.0.48729'"
        };

        var result = _formatter.Format(entry);

        result.Should().Be("10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tВерсия программы: '3.4.0.48729'");
    }
}