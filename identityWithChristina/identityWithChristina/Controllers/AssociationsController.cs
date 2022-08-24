using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using identityWithChristina;
using Microsoft.AspNetCore.Identity;
using identityWithChristina.Models;
using identityWithChristina.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace DBProject.Controllers
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

        //-----------User Actions-----------//

        //Displaying Associations
        public async Task<IActionResult> Index()
        {
            AssociationUserViewModel v = new()
            {
                Products = _context.Products.Where(p=>p.OwnerUserId == userManager.GetUserId(User) && p.ExchangationUserId ==null && p.DonationAssId == null).ToList(),
                Associations = _context.Associations.ToList()

            };
            return View(v);
        }


        //--------Donate New product for Association--//
        [HttpGet]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DonateNew(IFormFile file, [Bind("product,association")] ProductAssociationViewModel _Product)
        {
            _Product.product.PhotoUrl = "Image";
            ViewBag.request = "DonateNew";
            var prd = _Product.product.ProductId.ToString();
            prd = userManager.GetUserId(User);
            Createprd(_Product.product);

            if (file != null)
            {
                if (file.ContentType.ToLower().Contains("image"))
                {
                    string path = "wwwroot/productsImages/" + _Product.product.ProductId;
                    Directory.CreateDirectory("./" + path);
                    _Product.product.PhotoUrl = "/productsImages/" + _Product.product.ProductId + "/" + file.FileName;
                    using (var img = new FileStream(path + "/" + file.FileName, FileMode.Create))
                    {
                        file.CopyTo(img);
                    }
                    Update(_Product.product);
                    return RedirectToAction("index", "Products");
                }
            }
            if (_Product.product.ProductId != 0)
            {
                return RedirectToAction("index", "Products");
            }
            ModelState.AddModelError("", "Not Added");
            var productss = GetAll().Select(c => new { c.ProductId, c.ProductName });
            ViewBag.ProductId = new SelectList(productss, "ProductId", "ProductName", _Product.product.ProductId);

            return View(_Product);


        }


        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product Update(Product Entity)
        {
            Product o = _context.Products.FirstOrDefault(a => a.ProductId == Entity.ProductId);
            if (o != null)
            {
                o.ProductName = Entity.ProductName;
                o.ProductDescription = Entity.ProductDescription;
                o.DonationAssId = Entity.DonationAssId;
                o.Points = Entity.Points;
                o.PhotoUrl = Entity.PhotoUrl;

            }
            _context.SaveChanges();

            return o;
        }

        public Product Createprd(Product Entity)
        {
            _context.Products.Add(Entity);
            _context.SaveChanges();
            return Entity;
        }








        //----------------------------------------Admin Actions For Associations----------------------------------------//


        #region AdminIndex  // Index View for admin

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            IEnumerable<Association> list = _context.Associations.ToList();
            return View(list);
        }
        #endregion

        //-----------------------------------------------------------------------------------------------------//


        #region Create New Association
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Assid,Assname,AssPhone,AssDescription,AssAddress,AssLogoUrl")] Association association)
        {
            association.AssLogoUrl = "Image";
            ViewBag.request = "Create";
            var a = association.Assid.ToString();
            a = userManager.GetUserId(User);
            //association.Assid = a;
            Createassociation(association);
            if (file != null)
            {
                if (file.ContentType.ToLower().Contains("image"))
                {
                    string path = "wwwroot/AssociationImage/" + association.Assid;
                    Directory.CreateDirectory("./" + path);
                    association.AssLogoUrl = "/AssociationImage/" + association.Assid + "/" + file.FileName;
                    using (var img = new FileStream(path + "/" + file.FileName, FileMode.Create))
                    {
                        file.CopyTo(img);
                    }
                    Update_Association(association);
                    return RedirectToAction("AdminIndex", "Associations");
                }
            }
            if (association.Assid != 0)
            {
                return RedirectToAction("Index", "Associations");
            }


            return View(association);



        }


        //create association function
        public Association Createassociation(Association Entity)
        {
            _context.Associations.Add(Entity);
            _context.SaveChanges();
            return Entity;
        }
        #endregion

        //-----------------------------------------------------------------------------------------------------//


        #region Update association 
        public Association Update(Association Entity)
        {
            _context.Associations.Update(Entity);
            _context.SaveChanges();
            return Entity;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = _context.Associations.SingleOrDefault(a => a.Assid == id);
            if (association == null)
            {
                return NotFound();
            }
            return View(association);
        }


        [HttpPost]

        public async Task<IActionResult> Edit([Bind("Assid,Assname,AssDescription,AssAddress,AssPhone,AssLogoUrl")] Association association)
        {
            Association newAssociation = _context.Associations.FirstOrDefault(a => a.Assid == association.Assid);

            if (ModelState.IsValid)
            {
                try
                {
                    newAssociation = Update_Association(association);
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
                return RedirectToAction("AdminIndex");
            }
            return Content("error");
        }


        //Update Method For Updating
        public Association Update_Association([Bind("Assname,AssDescription,AssAddress,AssPhone,AssLogoUrl")] Association association)
        {
            Association o = _context.Associations.FirstOrDefault(a => a.Assid == association.Assid);
            if (o != null)
            {
                o.Assname = association.Assname;
                o.AssPhone = association.AssPhone;
                o.AssDescription = association.AssDescription;
                o.AssAddress = association.AssAddress;
                o.AssLogoUrl = association.AssLogoUrl;

            }
            _context.SaveChanges();

            return o;

        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        #endregion


        //-------------------------------------------------------------------------delete association--------------------------------------------------------------------------//

        #region Delete Association
        public void DeleteAssociation(int id)
        {
            Association s = _context.Associations.FirstOrDefault(a => a.Assid == id);
            if (s != null)
                _context.Associations.Remove(s);
            _context.SaveChanges();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {


            DeleteAssociation(id);
            _context.SaveChanges();
            //retrun to the list after deleting
            return RedirectToAction("AdminIndex");

        }

        #endregion




        // POST: Associations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Associations == null)
        //    {
        //        return Problem("Entity set 'TabadolContext.Associations'  is null.");
        //    }
        //    var association = await _context.Associations.FindAsync(id);
        //    if (association != null)
        //    {
        //        _context.Associations.Remove(association);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(AdminIndex));
        //}

        private bool AssociationExists(int id)
        {
            return (_context.Associations?.Any(e => e.Assid == id)).GetValueOrDefault();
        }

        //create controller for uploading new item for donation

    }
}
