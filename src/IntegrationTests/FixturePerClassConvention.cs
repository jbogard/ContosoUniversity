namespace ContosoUniversity.IntegrationTests
{
    public class FixturePerClassConvention : IntegrationTestConvention
    {
        public FixturePerClassConvention()
        {
            Classes
                .ConstructorHasArguments();

            ClassExecution
                .CreateInstancePerClass()
                .Wrap<DeleteData>();
        }
    }
}