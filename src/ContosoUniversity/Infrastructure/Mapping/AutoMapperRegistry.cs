namespace ContosoUniversity.Infrastructure.Mapping
{
    using AutoMapper;
    using StructureMap.Configuration.DSL;

    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<AutoMapperRegistry>();
                scan.AddAllTypesOf<Profile>();
                scan.WithDefaultConventions();
            });
        }
    }
}