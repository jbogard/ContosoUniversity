namespace ContosoUniversity.Features.Department
{
    using AutoMapper;
    using Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, Index.Model>();
            CreateMap<Create.Command, Department>(MemberList.Source);
            CreateMap<Department, Edit.Command>().ReverseMap();
            CreateMap<Department, Delete.Command>();
        }
    }
}