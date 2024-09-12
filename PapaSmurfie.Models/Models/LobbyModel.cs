using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.Models.Models
{
    public class LobbyModel
    {
        [Key]
        public int LobbyModelId { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string LobbyId { get; set; } = null!;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
