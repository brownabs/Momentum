using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.ViewModels
{
    public class HomePageViewModel
    {
        public Quote Quote { get; set; }

        //don't know yet if I need an IEnumerable or List of Quotes
        public IEnumerable<Quote> Quotes { get; set; }

        public Project Project { get; set; }

        //don't know yet if I need an IEnumerable or List of Quotes
        public IEnumerable<Project> Projects { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

    }

}
