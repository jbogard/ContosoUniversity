namespace ContosoUniversity.Features.Department
{
    using AutoMapper;
    using Models;

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Department, IndexModel>();
            CreateMap<CreateModel, Department>(MemberList.Source);
            CreateMap<Department, EditModel>().ReverseMap();
            CreateMap<Department, DeleteModel>();
        }
    }
}