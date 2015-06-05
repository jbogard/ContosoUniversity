namespace ContosoUniversity.IntegrationTests
{
    using Ploeh.AutoFixture;

    public abstract class IntegrationTestConvention : FixieConvention
    {
        public IntegrationTestConvention()
        {
            Classes
                .AreInIntegrationTestNamespace();
        }

        protected override ICustomization AutoFixtureCustomization
        {
            get { return new IntegrationTestsFixtureCustomization(); }
        }
    }
}