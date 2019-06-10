using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Identity;


namespace Momentum.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Upload Profile Image")]
        public string ImagePath { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<Friendship> Friendships { get; set; }

        public virtual ICollection<ProjectComment> ProjectComments { get; set; }

    }
}