namespace CrudMvcWithDB.Models
{
    public class Stu_course
    {

        public int? Grade { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }


        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
