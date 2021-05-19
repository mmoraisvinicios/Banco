using CrossCutting.Util;
using Domain.Entities;
using FluentValidation;

namespace Banco.Validacao
{
    public class NovaContaValidation : AbstractValidator<Conta>
    {
        public NovaContaValidation()
        {

            RuleFor(c => c.Nome)
                   .NotNull().WithMessage("Nome é obrigatório")
                   .NotEmpty().WithMessage("Nome é obrigatório")
                   .MinimumLength(3).WithMessage("Digite um nome válido")
                   .MaximumLength(100).WithMessage("Digite no maxímo 100 caracteres");


            RuleFor(c => c.Documento)
                .Must(c => ValidacaoDocumento.IsValid(c)).WithMessage("Documento inválido")
                .NotNull().WithMessage("Documento é obrigatório")
                .NotEmpty().WithMessage("Documento é obrigatório");


            RuleFor(c => c.NumeroAgencia)
                .NotNull().WithMessage("Número da Agênciá é obrigatório")
                .NotEmpty().WithMessage("Número da Agênciá  é obrigatório");


            RuleFor(c => c.NumeroConta)
                .NotNull().WithMessage("Número da conta é obrigatório")
                .NotEmpty().WithMessage("Número da conta é obrigatório");
        }
    }
}
