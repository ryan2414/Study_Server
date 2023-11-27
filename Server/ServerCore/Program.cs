﻿namespace ServerCore
{
    // 메모리 배리어
    // A) 코드 재배치 억제
    // B) 가시성

    // 1) *** Full Memory Barrier (ASM MFENCE, C# Thread.MemoryBarrier) : Store/Load 둘다 막는다.
    // 2) Store Memory Barrier (ASM SFENCE) : Store만 막는다.
    // 3) Load Memory Barrier (ASM LFENCE) : Load만 막는다.

    internal class Program
    {
        static volatile int x = 0;
        static volatile int y = 0;
        static volatile int r1 = 0;
        static volatile int r2 = 0;

        // 하드웨어가 지 마음데로 순서를 연관된 것이 없으면 순서를 바꿀 수도 있음.
        
        static void Thread_1()
        {
            y = 1; // Store y

            // -------------------------------
            Thread.MemoryBarrier();
            
            r1 = x; // Load x
        }

        static void Thread_2()
        {
            x = 1; // Story x

            // -------------------------------
            Thread.MemoryBarrier();

            r2 = y; // Load y
        }

        static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                count++;
                x = y = r1 = r2 = 0;

                Task t1 = new Task(Thread_1);
                Task t2 = new Task(Thread_2);
                t1.Start();
                t2.Start();

                Task.WaitAll(t1, t2);

                if (r1 == 0 && r2 == 0)
                    break;
            }

            Console.WriteLine($"{count} 번 만에 빠져나옴!");
        }
    }
}