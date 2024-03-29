﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Momentum.Models.ViewModels
{
    public class UserProfileViewModel
    {

        public IFormFile ImageFile { get; set; }
        public Project Project { get; set; }

        //don't know yet if I need an IEnumerable or List of Quotes
        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<Project> AccomplishedProjects { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public IEnumerable<ApplicationUser> notFriends { get; set; }

        public IEnumerable<ApplicationUser> peopleCurrentUserIsFollowing { get; set; }

        public IEnumerable<ApplicationUser> peopleFollowingCurrentUser { get; set; }

        public Friendship Friendship { get; set; }

        public IEnumerable<Friendship> Friendships { get; set; }
    }
}
