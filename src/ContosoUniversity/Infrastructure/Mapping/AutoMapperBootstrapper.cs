namespace ContosoUniversity.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using DependencyResolution;
    using AutoMapper;

    public class AutoMapperBootstrapper
    {
        private static bool _initialized;
        private static readonly object Lock = new object();

        public static void Initialize(StructureMapDependencyScope scope)
        {
            lock (Lock)
            {
                if (_initialized) return;
                InitializeInternal(scope);
                _initialized = true;
            }
        }

        private static void InitializeInternal(StructureMapDependencyScope scope)
        {
            //var logger = Logger.Instance;

            //logger.Debug("Initializing Automapper");

            Mapper.Initialize(cfg =>
            {
                var profileNames = new List<string>();
                foreach (var profile in scope.GetAllInstances<Profile>())
                {
                    profileNames.Add(profile.ProfileName);
                    cfg.AddProfile(profile);
                }

                //logger.Verbose("Added profiles: {ProfileName}", profileNames);

                cfg.ConstructServicesUsing(scope.GetInstance);
            });
        }
    }
}