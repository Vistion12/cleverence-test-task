namespace Compression.Abstractions;

public interface IStringCompressor
{
    /// <summary>
    /// Сжимает строку, заменяя повторяющиеся символы на формат "символ+количество".
    /// </summary>
    string Compress(string input);
}