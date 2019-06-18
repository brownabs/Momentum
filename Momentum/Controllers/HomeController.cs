using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Momentum.Data;
using Momentum.Models;
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

            var projectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == true && p.User == user).Count();
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
        public async Task<IActionResult> GetUserProfile()
        {

            var user = await GetCurrentUserAsync();
            var projects = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user);

            var projectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == true && p.User == user).Count();
            ViewData["projectCount"] = projectCount;

            var friendships = _context.Friendship.Include(f => f.User).Include(f => f.Friended).Where(f => f.UserId == user.Id);
            var friendCount = _context.Friendship.Include(f => f.User).Include(f => f.Friended).Where(f => f.UserId == user.Id).Count();
            ViewData["friendCount"] = friendCount;

            UserProfileViewModel model = new UserProfileViewModel();

            model.Friendships = friendships;
            model.Projects = projects;
            model.User = user;



            return View(model);

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
