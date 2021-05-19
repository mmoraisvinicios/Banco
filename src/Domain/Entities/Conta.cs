using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Conta : BaseEntity
    {
        public string NumeroConta { get; set; }
        
        public string NumeroAgencia { get; set; }
        public string CodigoBanco { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
        public DateTime? DataAbertura { get; set; }
        public bool Situacao { get; set; }
    }
}
