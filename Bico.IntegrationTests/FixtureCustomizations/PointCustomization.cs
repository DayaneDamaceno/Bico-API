using AutoFixture;
using Bico.IntegrationTests.Builders;

namespace Bico.IntegrationTests.FixtureCustomizations;

public class PointCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new PointSpecimenBuilder());
    }
}
