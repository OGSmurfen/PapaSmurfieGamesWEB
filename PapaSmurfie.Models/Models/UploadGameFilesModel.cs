using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.Models.Models
{
    public class UploadGameFilesModel
    {
        public IFormFile GameFile { get; set; }
        public IFormFile GameImage { get; set; }
    }
}
