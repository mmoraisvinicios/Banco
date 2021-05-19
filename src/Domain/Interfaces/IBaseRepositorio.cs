using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IBaseRepositorio<TEntity> where TEntity : BaseEntity
    {
        void Inserir(TEntity obj);

        void Atualizar(TEntity obj);

        void Remover(int id);

        IList<TEntity> Buscar();

        TEntity BuscarPorId(int id);
    }
}
