namespace ContosoUniversity.Features.Course
{
    using AutoMapper;
    using Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, Index.Result.Course>();
            CreateMap<Course, Details.Model>();
            CreateMap<Create.Command, Course>();
            CreateMap<Course, Edit.Command>().ReverseMap();
            CreateMap<Course, Delete.Command>();
        }
    }
}