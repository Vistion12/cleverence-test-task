using LogProcessor.Abstractions;
using LogProcessor.Models;

namespace LogProcessor.Services;

public class LogEntryFormatter : ILogEntryFormatter
{
    public string Format(LogEntry entry)
    {
        return string.Join('\t',
            entry.Date,
            entry.Time,
            entry.Level,
            entry.Method,
            entry.Message);
    }
}