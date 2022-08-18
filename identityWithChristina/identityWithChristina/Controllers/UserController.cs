using identityWithChristina.Models;
using identityWithChristina.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace identityWithChristina.Controllers
{
    public class UserController : Controller
    {
            
        private readonly ITIContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> _userManager, ITIContext context)
        {
            _context = context;
            userManager = _userManager;
        }




        // GET: UserController
        public ActionResult Index()
        {
            string userid = userManager.GetUserId(User);
            var e = _context.ApplicationUsers.FirstOrDefault(i => i.Id == userid);
            return View(e);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string id)

        {
            string i = userManager.GetUserId(User);

            var e = _context.ApplicationUsers.FirstOrDefault(i => i.Id == id);

            return View(e);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit()
        {
            string userid=userManager.GetUserId(User);
            var obj=_context.ApplicationUsers.FirstOrDefault(i => i.Id == userid);
            EditProfileViewModel editObj=new EditProfileViewModel();
            editObj.points = obj.Points;
            editObj.email = obj.Email;
            editObj.street=obj.Street;
            editObj.city=obj.City;
            editObj.country=obj.Country;
            editObj.phone = obj.PhoneNumber;
            editObj.Lname=obj.Lname;
            editObj.ssn=obj.Ssn;
           
            editObj.gender=obj.Gender;
            editObj.postalCode=obj.PostalCode;

            return View(editObj);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProfileViewModel pro)
        {
            string userId=userManager.GetUserId(User);
            if (userId == string.Empty) { return NotFound(); }

            if (ModelState.IsValid)
            {

                string imgName = DateTime.Now.ToString("MM,dd,yyyy-HH:mm:ss");
             
                    //string path = "wwwroot/CategoriesImages/" + _category.CategoryId;
                    //Directory.CreateDirectory("./" + path);
                    //_category.PhotoUrl = "/CategoriesImages/" + _category.CategoryId + "/" + file.FileName;
                    //using (var img = new FileStream(path + "/" + file.FileName, FileMode.Create))
                    //{
                    //    file.CopyTo(img);
                    //}

                    var r = pro.profilePicture.FileName;
                    
                    var arr = pro.profilePicture.FileName.Split(".");
                    string ext = arr[arr.Length - 1];
                     string path = ".//wwwroot//gallary//" + r+pro.street + "." + ext;

                  //  var path = Path.Combine(
                  //Directory.GetCurrentDirectory(), "wwwroot/gallary",
                  //imgName);

                    string accPath = "/gallary/" + r + pro.street + "." + ext;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        pro.profilePicture.CopyTo(stream);
                    }


                
                ApplicationUser appUser = _context.ApplicationUsers.FirstOrDefault(e=>e.Id==userId);
                appUser.Street = pro.street;
                appUser.City = pro.city;
                appUser.PostalCode = pro.postalCode;
                appUser.Country = pro.country;
                appUser.Ssn = pro.ssn;
                appUser.Points = pro.points;
                appUser.Email = pro.email;
                appUser.Gender = pro.gender;
                appUser.Lname = pro.Lname;
                appUser.ProfilePictureUrl = accPath;


                _context.Update(appUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(); }


        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
