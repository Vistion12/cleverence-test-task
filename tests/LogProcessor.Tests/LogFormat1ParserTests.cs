using FluentAssertions;
using LogProcessor.Parsers;

namespace LogProcessor.Tests;

public class LogFormat1ParserTests
{
    private readonly LogFormat1Parser _parser = new();

    [Fact]
    public void TryParse_ValidLine_ReturnsTrue()
    {
        const string line = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";

        var result = _parser.TryParse(line, out var entry);

        result.Should().BeTrue();
        entry.Should().NotBeNull();
        entry!.Date.Should().Be("10-03-2025");
        entry.Time.Should().Be("15:14:49.523");
        entry.Level.Should().Be("INFO");
        entry.Method.Should().Be("DEFAULT");
        entry.Message.Should().Be("Версия программы: '3.4.0.48729'");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("невалидная строка")]
    [InlineData("10.03.2025 15:14:49.523 UNKNOWN message")]
    public void TryParse_InvalidLine_ReturnsFalse(string line)
    {
        var result = _parser.TryParse(line, out var entry);

        result.Should().BeFalse();
        entry.Should().BeNull();
    }
}