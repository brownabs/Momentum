using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        public string Author { get; set; }


    }
}
