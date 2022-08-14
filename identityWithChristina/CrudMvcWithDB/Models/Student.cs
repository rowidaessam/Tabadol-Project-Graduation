using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudMvcWithDB.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "required")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "required")]
        public string Lname { get; set; }

        [Range(15,50)]
        public int Age { get; set; }

        public string phone { get; set; }
        public string address { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }


        public Student() {
            Stu_courses = new List<Stu_course>();
        }
        public List<Stu_course> Stu_courses { get; set; }

    }
}
