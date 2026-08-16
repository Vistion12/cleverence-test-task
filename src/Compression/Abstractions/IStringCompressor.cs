namespace Compression.Abstractions;

public interface IStringCompressor
{
    string Compress(string input);
}