using LogProcessor.Models;

namespace LogProcessor.Abstractions;

/// <summary>
/// Пытается распарсить строку лога. Возвращает true, если строка валидна.
/// </summary>
public interface ILogParser
{
    bool TryParse(string line, out LogEntry? entry);
}