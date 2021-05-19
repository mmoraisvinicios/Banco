using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Repository
{
    public class BaseRepository<TEntity> : IBaseRepositorio<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataBaseContext _mySqlContext;

        public BaseRepository(DataBaseContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public void Remover(int id)
        {    
            _mySqlContext.Set<TEntity>().Remove(BuscarPorId(id));
            _mySqlContext.SaveChanges();
        }

        public void Inserir(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        public IList<TEntity> Buscar()
        { 
            return _mySqlContext.Set<TEntity>().ToList();

        }

        public TEntity BuscarPorId(int id)
        {
            return _mySqlContext.Set<TEntity>().Find(id);
        }

        public void Atualizar(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }
    }
}
