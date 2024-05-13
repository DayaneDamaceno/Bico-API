using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Bico.Infra.Extensions;

internal static class PostGeoExtensions
{
    public static void AddStDWithin(ModelBuilder modelBuilder)
    {
            var methodInfo = typeof(PostGeoExtensions)
                .GetMethod(nameof(StDWithin), [typeof(Geometry), typeof(Geometry), typeof(float), typeof(bool?)]);
            if (methodInfo != null)
                modelBuilder
                    .HasDbFunction(methodInfo)
                    .HasTranslation(args =>
                        new SqlFunctionExpression(
                            "public",
                            "st_dwithin",
                            args,
                            true,
                          [false, false, false, true],
                          typeof(bool),
                          new NpgsqlBoolTypeMapping()
                      ));
        
    }

    public static void AddStDistance(ModelBuilder modelBuilder)
    {
        var methodInfo = typeof(PostGeoExtensions)
            .GetMethod(nameof(StDistance), [typeof(Geometry), typeof(Geometry), typeof(bool?)]);
        if (methodInfo != null)
            modelBuilder
                .HasDbFunction(methodInfo)
                .HasTranslation(args =>
                    new SqlFunctionExpression(
                        "public",
                        "st_distance",
                        args,
                        false,
                        [ false, false, true ],
                        typeof(double),
                        null
                    ));
    }


    public static bool StDWithin(this Geometry point1, Geometry p2, float distanceInMeter, bool? useSpheroid = true)
    {
        return point1.IsWithinDistance(p2, distanceInMeter);
    }

    public static double StDistance(this Geometry geom1, Geometry geom2, bool? useSpheroid = true)
    {
        // A implementação real será feita pelo PostGIS. Este método serve como um marcador.
        throw new NotImplementedException();
    }

}
