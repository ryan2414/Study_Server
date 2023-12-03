using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    internal class Listener
    {
        Socket _listenSocket;
        Action<Socket> _onAcceptHandler;

        public void Init(IPEndPoint endPoint, Action<Socket> onAcceptHandler)
        {
            // 문지기
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _onAcceptHandler = onAcceptHandler;

            // 문지기 교육
            _listenSocket.Bind(endPoint);

            // 영업 시작
            // backlog : 최대 대기수 
            _listenSocket.Listen(10);
        

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            RegisterAccept(args);
        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            // args 초기화 필요
            args.AcceptSocket = null;

            bool pending = _listenSocket.AcceptAsync(args);
            if (pending == false)
                OnAcceptCompleted(null, args);
            
        }

        // 공유 변수에 대한 접근을 주의해야됨 
        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // TODO:
                _onAcceptHandler?.Invoke(args.AcceptSocket);
            }
            else
                Console.WriteLine(args.SocketError.ToString());

            RegisterAccept(args);
        }

    }
}
