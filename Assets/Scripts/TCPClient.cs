using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Net;
using System.Text;

namespace TCPetc
{
    public class TCPClient
    {
        public IPEndPoint ServerIPEndPoint { get; set; }
        private Socket Socket { get; set; }
        public const int BufferSize = 1024;
        public byte[] Buffer { get; } = new byte[BufferSize];

        public TCPClient(string Host, int port)
        {
            this.ServerIPEndPoint = new IPEndPoint(IPAddress.Parse(Host), port);
        }

        // ソケット通信の接続
        public void Connect()
        {
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Socket.Connect(this.ServerIPEndPoint);

            // 非同期で受信を待機
            this.Socket.BeginReceive(this.Buffer, 0, BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), this.Socket);
        }

        // ソケット通信接続の切断
        public void DisConnect()
        {
            this.Socket?.Disconnect(false);
            this.Socket?.Dispose();
        }

        // メッセージの送信(同期処理)
        public void Send(string message)
        {
            var sendBytes = new UTF8Encoding().GetBytes(message);
            this.Socket.Send(sendBytes);
        }

        // 非同期受信のコールバックメソッド(別スレッドで実行される)
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            var socket = asyncResult.AsyncState as Socket;

            var byteSize = -1;
            try
            {
                // 受信を待機
                byteSize = socket.EndReceive(asyncResult);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return;
            }

            // 受信したデータがある場合、その内容を表示する
            // 再度非同期での受信を開始する
            if (byteSize > 0)
            {
                Debug.Log($"{Encoding.UTF8.GetString(this.Buffer, 0, byteSize)}");
                socket.BeginReceive(this.Buffer, 0, this.Buffer.Length, SocketFlags.None, ReceiveCallback, socket);
                ChoiceClass.InputHolder = Encoding.UTF8.GetString(this.Buffer, 0, byteSize);
                ChoiceClass.WaitOperation = true;
            }
        }
    }
}
