namespace Bico.Api.v1.Models;

public class CategoriaDto
{
    public CategoriaDto(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; } 
}
