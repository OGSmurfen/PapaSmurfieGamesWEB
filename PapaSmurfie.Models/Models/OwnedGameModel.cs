using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapaSmurfie.Models
{
    public class OwnedGameModel
    {
        [Key]
        public int RecordId { get; set; }
        public int UserOwnerId { get; set; }
        [ForeignKey("UserOwnerId")]
        public ApplicationUser UserOwner { get; set; } = null!;
        public int GameOwnedId { get; set; }
        [ForeignKey("GameOwnedId")]
        public GameModel GameOwned { get; set; } = null!;
    }
}
