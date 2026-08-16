using LogProcessor.Models;

namespace LogProcessor.Abstractions;

public interface ILogEntryFormatter
{
    string Format(LogEntry entry);
}