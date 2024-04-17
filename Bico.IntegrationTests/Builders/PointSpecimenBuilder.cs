using AutoFixture.Kernel;
using NetTopologySuite.Geometries;

namespace Bico.IntegrationTests.Builders;

public class PointSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(Point))
            return new Point(-46.53343257495357, -23.667905555073688) { SRID = 4326 };

        return new NoSpecimen();
    }
}