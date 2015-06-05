namespace ContosoUniversity.IntegrationTests
{
    using System;
    using System.Data.SqlClient;
    using Fixie;
    using Respawn;

    public class DeleteData : FixtureBehavior, ClassBehavior
    {
        private static Checkpoint checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                "sysdiagrams",
            },
            SchemasToExclude = new[]
            {
                "RoundhousE"
            }
        };

        public void Execute(Fixture context, Action next)
        {
            DeleteAllData();
            next();
        }

        public void Execute(Class context, Action next)
        {
            DeleteAllData();
            next();
        }

        private static void DeleteAllData()
        {
            var localDbInstance = LocalDbFactory.Instance;
            var builder = localDbInstance.CreateConnectionStringBuilder();
            builder.InitialCatalog = localDbInstance.Name;

            checkpoint.Reset(builder.ConnectionString);
        }
    }
}