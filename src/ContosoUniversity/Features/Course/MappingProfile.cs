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
            CreateMap<Create.Command, Course>().ForMember(x => x.CourseID, x => x.MapFrom(z => z.CourseNumber));
            CreateMap<Course, Edit.Command>().ReverseMap();
            CreateMap<Course, Delete.Command>();
        }
    }
}