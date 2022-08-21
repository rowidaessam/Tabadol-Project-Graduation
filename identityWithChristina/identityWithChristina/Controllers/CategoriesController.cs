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
using Microsoft.AspNetCore.Authorization;

namespace identityWithChristina.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ITIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public CategoriesController(ITIContext context,
                             UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'ITIContext.Categories'  is null.");
        }



        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category CreateCat(Category Entity)
        {
            _context.Categories.Add(Entity);
            _context.SaveChanges();
            return Entity;
        }
        public Category Find(int id)
        {
            return _context.Categories.Find(id);
        }

        public Category Update(Category Entity)
        {
            _context.Categories.Update(Entity);
            _context.SaveChanges();
            return Entity;
        }


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("CategoryId,CategoryName,Description,PhotoUrl")] Category _category)
        {
            ViewBag.request = "Create";
            var catog = _category.CategoryId.ToString();
            catog = _userManager.GetUserId(User);
            CreateCat(_category);

            if (file != null)
            {
                if (file.ContentType.ToLower().Contains("image"))
                {
                    string path = "wwwroot/CategoriesImages/" + _category.CategoryId;
                    Directory.CreateDirectory("./" + path);
                    _category.PhotoUrl = "/CategoriesImages/" + _category.CategoryId + "/" + file.FileName;
                    using (var img = new FileStream(path + "/" + file.FileName, FileMode.Create))
                    {
                        file.CopyTo(img);
                    }
                    Update(_category);
                    return RedirectToAction("index", "Categories");
                }
            }
            if (_category.CategoryId != 0)
            {
                return RedirectToAction("index", "Categories");
            }
            ModelState.AddModelError("", "Not Added");
            var categoriees = GetAll().Select(c => new { c.CategoryId, c.CategoryName });
            ViewBag.CategoryId = new SelectList(categoriees, "CategoryId", "CategoryName", _category.CategoryId);

            return View(_category);

        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model, IFormFile img1)
        {
            ViewBag.request = "Edit";
            string userid = _userManager.GetUserId(User);
            Category cat = Find(model.CategoryId);
            if (model.CategoryId.ToString() == userid)
            {
                if (img1 != null)
                {
                    if (img1.ContentType.ToLower().Contains("image"))
                    {
                        string pathforfolder = "wwwroot/CategoriesImages/" + model.CategoryId;
                        model.PhotoUrl = "/CategoriesImages/" + model.CategoryId + "/" + img1.FileName;
                        using (var ms = new FileStream(pathforfolder + "/" + img1.FileName, FileMode.Create))
                        {
                            img1.CopyTo(ms);
                        }
                    }
                }

                cat.CategoryId = model.CategoryId;
                cat.CategoryName = model.CategoryName;
                cat.Description = model.Description;
                cat.PhotoUrl = model.PhotoUrl;

                try
                {
                    Update(cat);
                }
                catch (Exception)
                {
                    List<Category> categories = GetAll();
                    ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", model.CategoryId);
                    return RedirectToAction("index", model);
                }
                return RedirectToAction("index", model);
            }
            return View("AccessDenied");
        }



        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ITIContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
