using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bico.Domain.Entities;

public class FotoServico
{
    public int Id { get; set; }
    public string Foto { get; set; }
    public int PrestadorId { get; set; }
}
