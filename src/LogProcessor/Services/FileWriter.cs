using LogProcessor.Abstractions;

namespace LogProcessor.Services;

public class FileWriter : IFileWriter, IDisposable
{
    private readonly StreamWriter _writer;

    public FileWriter(string path)
    {
        _writer = new StreamWriter(path);
    }

    public void WriteLine(string line)
    {
        _writer.WriteLine(line);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}