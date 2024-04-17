using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs
{
    public class HabilidadeDto
    {
        public HabilidadeDto(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

        public HabilidadeDto(Habilidade habilidade)
        {
            Nome = habilidade.Nome;
        }
    }
}
