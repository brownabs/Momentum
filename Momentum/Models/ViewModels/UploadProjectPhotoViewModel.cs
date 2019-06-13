using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.ViewModels
{
    public class UploadProjectPhotoViewModel
    {
        public Project Project { get; set; }

        [Display(Name = "Project Image")]
        public IFormFile ImageFile { get; set; }
    }
}
