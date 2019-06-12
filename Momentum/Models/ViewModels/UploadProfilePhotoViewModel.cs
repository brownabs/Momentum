using Microsoft.AspNetCore.Http;
using Momentum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.ViewModels
{
    public class UploadProfilePhotoViewModel
    {

        public ApplicationUser User { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
