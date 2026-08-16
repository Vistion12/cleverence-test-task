using Compression.Services;
using FluentAssertions;

namespace Compression.Tests;

public class CompressionRoundTripTests
{
    private readonly StringCompressor _compressor = new();
    private readonly StringDecompressor _decompressor = new();

    [Theory]
    [InlineData("aaabbcccdde")]
    [InlineData("a")]
    [InlineData("abcdef")]
    [InlineData("aabbaaaa")]
    [InlineData("aaaaaaaaaaab")]
    public void CompressThenDecompress_ReturnsOriginalString(string input)
    {
        var compressed = _compressor.Compress(input);
        var decompressed = _decompressor.Decompress(compressed);

        decompressed.Should().Be(input);
    }
}