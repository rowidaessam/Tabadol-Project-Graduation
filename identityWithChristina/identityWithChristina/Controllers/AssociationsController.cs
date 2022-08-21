using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using identityWithChristina.Models;
using identityWithChristina;
using Microsoft.AspNetCore.Identity;
using identityWithChristina.ViewModel;

namespace identityWithChristina.Controllers
{
    public class AssociationsController : Controller
    {
        private readonly ITIContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public AssociationsController(ITIContext context, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            userManager = _userManager;
        }

        // GET: Associations
        //public async Task<IActionResult> Index()
        //{
        //      return _context.AssociationUserProductViewModels != null ? 
        //                  View(await _context.
        //                  AssociationUserProductViewModels.ToList()) :
        //                  Problem("Entity set 'TabadolContext.Associations'  is null.");
        //}
        public async Task<IActionResult> Index()
        {
            AssociationUserProductViewModel v = new()
            {
                Products = _context.Products.ToList(),
                Associations = _context.Associations.ToList()
            };
            return View(v);
        }
        [HttpGet]
        public IActionResult DonateNew(int id)
        {
            ViewBag.categoryee = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            ProductAssociationViewModel a = new()
            {
                association = _context.Associations.Single(a => a.Assid == id),
                product = new Product()
            };

            return View(a);
        }

        

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product Update(Product Entity)
        {
            _context.Products.Update(Entity);
            _context.SaveChanges();
            return Entity;
        }

        public Product Createprd(Product Entity)
        {
            _context.Products.Add(Entity);
            _context.SaveChanges();
            return Entity;
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> DonateNew(IFormFile file, Product _Product, Category cat)
        {
            ViewBag.request = "GetAssociationName";
            ViewBag.categoryee = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            var prd = _Product.ProductId.ToString();
            prd = userManager.GetUserId(User);
            Createprd(_Product);

            if (file != null)
            {
                if (file.ContentType.ToLower().Contains("image"))
                {
                    string path = "wwwroot/productssImages/" + _Product.ProductId;
                    Directory.CreateDirectory("./" + path);
                    _Product.PhotoUrl = "/productsImages/" + _Product.ProductId + "/" + file.FileName;
                    using (var img = new FileStream(path + "/" + file.FileName, FileMode.Create))
                    {
                        file.CopyTo(img);
                    }
                    Update(_Product);
                    return RedirectToAction("index", "Products");
                }
            }
            if (_Product.ProductId != 0)
            {
                return RedirectToAction("index", "Products");
            }
            ModelState.AddModelError("", "Not Added");
            var productss = GetAll().Select(c => new { c.ProductId, c.ProductName });
            ViewBag.ProductId = new SelectList(productss, "ProductId", "ProductName", _Product.ProductId);

            return View(_Product);

        }

        // GET: Associations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .FirstOrDefaultAsync(m => m.Assid == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // GET: Associations/Create


        // POST: Associations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ASSId,ASSName,AssCity,AssCountry,AssStreet,Phone")] Association association)
        {
            if (ModelState.IsValid)
            {
                _context.Add(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(association);
        }

        // GET: Associations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = await _context.Associations.FindAsync(id);
            if (association == null)
            {
                return NotFound();
            }
            return View(association);
        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ASSId,ASSName,AssCity,AssCountry,AssStreet,Phone")] Association association)
        {
            if (id != association.Assid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(association);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationExists(association.Assid))
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
            return View(association);
        }

        // GET: Associations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .FirstOrDefaultAsync(m => m.Assid == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Associations == null)
            {
                return Problem("Entity set 'TabadolContext.Associations'  is null.");
            }
            var association = await _context.Associations.FindAsync(id);
            if (association != null)
            {
                _context.Associations.Remove(association);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssociationExists(int id)
        {
            return (_context.Associations?.Any(e => e.Assid == id)).GetValueOrDefault();
        }

        //create controller for uploading new item for donation

    }
}
