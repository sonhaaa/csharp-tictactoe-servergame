using Csharp_tictoe_game.Applications.Interfaces;
using Csharp_tictoe_game.Applications.Messaging;
using Csharp_tictoe_game.Applications.Messaging.Constants;
using Csharp_tictoe_game.GameModels;
using Csharp_tictoe_game.Logging;
using GameDatabase.Mondodb.Interfaces;
using GameDatabase.Mongodb.Handlers;
using MongoDB.Driver;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_tictoe_game.Applications.Handlers
{
    public class Player : WsSession, IPlayer
    {
        public string SessionId { get; set; }
        public string Name { get; set; }
        private bool IsDisconnected { get; set; }
        private readonly IGameLogger _logger;
        private IGameDB<User> UsersDb { get; set; }

        public Player(WsServer server, IMongoDatabase mongoDatabase) : base(server)
        {
            SessionId = this.Id.ToString();
            IsDisconnected = false;
            _logger = new GameLogger();
            UsersDb = new MongoHandler<User>(mongoDatabase);
        }

        public override void OnWsConnected(HttpRequest request)
        {
            _logger.Info("Player connected");
            IsDisconnected = false;
        }

        public override void OnWsDisconnected()
        {
            // TODO: login on player connected
            OnDisconnect();
            base.OnWsDisconnected();
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            var mess = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            try
            {
                var wsMess = GameHelper.ParseStruct<WsMessage<Object>>(mess);
                switch (wsMess.Tags)
                {
                    case WsTags.Invalid:
                        break;
                    case WsTags.Login:
                        var loginData = GameHelper.ParseStruct<LoginData>(wsMess.Data.ToString());
                        var user = new User(username: loginData.Username , password: loginData.Password, displayName: "");
                        var newUser = UsersDb.Create(user);
                        break;
                    case WsTags.Register:
                        break;
                    case WsTags.Lobby:
                        break;
                }
            }
            catch (Exception e)
            {
                // TODO: handle invalid login
                _logger.Error("OnWsReceived error", e);
            }
            //((WsGameServer)Server).SendAll($"{this.SessionId} send message {mess}");
        }

        public void OnDisconnect()
        {
            _logger.Warning("Player disconnected", null);
            //((WsGameServer)Server).PlayerManager.RemovePlayer(this);
        }

        public bool SendMessage(string mes)
        {
            return this.SendTextAsync(mes);
        }

        public void SetDisconnect(bool value)
        {
            this.IsDisconnected = value;
        }
    }
}
