using System.Text;
using Compression.Abstractions;

namespace Compression.Services;

public class StringCompressor : IStringCompressor
{
    public string Compress(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Length == 0)
        {
            return string.Empty;
        }

        var result = new StringBuilder();
        var currentChar = input[0];
        var count = 1;

        for (var i = 1; i < input.Length; i++)
        {
            if (input[i] == currentChar)
            {
                count++;
            }
            else
            {
                AppendGroup(result, currentChar, count);
                currentChar = input[i];
                count = 1;
            }
        }

        AppendGroup(result, currentChar, count);

        return result.ToString();
    }

    private static void AppendGroup(StringBuilder builder, char symbol, int count)
    {
        builder.Append(symbol);

        if (count > 1)
        {
            builder.Append(count);
        }
    }
}