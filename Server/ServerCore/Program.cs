using System.Buffers;

namespace ServerCore
{
    // 메모리 배리어
    // A) 코드 재배치 억제
    // B) 가시성

    // 1) *** Full Memory Barrier (ASM MFENCE, C# Thread.MemoryBarrier) : Store/Load 둘다 막는다.
    // 2) Store Memory Barrier (ASM SFENCE) : Store만 막는다.
    // 3) Load Memory Barrier (ASM LFENCE) : Load만 막는다.

    class Program
    {
        int _answer;
        bool _complete;

        void A()
        {
            _answer = 123;
            Thread.MemoryBarrier(); // Barrier 1
            _complete = true;
            Thread.MemoryBarrier(); // Barrier 2
        }

        void B()
        {
            Thread.MemoryBarrier(); // Barrier 3
            if ( _complete )
            {
                Thread.MemoryBarrier(); // Barrier 4
                Console.WriteLine(_answer);
            }
        }

        static void Main(string[] args)
        {
            
        }
    }
}