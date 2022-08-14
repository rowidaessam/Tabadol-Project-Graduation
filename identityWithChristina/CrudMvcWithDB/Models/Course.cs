namespace CrudMvcWithDB.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public int Duration { get; set; }


        public Course() {
            ins_Courses = new List<Ins_course>();
            stu_Courses= new List<Stu_course>();
        }
        public List<Ins_course> ins_Courses { get; set; }


       
        public List<Stu_course>stu_Courses { get; set; }

    }
}
