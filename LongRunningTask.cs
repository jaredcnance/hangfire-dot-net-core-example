using System;
using System.Threading;

namespace concurrent_queues
{
  public static class LongRunningTask
  {
    public static void Run()
    {
      Thread.Sleep(20000);
      Console.WriteLine("Done");
    }
  }
}
