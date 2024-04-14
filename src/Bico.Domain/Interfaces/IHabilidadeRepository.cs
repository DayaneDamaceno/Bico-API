using Bico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bico.Domain.Interfaces
{
    public interface IHabilidadeRepository
    {
        Task<List<Habilidade>> ListarHabilidades(int idCategoria);
    }
}
