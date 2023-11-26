using System;
using System.Linq.Expressions;

namespace ServerCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 5 * 5 배경
            // [][][][][] [][][][][] [][][][][] [][][][][] [][][][][]
            int[,] arr = new int[10000, 10000];

            // Spacial Locality로 상대적으로 빠름.
            // 주변의 정보를 캐시해 놓음
            {
                long now = DateTime.Now.Ticks;
                for (int i = 0; i < 10000; i++)
                    for (int j = 0; j < 10000; j++)
                        arr[i, j] = 1;
                long end = DateTime.Now.Ticks;
                Console.WriteLine($"(y, x) 순서 걸린 시간 {end - now}");
            }
            // 캐시를 사용할 수 없는 코드 
            {
                long now = DateTime.Now.Ticks;
                for (int i = 0; i < 10000; i++)
                    for (int j = 0; j < 10000; j++)
                        arr[j, i] = 1;
                long end = DateTime.Now.Ticks;
                Console.WriteLine($"(x, y) 순서 걸린 시간 {end - now}");
            }
        }
    }
}