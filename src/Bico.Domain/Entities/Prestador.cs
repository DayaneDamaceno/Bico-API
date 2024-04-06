
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bico.Domain.Entities;

public class Prestador
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarFileName { get; set; }
    public int RaioDeAlcance { get; set; }
    public Point Localizacao { get; set; }

    public virtual ICollection<Habilidade> Habilidades { get; set; }

    [NotMapped]
    public string AvatarUrl { get; set; }
}


