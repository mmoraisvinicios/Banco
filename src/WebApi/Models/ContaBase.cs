using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Models
{
    public class ContaBase
    {
        public string NumeroConta { get; set; }
        public string NumeroAgencia { get; set; }
        public string CodigoBanco { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
    }


}
