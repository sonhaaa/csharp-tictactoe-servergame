using Csharp_tictoe_game.Applications.Interfaces;
using NetCoreServer;
using System;
using System.Net;
using System.Net.Sockets;

namespace Csharp_tictoe_game.Applications.Handlers
{
    public class WsGameServer : WsServer, IWsGameServer
    {
        private int _port;
        public readonly IPlayerManager PlayerManager;

        public WsGameServer(IPAddress address, int port, IPlayerManager playerManager) : base(address, port)
        {
            _port = port;
            PlayerManager = playerManager;
        }

        protected override TcpSession CreateSession()
        {
            // TODO: handle new session
            Console.WriteLine("New Session connected");
            var player = new Player(this);
            PlayerManager.AddPlayer(player);
            return player;
        }

        protected override void OnDisconnected(TcpSession session)
        {
            Console.WriteLine("Session disconnected");
            var player = PlayerManager.FindPlayer(session.Id.ToString());
            if (player != null)
            {
                PlayerManager.RemovePlayer(player);
                // TODO: mark player disconnected;
            }
            base.OnDisconnected(session);
        }

        public void StartServer()
        {
            // TODO: logic before start server
            if (this.Start())
            {
                Console.WriteLine($"Server Ws started at {_port}");
                return;
            }
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Server Ws error");
            base.OnError(error);
        }

        public void StopSerever()
        {
            // TODO: logic before stop server
            this.Stop();
        }

        public void RestartServer()
        {
            // TODO: logic before restart server
            this.Restart();
        }

        public void SendAll(string mes)
        {
            this.MulticastText(mes);
        }
    }
}
