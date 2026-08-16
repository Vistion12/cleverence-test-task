using FluentAssertions;
using LogProcessor.Parsers;
using Xunit;

namespace LogProcessor.Tests;

public class LogFormat2ParserTests
{
    private readonly LogFormat2Parser _parser = new();

    [Fact]
    public void TryParse_ValidLine_ReturnsTrue()
    {
        const string line = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        var result = _parser.TryParse(line, out var entry);

        result.Should().BeTrue();
        entry.Should().NotBeNull();
        entry!.Date.Should().Be("10-03-2025");
        entry.Time.Should().Be("15:14:51.5882");
        entry.Level.Should().Be("INFO");
        entry.Method.Should().Be("MobileComputer.GetDeviceId");
        entry.Message.Should().Be("Код устройства: '@MINDEO-M40-D-410244015546'");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("невалидная строка")]
    [InlineData("2025-03-10 15:14:51.5882| UNKNOWN|11|Method| message")]
    public void TryParse_InvalidLine_ReturnsFalse(string line)
    {
        var result = _parser.TryParse(line, out var entry);

        result.Should().BeFalse();
        entry.Should().BeNull();
    }
}