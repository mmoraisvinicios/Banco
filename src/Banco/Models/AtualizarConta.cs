using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Models
{
    public class AtualizarConta : ContaBase
    {
        public int ContaId { get; set; }
        public DateTime? DataAbertura { get; set; }
    }
}
