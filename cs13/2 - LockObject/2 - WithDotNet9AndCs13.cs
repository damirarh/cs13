using FluentAssertions;

namespace cs13.LockObject;

public class WithDotNet9AndCs13
{
    public interface IWithLock
    {
        public List<string> Messages { get; }
        public void Invoke(string message, int delay);
    }

    public class WithLockStatement : IWithLock
    {
        private readonly Lock myLock = new Lock();

        public List<string> Messages { get; } = new List<string>();

        public void Invoke(string message, int delay)
        {
            lock (myLock)
            {
                Thread.Sleep(delay);
                Messages.Add(message);
            }
        }
    }

    [Test]
    public void LockStatementProtectsCriticalSection()
    {
        var withLock = new WithLockStatement();

        var task1 = Task.Run(() => withLock.Invoke("Message 1", 200));
        Thread.Sleep(50);
        var task2 = Task.Run(() => withLock.Invoke("Message 2", 50));
        Task.WaitAll([task1, task2]);

        withLock.Messages.Should().Equal("Message 1", "Message 2");
    }

    public class WithoutLockStatement : IWithLock
    {
        private readonly Lock myLock = new Lock();

        public List<string> Messages { get; } = new List<string>();

        public void Invoke(string message, int delay)
        {
            using (myLock.EnterScope())
            {
                Thread.Sleep(delay);
                Messages.Add(message);
            }
        }
    }

    [Test]
    public void LockClassProtectsCriticalSection()
    {
        var withLock = new WithoutLockStatement();

        var task1 = Task.Run(() => withLock.Invoke("Message 1", 200));
        Thread.Sleep(50);
        var task2 = Task.Run(() => withLock.Invoke("Message 2", 50));
        Task.WaitAll([task1, task2]);

        withLock.Messages.Should().Equal("Message 1", "Message 2");
    }

    public class WithLockStatementBroken : IWithLock
    {
        private readonly object myLock = new Lock();

        public List<string> Messages { get; } = new List<string>();

        public void Invoke(string message, int delay)
        {
            lock (myLock)
            {
                Thread.Sleep(delay);
                Messages.Add(message);
            }
        }
    }
}
