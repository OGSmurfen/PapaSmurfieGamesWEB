using System.ComponentModel.DataAnnotations;

namespace PapaSmurfie.Models
{
    public class GameModel
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; } = null!;
        public string GameURL { get; set; } = "default";
        public string GameDescription { get; set; } = null!;
        public string GameCompanyName { get; set; } = "PapaSmurfie";
        public string GamePhotoURI { get; set; } = "default";
        public double GameRating { get; set; }
        public double GamePrice { get; set; }
        public string GameGenre { get; set; } = null!;


        public double RoundedGameRating { get { return Math.Round((GameRating * 10) / 10); } }
    }
}
