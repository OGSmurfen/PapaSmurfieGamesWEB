using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.Models.Models
{
    public class UserStatusModel
    {
        [Key]
        public int UserStatusId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
