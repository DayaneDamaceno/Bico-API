using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bico.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarFileName { get; set; }
    public Point Localizacao { get; set; }

    [NotMapped]
    public string AvatarUrl { get; set; }
}
