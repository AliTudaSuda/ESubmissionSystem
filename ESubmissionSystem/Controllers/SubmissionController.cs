using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESubmissionSystem.Data;
using ESubmissionSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ESubmissionSystem.Controllers
{
    [Authorize]
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubmissionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var user = await _userManager.GetUserAsync(User);
            var submissions = from s in _context.Submissions select s;

            if (!User.IsInRole("Admin") && !User.IsInRole("Management") && User.Identity.Name != "manager@sunway.edu")
            {
                submissions = submissions.Where(s => s.UserEmail == user.Email);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                submissions = submissions.Where(s => s.Description.Contains(searchString));
            }

            return View(await submissions.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Submission submission)
        {
            if (ModelState.IsValid)
            {
                submission.UserEmail = User.Identity.Name;
                submission.Status = "Pending";
                submission.CreatedDate = DateTime.Now;
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(submission);
        }

        public IActionResult Dashboard()
        {
            if (User.IsInRole("Management") || User.IsInRole("Admin") || User.Identity.Name == "manager@sunway.edu")
            {
                ViewBag.PendingCount = _context.Submissions.Where(s => s.Status == "Pending").Count();
                ViewBag.ApprovedCount = _context.Submissions.Where(s => s.Status == "Approved").Count();
                ViewBag.RejectedCount = _context.Submissions.Where(s => s.Status == "Rejected").Count();
                ViewBag.TotalAmount = _context.Submissions.Sum(s => s.Amount);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Approve(int id)
        {
            if (User.IsInRole("Management") || User.IsInRole("Admin") || User.Identity.Name == "manager@sunway.edu")
            {
                var sub = await _context.Submissions.SingleOrDefaultAsync(m => m.Id == id);
                if (sub != null)
                {
                    sub.Status = "Approved";
                    _context.Update(sub);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int id)
        {
            if (User.IsInRole("Management") || User.IsInRole("Admin") || User.Identity.Name == "manager@sunway.edu")
            {
                var sub = await _context.Submissions.SingleOrDefaultAsync(m => m.Id == id);
                if (sub != null)
                {
                    sub.Status = "Rejected";
                    _context.Update(sub);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var submission = await _context.Submissions.SingleOrDefaultAsync(m => m.Id == id);
            if (submission == null) return NotFound();
            return View(submission);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var submission = await _context.Submissions.SingleOrDefaultAsync(m => m.Id == id);
            if (submission == null) return NotFound();
            return View(submission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submissions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}