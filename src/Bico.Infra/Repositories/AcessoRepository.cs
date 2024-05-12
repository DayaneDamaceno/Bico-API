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
    public class AcessoRepository : IAcessoRepository
    {
        private readonly BicoContext _context;

        public AcessoRepository(BicoContext bicoContext)
        {
            _context = bicoContext;
        }

        public async Task<Acesso> AlterarSenha(string email, string senha)
        {
            var credencial = await ObterUsuarioPorEmail(email);

            if (credencial != null)
            {

                credencial.Senha = senha;

                _context.Acessos.Update(credencial);

                await _context.SaveChangesAsync();

                return credencial;

            }

            return null;
        }

        public async Task<Acesso> ObterUsuarioPorEmail(string email)
        {
             var acesso = await _context.Acessos
                .Where(a => a.Email == email)
                .FirstOrDefaultAsync();


            return acesso;
        }
    }
}