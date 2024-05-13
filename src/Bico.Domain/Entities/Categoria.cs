namespace Bico.Domain.Entities;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    //Relacionamento
    public virtual ICollection<Habilidade> Habilidades { get; set; }

}
