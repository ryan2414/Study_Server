namespace ServerCore
{
    class Program
    {
        static int number = 0;

        static void Thread_1()
        {
            // atomic = 원자성
            // 더 이상 쪼개지면 안 되는 단위.

            // 골드 -= 100;

            // 서버다운

            // 인벤 += 검;

            for (int i = 0; i < 1000000; i++)
            {
                // All or Nothing
                int afterValue = Interlocked.Increment(ref number);

                //number++;

                //int temp = number;
                //temp += 1;
                //number = temp;
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Interlocked.Decrement(ref number);

                //number--;

                //int temp = number;
                //temp += 1;
                //number = temp;}
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}