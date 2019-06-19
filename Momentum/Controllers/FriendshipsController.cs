using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Momentum.Data;
using Momentum.Models;
using Momentum.Models.ViewModels;

namespace Momentum.Controllers
{
    public class FriendshipsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public FriendshipsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users That Are Not Friends With Current User
        public async Task<IActionResult> Index()
        {
            //created a list of users

            List<ApplicationUser> usersNotFriendsWith = new List<ApplicationUser>();

            //get current user
            var currentUser = await GetCurrentUserAsync();

            //get users that aren't current user
            var otherPersons = await _context.ApplicationUsers.Where(u => u.Id != currentUser.Id).ToListAsync();

            //get all friendships
            var allFriendRelationships = await _context.Friendship.ToListAsync();

            //flag set friend to false
            bool friend = false;

            //iterate over all other users
            foreach (var otherPerson in otherPersons)
            {
                    friend = false;

                //iterate over all friendships
                foreach (var friendship in allFriendRelationships)
                {

                    // if other user's id is equal to a userId in the Friendship table 
                    //and current user is equal to a FriendId on the Friendship table
                    //set friend to true
                    if (otherPerson.Id == friendship.UserId && currentUser.Id == friendship.FriendId)
                    {
                        friend = true;
                    }

                    //also if current user id is equal to UserId on table 
                    //and other user's id is equal to FriendId on Friendship table
                    //set friend to true
                    if (currentUser.Id == friendship.UserId && otherPerson.Id == friendship.FriendId)
                    {
                        friend = true;
                        
                    }

                   
                }
                    //if there isn't a relationship on the FriendShip Table, set friend to false and add to list
                    if (friend == false)
                    {
                        usersNotFriendsWith.Add(otherPerson);
                        
                    }

            }
                if (usersNotFriendsWith == null)
                {
                    return NotFound();
                }

                var UsersNotFriendsWithCount = usersNotFriendsWith.Count();
             

            ViewData["UsersNotFriendsWithCount"] = UsersNotFriendsWithCount;
            return View(usersNotFriendsWith);

        }


       

        // GET: Friendships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendship = await _context.Friendship
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FriendshipId == id);
            if (friendship == null)
            {
                return NotFound();
            }

            return View(friendship);
        }

        // GET: Friendships/Create
        public async Task<IActionResult> Create(string id)
        {
            var personToFriend = await _context.ApplicationUsers.FirstOrDefaultAsync(p => p.Id == id);

            var user = await GetCurrentUserAsync();
          

            var friendship = new Friendship()
            {
                FriendId = id,
                UserId = user.Id,
                Friended = personToFriend,
                User = user
               
            };


           
            return View(friendship);
        }

        // POST: Friendships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId, FriendId")] Friendship friendship)
        {    

            if (ModelState.IsValid)
            {
                _context.Add(friendship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", friendship.UserId);
            return View(friendship);
        }

        // GET: Friendships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendship = await _context.Friendship.FindAsync(id);
            if (friendship == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", friendship.UserId);
            return View(friendship);
        }

        // POST: Friendships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendshipId,UserId")] Friendship friendship)
        {
            if (id != friendship.FriendshipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friendship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendshipExists(friendship.FriendshipId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", friendship.UserId);
            return View(friendship);
        }

        // GET: Friendships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendship = await _context.Friendship
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FriendshipId == id);
            if (friendship == null)
            {
                return NotFound();
            }

            return View(friendship);
        }

        // POST: Friendships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friendship = await _context.Friendship.FindAsync(id);
            _context.Friendship.Remove(friendship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendshipExists(int id)
        {
            return _context.Friendship.Any(e => e.FriendshipId == id);
        }
    }
}
