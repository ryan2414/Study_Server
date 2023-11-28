namespace ServerCore
{
    class FastLock
    {
        public int id;
    }

    internal class SessionManager
    {
        private FastLock l;
        private static object _lock = new object();

        public static void TestSession()
        {
            lock (_lock)
            {
            }
        }

        public static void Test()
        {
            lock (_lock)
            {
                UserManager.TestUser();
            }
        }
    }

    internal class UserManager
    {
        private FastLock l;
        private static object _lock = new Object();

        public static void Test()
        {
            //Monitor.TryEnter()
            lock (_lock)
            {
                SessionManager.TestSession();
            }
        }

        public static void TestUser()
        {
            lock (_lock)
            {
            }
        }
    }

    internal class Program
    {
        private static int number = 0;
        private static object _obj = new object();
        private static object _obj2 = new object();

        private static void Thread_1()
        {
            for (int i = 0; i < 10000; i++)
            {
                SessionManager.Test();

                // 상호배제 Mutual Exclusive
                //lock (_obj)
                //{
                //    number++;
                //}

                //try
                //{
                //    Monitor.Enter(_obj);
                //    number++;

                //    return;
                //}
                //finally
                //{
                //    Monitor.Exit(_obj);
                //}

                // CriticalSection std:mutex
                //Monitor.Enter(_obj); // 문을 잠그는 행위
                //{
                //    number++;

                //}
                //Monitor.Exit(_obj); // 잠금을 풀어준다.
            }
        }

        // 데드락 DeadLock

        private static void Thread_2()
        {
            for (int i = 0; i < 10000; i++)
            {
                UserManager.Test();
                //lock (_obj)
                //{
                //    number--;
                //}
                //Monitor.Enter(_obj);

                //number--;

                //Monitor.Exit(_obj);
            }
        }

        private static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();

            Thread.Sleep(100);

            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}