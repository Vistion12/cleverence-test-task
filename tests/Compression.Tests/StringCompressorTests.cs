using Compression.Services;
using FluentAssertions;

namespace Compression.Tests;

public class StringCompressorTests
{
    private readonly StringCompressor _compressor = new();

    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("abc", "abc")]
    [InlineData("aaabbcccdde", "a3b2c3d2e")]
    [InlineData("aaaaaaaaaa", "a10")]
    [InlineData("aaaaaaaaaaab", "a11b")]
    [InlineData("aabbaaaa", "a2b2a4")]
    public void Compress_ReturnsExpectedResult(string input, string expected)
    {
        var result = _compressor.Compress(input);

        result.Should().Be(expected);
    }

    [Fact]
    public void Compress_NullInput_ThrowsArgumentNullException()
    {
        var action = () => _compressor.Compress(null!);

        action.Should().Throw<ArgumentNullException>();
    }
}