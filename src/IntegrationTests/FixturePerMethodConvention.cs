namespace ContosoUniversity.IntegrationTests
{
    public class FixturePerMethodConvention : IntegrationTestConvention
    {
        public FixturePerMethodConvention()
        {
            Classes
                .ConstructorDoesntHaveArguments();

            ClassExecution
                .CreateInstancePerCase();

            FixtureExecution
                .Wrap<DeleteData>();
        }
    }
}