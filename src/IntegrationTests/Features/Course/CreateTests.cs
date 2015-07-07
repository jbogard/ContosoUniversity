namespace ContosoUniversity.IntegrationTests.Features.Course
{
    using System.Linq;
    using ContosoUniversity.Features.Course;
    using Models;
    using Shouldly;

    public class CreateTests
    {
        public void Should_create(TestContextFixture fixture, Department dept)
        {
            fixture.SaveAll(dept);

            var command = new Create.Command
            {
                Title = "Blarg",
                Credits = 10,
                Department = dept
            };

            fixture.Send(command);

            Course course = null;

            fixture.DoClean(ctx => course = ctx.Set<Course>().FirstOrDefault(c => c.Title == command.Title));

            course.Title.ShouldBe(command.Title);
            course.Credits.ShouldBe(command.Credits);
            course.DepartmentID.ShouldBe(dept.DepartmentID);
        }
    }
}