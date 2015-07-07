namespace ContosoUniversity.IntegrationTests
{
    using System;
    using System.Data.SqlClient;
    using System.Data.SqlLocalDb;
    using System.IO;
    using roundhouse;
    using Logger = roundhouse.infrastructure.logging.Logger;

    public static class LocalDbFactory
    {
        private static readonly Lazy<TemporarySqlLocalDbInstance> _instance = new Lazy<TemporarySqlLocalDbInstance>(ValueFactory);

        public static TemporarySqlLocalDbInstance Instance
        {
            get { return _instance.Value; }
        }

        private static TemporarySqlLocalDbInstance ValueFactory()
        {
            var instance = TemporarySqlLocalDbInstance.Create(true);
            var builder = instance.CreateConnectionStringBuilder();

            var databaseName = instance.Name;

            using (var conn = instance.CreateConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                var fileName = Path.Combine(Environment.CurrentDirectory, databaseName + ".mdf");
                cmd.CommandText = string.Format("CREATE DATABASE [{0}] on (name='{0}', filename='{1}')", databaseName, fileName);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine(instance.Name);

            builder.InitialCatalog = databaseName;

            var connectionString = builder.ConnectionString;

            var migrate = new Migrate().Set(c =>
            {
                c.ConnectionString = connectionString;
                c.DatabaseName = databaseName;
                c.Silent = true;
                c.WithTransaction = true;
                c.SqlFilesDirectory = Path.Combine(Environment.CurrentDirectory, "DatabaseMigration");
            });
            migrate.Run();

            return instance;
        }

        private class ConsoleLogger : Logger
        {
            public void log_a_debug_event_containing(string message, params object[] args)
            {
                Console.WriteLine(message, args);
            }

            public void log_an_info_event_containing(string message, params object[] args)
            {
                Console.WriteLine(message, args);
            }

            public void log_a_warning_event_containing(string message, params object[] args)
            {
                Console.WriteLine(message, args);
            }

            public void log_an_error_event_containing(string message, params object[] args)
            {
                Console.WriteLine(message, args);
            }

            public void log_a_fatal_event_containing(string message, params object[] args)
            {
                Console.WriteLine(message, args);
            }

            public object underlying_type { get { return typeof (ConsoleLogger); } }
        }
    }
}