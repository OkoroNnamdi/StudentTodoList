using System.Security.Policy;
using Microsoft.EntityFrameworkCore;

namespace StudentRestAPI.Model
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //send student table

            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 1,
                FirstName ="Okoro",
                LastName ="Nnamdi",
                Email = "okoronnamdi4044@gmail.com",
                Gender =Gender.Male,
                DepartmentId = 1,
                PhotoPath ="Images/okoro.png"

            });
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 2,
                FirstName = "Okeh",
                LastName = "Chukwu",
                Email = "okoronnamdi4044@gmail.com",
                Gender = Gender.Male,
                DepartmentId = 2,
                PhotoPath = "Images/okoro.png"

            });
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 3,
                FirstName = "Israel",
                LastName = "Mmadu",
                Email = "okoronnamdi@gmail.com",
                Gender = Gender.Female,
                DepartmentId = 3,
                PhotoPath = "Images/okoro.png"

            });
        }
    }
}
