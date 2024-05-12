using Bico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bico.Domain.Interfaces
{
    public interface IAcessoRepository
    {
        Task<Acesso> AlterarSenha(string email, string senha);

        Task<Acesso> ObterUsuarioPorEmail(string email);
    }
}
