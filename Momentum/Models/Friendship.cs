using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public class Friendship
    {

        [Key]
        public int FriendshipId { get; set; }

        [Required]
        public string FriendId { get; set; }

        public ApplicationUser Friended { get; set; }

        [Required]
        public string UserId { get; set; }
      
        public ApplicationUser User { get; set; }


    }
}
