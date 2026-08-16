namespace LogProcessor.Models;

public class LogEntry
{
    public string Date { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Method { get; set; } = "DEFAULT";
    public string Message { get; set; } = string.Empty;
}