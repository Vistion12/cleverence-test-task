namespace CountServer.Services;

/// <summary>
/// Потокобезопасный сервер счётчика.
/// Читатели работают параллельно, писатели — эксклюзивно.
/// </summary>
public static class ThreadSafeCounter
{
    private static readonly ReaderWriterLockSlim Lock = new();
    private static int _count;

    public static int GetCount()
    {
        Lock.EnterReadLock();
        try
        {
            return _count;
        }
        finally
        {
            Lock.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        Lock.EnterWriteLock();
        try
        {
            _count += value;
        }
        finally
        {
            Lock.ExitWriteLock();
        }
    }

    /// <summary>
    /// Сбрасывает счётчик в ноль. Используется в тестах для изоляции состояния.
    /// </summary>
    public static void Reset()
    {
        Lock.EnterWriteLock();
        try
        {
            _count = 0;
        }
        finally
        {
            Lock.ExitWriteLock();
        }
    }
}