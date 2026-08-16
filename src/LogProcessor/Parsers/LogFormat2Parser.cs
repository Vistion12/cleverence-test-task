using System.Globalization;
using LogProcessor.Abstractions;
using LogProcessor.Models;

namespace LogProcessor.Parsers;

public class LogFormat2Parser : ILogParser
{
    private const string SourceDateFormat = "yyyy-MM-dd";

    public bool TryParse(string line, out LogEntry? entry)
    {
        entry = null;

        if (string.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        // Формат 2: 2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'
        var parts = line.Split('|');

        if (parts.Length < 5)
        {
            return false;
        }

        var dateTime = parts[0].Trim().Split(' ', 2);

        if (dateTime.Length < 2)
        {
            return false;
        }

        var date = dateTime[0];
        var time = dateTime[1];
        var level = parts[1].Trim();
        var method = parts[3].Trim();
        var message = parts[4].Trim();

        if (!IsValidDate(date) || !IsValidTime(time) || !IsValidLevel(level))
        {
            return false;
        }

        entry = new LogEntry
        {
            Date = ConvertDate(date, SourceDateFormat),
            Time = time,
            Level = NormalizeLevel(level),
            Method = string.IsNullOrWhiteSpace(method) ? "DEFAULT" : method,
            Message = message
        };

        return true;
    }

    private static bool IsValidDate(string date)
    {
        return DateTime.TryParseExact(date, SourceDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    private static bool IsValidTime(string time)
    {
        return DateTime.TryParseExact(time, "HH:mm:ss.ffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    private static bool IsValidLevel(string level)
    {
        return level is "INFO" or "WARN" or "ERROR" or "DEBUG";
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

    private static string ConvertDate(string date, string sourceFormat)
    {
        var parsed = DateTime.ParseExact(date, sourceFormat, CultureInfo.InvariantCulture);
        return parsed.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
    }
}