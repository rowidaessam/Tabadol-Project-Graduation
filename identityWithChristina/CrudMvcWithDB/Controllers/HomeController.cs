using CrudMvcWithDB.Data;
using CrudMvcWithDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CrudMvcWithDB.Controllers
{
    public class HomeController : Controller
    {
        IStudent ist;
        NewITI ni;
        public HomeController(IStudent _is ,NewITI _ni) { 
        ist=_is;
            ni = _ni;
        }
      
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            
            return View(ist.GetAllStudents().ToList());
        }

        // create 
        public IActionResult Create()
        {
            
            SelectList deps = new SelectList(ni.departments.ToList(), "ID", "Name");
            ViewBag.depts = deps;
           
            return View();
        }
        // create [http post]
        [HttpPost]
        public IActionResult Create(Student student)
        {
            ist.AddStudent(student);

            return RedirectToAction("Index", ist.GetAllStudents().ToList());
        }

        //update 
        public IActionResult Update(int ID) {
            
            SelectList deps = new SelectList(ni.departments.ToList(), "ID", "Name");
            ViewBag.depts = deps;
            ist.GetStudentById(ID);
           

            return View(ist.GetStudentById(ID));
        }

        // update [https post]
        [HttpPost]
        public IActionResult Update(Student student)
        {
            ist.UpdateStudent(student);
            return RedirectToAction("Index", ist.GetAllStudents().ToList());
        }

        public IActionResult Delete(int id) {

            ist.DeleteStudent(id);

            return RedirectToAction("Index", ist.GetAllStudents().ToList());
        }

        public IActionResult Details(int id)
        {

           

            return View(ist.GetStudentById(id));
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