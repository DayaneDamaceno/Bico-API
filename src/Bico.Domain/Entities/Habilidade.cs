namespace Bico.Domain.Entities;

public class Habilidade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; }

    public virtual ICollection<Prestador> Prestadores { get; set; }
}