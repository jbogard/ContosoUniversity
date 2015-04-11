namespace ContosoUniversity.Features.Student
{
    using AutoMapper;
    using Models;

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Student, Index.Model>();
        }
    }
}