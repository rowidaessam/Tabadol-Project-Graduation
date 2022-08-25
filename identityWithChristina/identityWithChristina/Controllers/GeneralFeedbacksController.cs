using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using identityWithChristina;
using identityWithChristina.Models;
using Microsoft.AspNetCore.Identity;
using identityWithChristina.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace identityWithChristina.Controllers
{
    public class GeneralFeedbacksController : Controller
    {
        private readonly ITIContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public GeneralFeedbacksController(UserManager<ApplicationUser> _userManager, ITIContext context)
        {
            _context = context;
            userManager = _userManager;
        }

        // GET: GeneralFeedbacks
        public async Task<IActionResult> Index()
        {

            var iTIContext = _context.GeneralFeedbacks.Include(g => g.User).ToList();
            ViewData["feedlist"] = iTIContext;
            return View("Create");
            
        }

        // GET: GeneralFeedbacks/Details/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GeneralFeedbacks == null)
            {
                return NotFound();
            }

            var generalFeedback = await _context.GeneralFeedbacks
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.FeedId == id);
            if (generalFeedback == null)
            {
                return NotFound();
            }

            return View(generalFeedback);
        }

        // GET: GeneralFeedbacks/Create
        public IActionResult Create()
        {
            var iTIContext = _context.GeneralFeedbacks.Include(g => g.User).ToList();
            ViewData["feedlist"] = iTIContext;
            return View();
        }

        // POST: GeneralFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGFeedViewModel gfb)

        {

            string userid = userManager.GetUserId(User);
            GeneralFeedback g=new GeneralFeedback();
            g.Rate = gfb.Rate;
            g.Date= DateTime.Now;
            g.Message = gfb.message;
            g.UserId = userid;
            g.User=_context.ApplicationUsers.FirstOrDefault(u => u.Id == userid);

            if (ModelState.IsValid)
            {
                _context.Add(g);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(gfb);
        }

        // GET: GeneralFeedbacks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GeneralFeedbacks == null)
            {
                return NotFound();
            }

            var generalFeedback = await _context.GeneralFeedbacks.FindAsync(id);
            if (generalFeedback == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", generalFeedback.UserId);
            return View(generalFeedback);
        }

        // POST: GeneralFeedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedId,UserId,Message,Rate,Date")] GeneralFeedback generalFeedback)
        {
            if (id != generalFeedback.FeedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralFeedbackExists(generalFeedback.FeedId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", generalFeedback.UserId);
            return View(generalFeedback);
        }

        // GET: GeneralFeedbacks/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GeneralFeedbacks == null)
            {
                return NotFound();
            }

            var generalFeedback = await _context.GeneralFeedbacks
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.FeedId == id);
            if (generalFeedback == null)
            {
                return NotFound();
            }

            return View(generalFeedback);
        }

        // POST: GeneralFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GeneralFeedbacks == null)
            {
                return Problem("Entity set 'ITIContext.GeneralFeedbacks'  is null.");
            }
            var generalFeedback = await _context.GeneralFeedbacks.FindAsync(id);
            if (generalFeedback != null)
            {
                _context.GeneralFeedbacks.Remove(generalFeedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralFeedbackExists(int id)
        {
          return _context.GeneralFeedbacks.Any(e => e.FeedId == id);
        }
    }
}
