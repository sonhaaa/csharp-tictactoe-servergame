using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_tictoe_game.Applications.Interfaces
{
    public interface IWsGameServer
    {
        void StartServer();
        void StopSerever();
        void RestartServer();

    }
}
