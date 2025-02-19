using System;
using System.Threading;

class Program
{
    static ThreadState threadStateIsBackgroundResult;
    static bool threadPoolTestResult;

    static void Main()
    {
        Console.WriteLine($"Is Thread Alive: {CheckThreadIsAlive()}");
        Console.WriteLine($"Thread State Unstarted: {CheckThreadStateIsUnstarted()}");
        Console.WriteLine($"Thread State Running: {CheckThreadStateIsRunning()}");
        Console.WriteLine($"Thread State Stopped: {CheckThreadStateIsStopped()}");
        Console.WriteLine($"Thread State WaitSleepJoin: {CheckThreadStateIsWaitSleepJoin()}");
        Console.WriteLine($"Thread State Background: {CheckThreadStateIsBackground()}");
        Console.WriteLine($"Is Background Thread: {CheckThreadIsBackground()}");
        Console.WriteLine($"Is Thread Pool Thread: {CheckThreadIsThreadPoolThread()}");
    }

    static bool CheckThreadIsAlive()
    {
        Thread thread = new Thread(() => Thread.Sleep(100));
        thread.Start();
        bool result = thread.IsAlive;
        thread.Join();
        return result;
    }

    static string CheckThreadStateIsUnstarted()
    {
        Thread thread = new Thread(() => { });
        return thread.ThreadState == ThreadState.Unstarted ? "Unstarted" : "Not Unstarted";
    }

    static string CheckThreadStateIsRunning()
    {
        string result = "Not Running";
        Thread thread = new Thread(() => result = Thread.CurrentThread.ThreadState.ToString());
        thread.Start();
        thread.Join();
        return result;
    }

    static string CheckThreadStateIsStopped()
    {
        Thread thread = new Thread(() => { });
        thread.Start();
        thread.Join();
        return thread.ThreadState == ThreadState.Stopped ? "Stopped" : "Not Stopped";
    }

    static string CheckThreadStateIsWaitSleepJoin()
    {
        string result = "Not in WaitSleepJoin";
        Thread thread = new Thread(() =>
        {
            Thread.Sleep(500);
            result = Thread.CurrentThread.ThreadState.ToString();
        });
        thread.Start();
        Thread.Sleep(100); 
        return result;
    }

    static string CheckThreadStateIsBackground()
    {
        Thread thread = new Thread(ThreadStateIsBackgroundTest);
        thread.IsBackground = true;
        thread.Start();
        thread.Join();
        return threadStateIsBackgroundResult.ToString();
    }

    static void ThreadStateIsBackgroundTest()
    {
        threadStateIsBackgroundResult = Thread.CurrentThread.ThreadState;
    }

    static string CheckThreadIsBackground()
    {
        Thread thread = new Thread(ThreadIsBackgroundTest);
        thread.IsBackground = true;
        thread.Start();
        thread.Join();
        return threadPoolTestResult.ToString();
    }

    static void ThreadIsBackgroundTest()
    {
        threadPoolTestResult = Thread.CurrentThread.IsBackground;
    }

    static string CheckThreadIsThreadPoolThread()
    {
        ThreadPool.QueueUserWorkItem(ThreadPoolTest);
        Thread.Sleep(100);
        return threadPoolTestResult.ToString();
    }

    static void ThreadPoolTest(object? state)
    {
        threadPoolTestResult = Thread.CurrentThread.IsThreadPoolThread;
    }
}
