using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.ViewModels
{
    public class UserAndFriendsViewModel
    {

        [Key]
        public int FriendshipId { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

    }
}
