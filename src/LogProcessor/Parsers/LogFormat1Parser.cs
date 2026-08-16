using LogProcessor.Abstractions;
using LogProcessor.Models;

namespace LogProcessor.Parsers;

public class LogFormat1Parser : ILogParser
{
    public bool TryParse(string line, out LogEntry? entry)
    {
        entry = null;

        if (string.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        // Формат 1: 10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'
        var parts = line.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 4)
        {
            return false;
        }

        var date = parts[0];
        var time = parts[1];
        var level = parts[2];
        var message = parts[3];

        if (!IsValidDate(date) || !IsValidTime(time) || !IsValidLevel(level))
        {
            return false;
        }

        entry = new LogEntry
        {
            Date = date,
            Time = time,
            Level = NormalizeLevel(level),
            Method = "DEFAULT",
            Message = message
        };

        return true;
    }

    private static bool IsValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _);
    }

    private static bool IsValidTime(string time)
    {
        return DateTime.TryParseExact(time, "HH:mm:ss.fff", null, System.Globalization.DateTimeStyles.None, out _);
    }

    private static bool IsValidLevel(string level)
    {
        return level is "INFORMATION" or "WARNING" or "ERROR" or "DEBUG";
    }

    private static string NormalizeLevel(string level)
    {
        return level switch
        {
            "INFORMATION" => "INFO",
            "WARNING" => "WARN",
            _ => level
        };
    }
}