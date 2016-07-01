namespace ContosoUniversity.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using StructureMap;

    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            var profiles =
                from t in typeof (AutoMapperRegistry).Assembly.GetTypes()
                where typeof (Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);


            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
        }
    }
}