using Csharp_tictoe_game.Applications.Interfaces;
using Csharp_tictoe_game.Logging;
using GameDatabase.Mongodb.Handlers;
using NetCoreServer;
using System;
using System.Net;
using System.Net.Sockets;

namespace Csharp_tictoe_game.Applications.Handlers
{
    public class WsGameServer : WsServer, IWsGameServer
    {
        private readonly int _port;
        private readonly IPlayerManager _playerManager;
        private readonly IGameLogger _logger;
        private readonly MongoDb _mongoDB;

        public WsGameServer(IPAddress address, int port, IPlayerManager playerManager, IGameLogger logger, MongoDb mongoDb) : base(address, port)
        {
            _port = port;
            _playerManager = playerManager;
            _logger = logger;
            _mongoDB = mongoDb;
        }

        protected override TcpSession CreateSession()
        {
            // TODO: handle new session
            _logger.Info("New Session connected");
            var player = new Player(this, _mongoDB.GetDatabase());
            _playerManager.AddPlayer(player);
            return player;
        }

        protected override void OnDisconnected(TcpSession session)
        {
            _logger.Info("Session disconnected");
            var player = _playerManager.FindPlayer(session.Id.ToString());
            if (player != null)
            {
                _playerManager.RemovePlayer(player);
                // TODO: mark player disconnected;
            }
            base.OnDisconnected(session);
        }

        public void StartServer()
        {
            // TODO: logic before start server
            if (this.Start())
            {
                _logger.Print($"Server Ws started at {_port}");
                return;
            }
        }

        protected override void OnError(SocketError error)
        {
            _logger.Error($"Server Ws error");
            base.OnError(error);
        }

        public void StopSerever()
        {
            // TODO: logic before stop server
            this.Stop();
            _logger.Print("Server Ws stopped");
        }

        public void RestartServer()
        {
            // TODO: logic before restart server
            if (this.Restart()) 
            {
                _logger.Print("Server Ws restarted");
            }
        }

        public void SendAll(string mes)
        {
            this.MulticastText(mes);
        }
    }
}
