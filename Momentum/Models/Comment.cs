using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Comment Date")]
        public DateTime DateCreated { get; set; }

        [Required]
        public int ProjectId { get; set; }


        public Project Project { get; set; }

        [Required]
        public string UserId { get; set; }

   
        public ApplicationUser User { get; set; }

    }
}
