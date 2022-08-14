namespace CrudMvcWithDB.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string location { get; set; }
        public DateTime ManagerHireDate { get; set; }

        public Department() { 
        Students = new List<Student>();
            Instructors = new List<Instructor>();
        }
        public ICollection<Student> Students { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}
