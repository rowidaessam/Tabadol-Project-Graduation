using CrudMvcWithDB.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudMvcWithDB.Models
{
    public interface IStudent
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(int id);
        public void DeleteStudent(int id);
        public void UpdateStudent(Student std);
        public void AddStudent(Student std);




    }
    public class StudentOp:IStudent
    {
        // NewITI n=new NewITI();
        NewITI n;
        public StudentOp(NewITI _n) { 
        n= _n;
        }

        // get all
        public List<Student> GetAllStudents() {
            var students = n.students.Include(q => q.Department).ToList();
            return students;
        }
        // get element by id
        public Student GetStudentById(int id)
        {
           return n.students.Include(q => q.Department).FirstOrDefault(i => i.ID == id);
           // return (Student)student;
        }
        // delete 
        public void DeleteStudent(int id)
        {
            n.students.Remove(GetStudentById(id));
            n.SaveChanges();
        }

        // update
        public void UpdateStudent(Student std)
        {
            n.students.Update(std);
            n.SaveChanges();
        }
        /// adding new
        public void AddStudent(Student std)
        {
            n.students.Add(std);
            n.SaveChanges();
        }
    }
}
