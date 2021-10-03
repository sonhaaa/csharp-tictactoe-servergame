using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_tictoe_game.Logging
{
    public interface IGameLogger
    {
        void Print(string msg);
        void Info(string info);
        void Warning(string msg, Exception exception);
        void Error(string error);
        void Error(string error, Exception exception);
    }
}
