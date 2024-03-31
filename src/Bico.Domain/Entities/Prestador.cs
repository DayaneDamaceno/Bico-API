﻿
using NetTopologySuite.Geometries;

namespace Bico.Domain.Entities;

public class Prestador
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public int RaioDeAlcance { get; set; }
    public Point Localizacao { get; set; }

    public virtual ICollection<Habilidade> Habilidades { get; set; }
}

