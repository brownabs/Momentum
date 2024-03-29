﻿using System;
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
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Completion Date Goal")]
        public DateTime DurationGoal { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Completion Date")]
        public DateTime? DateCompleted { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        [Display(Name = "Programming Language")]
        public string Language { get; set; }

        [Required]
        [Display(Name = "Github Repository Link")]
        public string SourceCodeLink { get; set; }

        public string PublishedApplicationLink { get; set; }

        [Display(Name = "Upload Project Image")]
        public string ImagePath { get; set; }


        [Display(Name = "Mark complete or incomplete")]
        public bool IsCompleted { get; set; }
 

        [Required]
        public string UserId { get; set; }


        public ApplicationUser User { get; set; }

        [NotMapped]
        public string Error { get; set; }

    }
}
