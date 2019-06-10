using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public class ProjectComment
    {

        [Key]
        public int ProjectCommentId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        //[Required]
        //public ApplicationUser User { get; set; }

    }
}

