using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A am Lion");

            using (var db = new StudentContext())
            {
                var student = new Student() { Name = "Fabio" };
                var mathSub = new Subject() { Name = "MAth" };
                var scienceSubj = new Subject() { Name = "Data struct" };
                student.Subjects.Add(mathSub);
                student.Subjects.Add(scienceSubj);

                db.Students.Add(student);
                db.SaveChanges();
            }

            Console.ReadKey();
        }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        public virtual List<Subject> Subjects { get; set; }

        public Student()
        {
            this.Subjects = new List<Subject>();
        }
    }
    public class Subject
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public virtual Student Student { get; set; }
    }
}
