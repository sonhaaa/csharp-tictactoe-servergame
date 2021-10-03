using Csharp_tictoe_game.Applications.Handlers;
using System.Net;
using System;
using Csharp_tictoe_game.Applications.Interfaces;

namespace Csharp_tictoe_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IPlayerManager playerManager = new PlayersManager();
            var wsServer = new WsGameServer(IPAddress.Any, port: 8080, playerManager);
            wsServer.StartServer();
            for (; ; )
            {
                string type = Console.ReadLine();
                if (type.Equals("restart"))
                {
                    wsServer.RestartServer();
                }

                if (type.Equals("shutdown"))
                {
                    wsServer.StopSerever();
                    break;
                }
            }
        }
    }
}
