namespace ServerCore
{
    class Program
    {
        private static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() => { return $"My Name is {Thread.CurrentThread.ManagedThreadId}"; });

        static void WhoAmI()
        {
            //ThreadName.Value = $"My Name is {Thread.CurrentThread.ManagedThreadId}";
            bool repeat = ThreadName.IsValueCreated;
            if (repeat)
                Console.WriteLine(ThreadName.Value + "(Repeat)");
            else
                Console.WriteLine(ThreadName.Value);


        }

        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(3, 3);

            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);

            ThreadName.Dispose();
        }
    }
}