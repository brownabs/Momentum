using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Momentum.Data;
using Momentum.Models;
using Momentum.Models.UploadProfilePhotoViewModel;
using Momentum.Models.ViewModels;

namespace Momentum.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var user = await GetCurrentUserAsync();
            var projects = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user);

            var projectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user).Count();
            ViewData["projectCount"] = projectCount;

            var quotes = _context.Quote.ToList();




            Random rand = new Random();
            var models = quotes.OrderBy(c => rand.Next()).ToList().Take(1);



            HomePageViewModel model = new HomePageViewModel();

            model.Projects = projects;
            model.User = user;
            model.Quotes = models;


            return View(model);

        }

        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var user = await GetCurrentUserAsync();

            if(id != null)
            {
                user = await _context.ApplicationUsers.FindAsync(id);
            }

            var projects = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user); 

            var projectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user).Count();
            ViewData["projectCount"] = projectCount;


            //created a list of users

            List<ApplicationUser> usersCurrentUserHasAdded = new List<ApplicationUser>();
            List<ApplicationUser> usersfollowingCurrentUser = new List<ApplicationUser>();





            //get users that aren't current user
            var otherUsers = await _context.ApplicationUsers.Where(u => u.Id != user.Id).ToListAsync();

            //get all friendships
            var allFriendRelationships = await _context.Friendship.ToListAsync();

            //flag set friend to false
            bool friend = false;
            bool follower = false;
            bool following = false;

            //iterate over all other users
            foreach (var otherPerson in otherUsers)
            {
                friend = false;
                follower = false;
                following = false;

                //iterate over all friendships
                foreach (var friendship in allFriendRelationships)
                {

                    //also if current user id is equal to UserId on table 
                    //and other user's id is equal to FriendId on Friendship table
                    //set friend to true
                    if (user.Id == friendship.UserId && otherPerson.Id == friendship.FriendId)
                    {
                        friend = true;
                        following = true;
                    }

                    if (user.Id == friendship.FriendId && otherPerson.Id == friendship.UserId)
                    {
                        friend = true;
                        follower = true;
                    }
                }

                if (friend == true && following == true)
                {
                    usersCurrentUserHasAdded.Add(otherPerson);
                }

                if (friend == true && follower == true)
                {
                    usersfollowingCurrentUser.Add(otherPerson);
                }
            }
            if (usersCurrentUserHasAdded == null)
            {
                return NotFound();
            }

            if (usersfollowingCurrentUser == null)
            {
                return NotFound();
            }

            var UserFollowingCount = usersCurrentUserHasAdded.Count();
            var UserFollowersCount = usersfollowingCurrentUser.Count();

            ViewData["UserFollowingCount"] = UserFollowingCount;
            ViewData["UserFollowersCount"] = UserFollowersCount;


            var accomplishedProjects = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == true && p.User == user);

            var accomplishedProjectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == true && p.User == user).Count();
            ViewData["accomplishedProjectCount"] = accomplishedProjectCount;


            UserProfileViewModel model = new UserProfileViewModel();

            model.peopleCurrentUserIsFollowing = usersCurrentUserHasAdded;
            model.peopleFollowingCurrentUser = usersfollowingCurrentUser;
            model.AccomplishedProjects = accomplishedProjects;
            model.Projects = projects;
            model.User = user;


            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProfilePic (UserProfileViewModel viewModel)
        {
            var currentuser = await GetCurrentUserAsync();
            var User = await _context.ApplicationUsers.FindAsync(currentuser.Id);

            ModelState.Remove("viewModel.Id");

            if (viewModel.ImageFile != null)
            {
                var fileName = Path.GetFileName(viewModel.ImageFile.FileName);
                Path.GetTempFileName();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images/profilepictures", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.ImageFile.CopyToAsync(stream);
                }

                User.ImagePath = viewModel.ImageFile.FileName;
            }


            _context.Update(User);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetUserProfile));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
