using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.Models.Models.ViewModels
{
    public class FriendRequestsVM
    {
        public List<string> OutgoingPending { get; set; } = null!;
        public List<string> IncomingPending { get; set; } = null!;
        public List<string> Accepted { get; set; } = null!;
    }
}
