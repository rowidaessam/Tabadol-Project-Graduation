using Microsoft.EntityFrameworkCore;
using CrudMvcWithDB.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CrudMvcWithDB.Data
{
    // public class NewITI:DbContext
    public class NewITI : DbContext
    {
        public NewITI(DbContextOptions options) : base(options) { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;DataBase=NewITI;Integrated Security=True");

        }

        public DbSet<Student>? students { get; set; }
        public DbSet<Course> courses { get; set; }
         public DbSet<Instructor> instructors { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Ins_course> ins_Courses { get; set; }
        public DbSet<Stu_course>   stu_Courses  { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Ins_course>()
        .HasKey(o => new { o.InstructorId, o.CourseId });

        modelBuilder.Entity<Stu_course>()
            .HasKey(o => new { o.StudentId, o.CourseId });
          
        }

    }

    
}
