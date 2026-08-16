using System.Text;
using Compression.Abstractions;

namespace Compression.Services;

public class StringDecompressor : IStringDecompressor
{
    public string Decompress(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Length == 0)
        {
            return string.Empty;
        }

        var result = new StringBuilder();
        var index = 0;

        while (index < input.Length)
        {
            var symbol = input[index];
            index++;

            var countStart = index;

            while (index < input.Length && char.IsDigit(input[index]))
            {
                index++;
            }

            var count = countStart == index
                ? 1
                : int.Parse(input.AsSpan(countStart, index - countStart));

            result.Append(symbol, count);
        }

        return result.ToString();
    }
}