using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesApp
{
    public class Game
    {
        public int GameId { get; set; }
        public int GenreId { get; set; }
        public string GameName { get; set; }
        public string Publisher { get; set; }
        public int Played { get; set; }
    }
}
