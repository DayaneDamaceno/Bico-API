namespace Bico.Api.v1.Models;

public class Categoria
{
    public Categoria(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
}
