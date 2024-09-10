using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.Models.Models
{
    public class FriendsList
    {
        [Key]
        public int FriendshipRecordId { get; set; }


        public string FriendshipSenderId { get; set; } = null!;


        public string FriendshipReceiverId { get; set; } = null!;


        public string Status { get; set; } = "Pending";
    }
}
