namespace Compression.Abstractions;

public interface IStringDecompressor
{
    /// <summary>
    /// Восстанавливает исходную строку из сжатого формата.
    /// </summary>
    string Decompress(string input);
}