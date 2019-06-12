using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.ViewModels
{
    public class UploadProjectPhotoViewModel
    {
        public Project Project { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
