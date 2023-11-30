namespace ServerCore
{
  
    internal class Program
    {
        // 1. 근성 
        // 2. 양보
        // 3. 갑질

            // 상호배제
            // Monitor
        private static object _lock = new object();
        private static SpinLock _lock2 = new SpinLock();// <= 1번과 2번의 혼합 형태
        private static Mutex _lock3 = new Mutex();
        // 직접 만든다.


        // RWLock ReaderWriterLock
        static ReaderWriterLockSlim _lock4 = new ReaderWriterLockSlim();


        class Reward
        {
            
        }

        static Reward GetRewardById(int id)
        {
            _lock4.EnterReadLock();

            _lock4.ExitReadLock();

            return null;
        }

        void AddReward(Reward reward)
        {
            _lock4.EnterWriteLock();

            _lock4.ExitWriteLock();
        }

        private static void Main(string[] args)
        {
            lock (_lock)
            {
                
            }   
            
            bool lockTaken = false;
            try
            {
                _lock2.Enter(ref lockTaken);
            }
            finally
            {
                if (lockTaken)
                    _lock2.Exit();
            }

        }
    }
}