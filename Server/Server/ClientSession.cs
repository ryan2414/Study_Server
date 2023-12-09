using ServerCore;
using System.Net;

namespace Server
{
    class Packet
    {
        public ushort size;
        public ushort packetId;

    }

    class PlayerInfoReq : Packet
    {
        public long playerId;
    }

    class PlayerInfoOk : Packet
    {
        public int hp;
        public int attack;
    }

    public enum PacketID
    {
        PlayerInfoReq = 1,
        PlayerInfoOk = 2,
    }

    // 엔진과 컨텐츠 세션을 분리하는 작업
    class ClientSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            /* Packet packet = new Packet() { size = 4, packetId = 7 };


             ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
             byte[] buffer = BitConverter.GetBytes(packet.size);
             byte[] buffer2 = BitConverter.GetBytes(packet.packetId);
             Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
             Array.Copy(buffer2, 0, openSegment.Array, buffer.Length, buffer2.Length);
             ArraySegment<byte> sendBuff = SendBufferHelper.Close(buffer.Length + buffer2.Length);
 */

            //Send(sendBuff);
            Thread.Sleep(5000);
            Disconnect();
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            ushort count = 0;

            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            switch ((PacketID)id)
            {
                case PacketID.PlayerInfoReq:
                    {
                        long playerId = BitConverter.ToInt64(buffer.Array, buffer.Offset + count);
                        count += 8;
                        Console.WriteLine($"PlayerInfoReq: {playerId}");

                    }
                    break;

            }
            Console.WriteLine($"RecvPacket Id : {id}, Size : {size}");
        }


        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");

        }
    }

}
