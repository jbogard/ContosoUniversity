namespace ContosoUniversity.Features.Student
{
    using AutoMapper;
    using Models;

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Student, Index.Model>();
            CreateMap<Student, Details.Model>();
            CreateMap<Enrollment, Details.Model.Enrollment>();
        }
    }
}