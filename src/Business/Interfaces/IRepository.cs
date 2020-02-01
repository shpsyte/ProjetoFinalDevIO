using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces {
    public interface IRepository<T> : IDisposable where T : Entity {
        Task Adicionar (T entity);
        Task<T> ObterPorId (Guid id);
        Task<IEnumerable<T>> ObterTodos ();

        Task<IEnumerable<T>> Query (Expression<Func<T, bool>> where);
        Task Atualizar (T entity);
        Task Remover (T entity);
        Task<int> SaveChanges ();

    }
}