using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudMvcWithDB.Models
{
    public class Instructor
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public int salary { get; set; }

        [Required(ErrorMessage = "required")]
        public string Degree { get; set; }


        public Instructor() {
            ins_Courses = new List<Ins_course>();
        }
        public List<Ins_course> ins_Courses { get; set; }


        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}
