using System;
using Newtonsoft.Json;

namespace Csharp_tictoe_game.Applications.Handlers
{
    public static class GameHelper
    {
        public static string ParseString<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T ParseStruct<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
