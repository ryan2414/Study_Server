using System;
using System.Linq.Expressions;

namespace ServerCore
{
    internal class Program
    {
        static void MainThread(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Hello Thread!");
            }
        }

        static void Main(string[] args)
        {
         

            // 간단한 업무
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);

            for (int i = 0; i < 5; i++)
            {
                // 직원이 할 일감 단위?
                Task t = new Task(() => { while (true) { } }, TaskCreationOptions.LongRunning);
                t.Start();
            }

            //for (int i = 0; i < 4; i++)
            //    ThreadPool.QueueUserWorkItem((obj) => { while (true) { } });

            ThreadPool.QueueUserWorkItem(MainThread);

            //Thread t = new Thread(MainThread);
            //t.Name = "Test Thread";
            //t.IsBackground = true; // 백그라운드에서 실행
            //t.Start();

            //Console.WriteLine("Waiting for Thread");

            //t.Join(); // 끝날 때까지 기다림 
            //Console.WriteLine("Hello World!");
            while (true)
            {

            }
        }
    }
}