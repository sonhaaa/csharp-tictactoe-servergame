using Csharp_tictoe_game.Applications.Interfaces;
using Csharp_tictoe_game.Applications.Messaging;
using Csharp_tictoe_game.Applications.Messaging.Constants;
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
        public Player(WsServer server) : base(server)
        {
            SessionId = this.Id.ToString();
            IsDisconnected = false;
        }

        public override void OnWsConnected(HttpRequest request)
        {
            Console.WriteLine("Player connected");
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
                        var x = 10;
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
                Console.WriteLine(e);
            }
            ((WsGameServer)Server).SendAll($"{this.SessionId} send message {mess}");
        }

        public void OnDisconnect()
        {
            ((WsGameServer)Server).PlayerManager.RemovePlayer(this);
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
