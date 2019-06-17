using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Momentum.Data;
using Momentum.Models;
using Momentum.Models.ViewModels;

namespace Momentum.Controllers
{
    public class ProjectsController : Controller
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Projects
        [Authorize]
        public async Task<IActionResult> Index()
        {


            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user);

            var projectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == false && p.User == user).Count();
            ViewData["projectCount"] = projectCount;
                return View(await applicationDbContext.ToListAsync());
            
            
        }

        // GET: Projects
        [Authorize]
        public async Task<IActionResult> GetAccomplishedProjects()
        {
            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == true && p.User == user);

            var projectCount = _context.Project.Include(p => p.User).Where(p => p.IsCompleted == true && p.User == user).Count();
            ViewData["projectCount"] = projectCount;
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize]
        public IActionResult Create()
        {

            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            UploadProjectPhotoViewModel viewModel = new UploadProjectPhotoViewModel();
            viewModel.Project = new Project();
            return View(viewModel);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UploadProjectPhotoViewModel viewModel)
        {

            ModelState.Remove("Project.UserId");
            var user = await GetCurrentUserAsync();

            viewModel.Project.UserId = user.Id;
            viewModel.Project.User = user;
            viewModel.Project.DateCreated = DateTime.Now;

           // If you want to check errors in model state use the code below:

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null)
                {
                    // don't rely on or trust the FileName property without validation
                    //**Warning**: The following code uses `GetTempFileName`, which throws
                    // an `IOException` if more than 65535 files are created without 
                    // deleting previous temporary files. A real app should either delete
                    // temporary files or use `GetTempPath` and `GetRandomFileName` 
                    // to create temporary file names.
                    var fileName = Path.GetFileName(viewModel.ImageFile.FileName);
                    Path.GetTempFileName();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images/projectpictures", fileName);
                    //var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(stream);
                        // validate file, then move to CDN or public folder
                    }

                    viewModel.Project.ImagePath = viewModel.ImageFile.FileName;
                }
                _context.Add(viewModel.Project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel.Project);
        }



    // GET: Projects/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", project.UserId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,DateCreated,DurationGoal,DateCompleted,ProjectName,Language,SourceCodeLink,PublishedApplicationLink,ImagePath,IsCompleted,UserId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", project.UserId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
