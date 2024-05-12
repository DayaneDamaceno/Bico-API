
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bico.Domain.Entities;

public class Prestador : Usuario
{
    public int RaioDeAlcance { get; set; }

    public virtual List<Habilidade> Habilidades { get; set; }
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
    public virtual List<FotoServico> FotosServico {  get; set; }

    [NotMapped]
    public double MediaEstrelas { get; set; }
}


