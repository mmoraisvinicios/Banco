using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IBaseServico<TEntity> where TEntity : BaseEntity
    {
        TOutputModel Adicionar<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        void Remover(int id);

        IEnumerable<TOutputModel> Buscar<TOutputModel>() where TOutputModel : class;

        TOutputModel BuscarPorId<TOutputModel>(int id) where TOutputModel : class;

        TOutputModel Atualizar<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
    }
}
