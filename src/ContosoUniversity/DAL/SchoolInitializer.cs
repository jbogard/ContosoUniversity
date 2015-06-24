using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {

            var instructors = new List<Instructor>
            {
                new Instructor{FirstMidName="Mary",LastName="Walker", HireDate=DateTime.Parse("2001-08-01")},
                new Instructor{FirstMidName="Jill",LastName="Seaver", HireDate=DateTime.Parse("2009-11-12")},
                new Instructor{FirstMidName="Mark",LastName="Scottman", HireDate=DateTime.Parse("2003-04-11")},
                new Instructor{FirstMidName="Joe",LastName="Filler", HireDate=DateTime.Parse("1990-04-01")},
            };

            instructors.ForEach(s => context.Instructors.Add(s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department{Administrator= instructors[0],Budget=1200,Name="Science", StartDate=DateTime.Parse("1990-04-01") },
                new Department{Administrator= instructors[1],Budget=1200,Name="Business", StartDate=DateTime.Parse("1990-04-01") },
                new Department{Administrator= instructors[2],Budget=1200,Name="Math", StartDate=DateTime.Parse("1990-04-01") },
                new Department{Administrator= instructors[3],Budget=1200,Name="English", StartDate=DateTime.Parse("1990-04-01") }
            };

            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();


            var students = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3, Department=departments[0]},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,Department=departments[1]},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,Department=departments[1]},
            new Course{CourseID=1045,Title="Calculus",Credits=4,Department=departments[2]},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,Department=departments[2]},
            new Course{CourseID=2021,Title="Composition",Credits=3,Department=departments[3]},
            new Course{CourseID=2042,Title="Literature",Credits=4,Department=departments[3]}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment>
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}