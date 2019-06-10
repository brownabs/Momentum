using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        [DataType(DataType.Date)] 
        public DateTime DurationGoal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateCompleted { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        public string Language { get; set; }

        [Required]
        public string SourceCodeLink { get; set; }

        public string PublishedApplicationLink { get; set; }

        [Display(Name = "Upload Project Image")]
        public string ImagePath { get; set; }

        public bool IsCompleted { get; set; }
 

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
