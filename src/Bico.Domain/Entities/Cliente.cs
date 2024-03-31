using NetTopologySuite.Geometries;

namespace Bico.Domain.Entities;

public class Cliente
{

    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public Point Localizacao { get; set; }
}
