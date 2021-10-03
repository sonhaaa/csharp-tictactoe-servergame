using Csharp_tictoe_game.GameModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_tictoe_game.GameModels
{
    class User : BaseModel
    {
        public User(string username, string password, string displayName)
        {
            Username = username;
            Password = password;
            DisplayName = displayName;
            Avatar = "";
            Level = 1;
            Amount = 0;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public int Level { get; set; }
        public long Amount { get; set; }

        
    }
}
