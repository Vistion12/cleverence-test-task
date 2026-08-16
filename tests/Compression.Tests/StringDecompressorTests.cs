using Compression.Services;
using FluentAssertions;

namespace Compression.Tests;

public class StringDecompressorTests
{
    private readonly StringDecompressor _decompressor = new();

    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("abc", "abc")]
    [InlineData("a3b2c3d2e", "aaabbcccdde")]
    [InlineData("a10", "aaaaaaaaaa")]
    [InlineData("a11b", "aaaaaaaaaaab")]
    [InlineData("a2b2a4", "aabbaaaa")]
    public void Decompress_ReturnsExpectedResult(string input, string expected)
    {
        var result = _decompressor.Decompress(input);

        result.Should().Be(expected);
    }

    [Fact]
    public void Decompress_NullInput_ThrowsArgumentNullException()
    {
        var action = () => _decompressor.Decompress(null!);

        action.Should().Throw<ArgumentNullException>();
    }
}