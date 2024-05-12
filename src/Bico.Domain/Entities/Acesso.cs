using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bico.Domain.Entities
{
    public class Acesso
    {
        public int Id { get; set; }
        public string Email{ get; set; }
        public string Senha { get; set; }
    }
}
