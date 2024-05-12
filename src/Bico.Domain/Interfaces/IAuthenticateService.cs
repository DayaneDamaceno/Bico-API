using Bico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bico.Domain.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> Authenticate(string email, string senha);
        public string GenerateToken(Acesso acesso);
    }
}
