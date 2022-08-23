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
using identityWithChristina.ViewModel;

namespace identityWithChristina.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ITIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductsController(ITIContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? category)
        {


            CategoryProductViewModel CatPrd = new CategoryProductViewModel()
            {
                Categories = _context.Categories.ToList(),
                Products = _context.Products.Where(p => p.ExchangationUserId == null && p.DonationAssId == null).ToList()
            };
            return View(CatPrd);

        }
        [Authorize]
        public async Task<IActionResult> MyProducts()
        {
            string productUser =  _userManager.GetUserId(User);

            CategoryProductViewModel CatPrd = new CategoryProductViewModel()
            {
                Categories = _context.Categories.ToList(),
                Products = _context.Products.Where(n => n.OwnerUserId == productUser && n.ExchangationUserId == null && n.DonationAssId == null).ToList()
            };
            return View(CatPrd);

        }

        public async Task<IActionResult> Filter(int? id)
        {


            CategoryProductViewModel CatPrd = new CategoryProductViewModel()
            {
                Categories = _context.Categories.ToList(),
                Products = _context.Products.Where(n=>n.CategoryId == id).ToList()
                //Products = _context.Products.Where(n=>n.CategoryId == id && p => p.ExchangationUserId == null && p.DonationAssId == null).ToList()
            };
            return View("index", CatPrd);



        }
        [Authorize]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.DonationAss)
                .Include(p => p.ExchangationUser)
                .Include(p => p.OwnerUser)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.categoryee = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product Createprd(Product Entity)
        {
            _context.Products.Add(Entity);
            _context.SaveChanges();
            return Entity;
        }
        public Product Find(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Update(Product Entity)
        {
            _context.Products.Update(Entity);
            _context.SaveChanges();
            return Entity;
        }


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("ProductId,ProductName,Points,ProductDescription, PhotoUrl, CategoryId, OwnerUserId, ExchangationUserId, DonationAssId")] Product _Product)
        {
            _Product.PhotoUrl = "Image";
            ViewBag.request = "Create";
            var prd = _Product.ProductId.ToString();
            prd = _userManager.GetUserId(User);
            _Product.OwnerUserId = prd;
            Createprd(_Product);

            if (file != null)
            {
                if (file.ContentType.ToLower().Contains("image"))
                {
                    string path = "wwwroot/productsImages/" + _Product.ProductId;
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

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.categoryee = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["DonationAssId"] = new SelectList(_context.Associations, "Assid", "Assid", product.DonationAssId);
            ViewData["ExchangationUserId"] = new SelectList(_context.Users, "UserId", "UserId", product.ExchangationUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "UserId", "UserId", product.OwnerUserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Points,ProductDescription,PhotoUrl,CategoryId,OwnerUserId,ExchangationUserId,DonationAssId")] Product product)
        {
            ViewBag.categoryee = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["DonationAssId"] = new SelectList(_context.Associations, "Assid", "Assid", product.DonationAssId);
            ViewData["ExchangationUserId"] = new SelectList(_context.Users, "UserId", "UserId", product.ExchangationUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "UserId", "UserId", product.OwnerUserId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.DonationAss)
                .Include(p => p.ExchangationUser)
                .Include(p => p.OwnerUser)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ITIContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
