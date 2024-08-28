using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapaSmurfie.Models
{
    public class OwnedGameModel
    {
        [Key]
        public int RecordId { get; set; }
        public string UserOwnerName { get; set; } = null!;
        public int GameOwnedId { get; set; }
    }
}
