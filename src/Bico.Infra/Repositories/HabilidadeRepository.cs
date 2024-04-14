using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bico.Infra.Repositories
{
    public class HabilidadeRepository : IHabilidadeRepository
    {
        private readonly BicoContext _context;
        public HabilidadeRepository(BicoContext bicoContext) {
            _context = bicoContext;
        }

        public async Task<List<Habilidade>> ListarHabilidades(int idCategoria)
        {
            var habilidades = await _context.Habilidades
                .Include(h => h.Categoria).Where(x => x.CategoriaId == idCategoria)
                .ToListAsync();

            return habilidades;
        }
    }
}
