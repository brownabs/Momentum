using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.UploadProfilePhotoViewModel
{
    public class UploadProfilePhotoViewModel
    {
        public ApplicationUser User { get; set; }

        public IFormFile ImageFile { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

    }
}