namespace ContosoUniversity.IntegrationTests
{
    using Ploeh.AutoFixture;

    public abstract class AutoFixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IdOmitterBuilder());
            fixture.Customizations.Add(new OmitListBuilder());

            CustomizeFixture(fixture);

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        protected abstract void CustomizeFixture(IFixture fixture);
    }
}