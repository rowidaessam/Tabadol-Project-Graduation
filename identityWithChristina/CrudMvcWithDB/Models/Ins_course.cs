using System.ComponentModel.DataAnnotations.Schema;

namespace CrudMvcWithDB.Models
{
    public class Ins_course
    {
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
