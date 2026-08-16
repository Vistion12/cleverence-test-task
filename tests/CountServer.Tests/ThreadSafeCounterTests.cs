using CountServer.Services;
using FluentAssertions;

namespace CountServer.Tests;

public class ThreadSafeCounterTests
{
    public ThreadSafeCounterTests()
    {
        ThreadSafeCounter.Reset();
    }

    [Fact]
    public void GetCount_WhenNoWriters_ReturnsInitialValue()
    {
        var result = ThreadSafeCounter.GetCount();

        result.Should().Be(0);
    }

    [Fact]
    public void AddToCount_SingleWriter_UpdatesCount()
    {
        ThreadSafeCounter.AddToCount(5);

        ThreadSafeCounter.GetCount().Should().Be(5);
    }

    [Fact]
    public async Task AddToCount_MultipleWriters_SequentiallyUpdatesCount()
    {
        var writers = Enumerable.Range(0, 10)
            .Select(_ => Task.Run(() => ThreadSafeCounter.AddToCount(1)))
            .ToArray();

        await Task.WhenAll(writers);

        ThreadSafeCounter.GetCount().Should().Be(10);
    }

    [Fact]
    public async Task GetCount_ParallelReaders_DoNotBlockEachOther()
    {
        ThreadSafeCounter.AddToCount(10);

        var readers = Enumerable.Range(0, 10)
            .Select(_ => Task.Run(() => ThreadSafeCounter.GetCount()))
            .ToArray();

        var results = await Task.WhenAll(readers);

        results.All(r => r == 10).Should().BeTrue();
    }
}