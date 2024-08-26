namespace PapaSmurfie.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string GameName { get; set; } = null!;
        public string GameURL { get; set; } = null!;
        public string GameDescription { get; set; } = null!;
        public string GameCompanyName { get; set; } = "PapaSmurfie";
        public double GameRating { get; set; }
        public double RoundedGameRating { get { return Math.Round((GameRating * 10) / 10); } }
    }
}
