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

        public string Description { get; set; }

        public string Author { get; set; }


    }
}
