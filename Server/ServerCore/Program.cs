namespace ServerCore
{
  
    internal class Program
    {
        private static int _num = 0;

        // AutoResetEvent 보다 좀더 많은 정보를 가지고 있음.
        // ex)
        // counting 몇번 잠겄는지
        // thread id 등
        static Mutex _lock = new Mutex(); // 커널 동기화 객체 

        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.WaitOne();
                _num++;
                _lock.ReleaseMutex();
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.WaitOne();
                _num--;
                _lock.ReleaseMutex();
            }
        }

        private static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(_num);
        }
    }
}