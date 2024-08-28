using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.Models.Models.ViewModels
{
    public class GameVM
    {
        public IEnumerable<GameModel> Games { get; set; } = null!;
        public IEnumerable<OwnedGameModel> GamesWithOwners { get; set; } = null!;

    }
}
