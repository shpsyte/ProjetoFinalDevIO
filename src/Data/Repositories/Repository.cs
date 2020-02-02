using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public abstract class Repository<T> : IRepository<T> where T : Entity {

        protected readonly MeuDbContext _context;
        protected readonly DbSet<T> _set;

        protected Repository (MeuDbContext context) {

            _context = context;
            _set = _context.Set<T> ();
        }

        public async Task Adicionar (T entity) {
            _set.Add (entity);
            await SaveChanges ();
        }

        public async Task Atualizar (T entity) {
            _set.Update (entity);
            await SaveChanges ();
        }

        public void Dispose () {
            _context?.Dispose ();
        }

        public async Task<T> ObterPorId (Guid id) {
            return await _set.FindAsync (id);
        }

        public async Task<IEnumerable<T>> ObterTodos () {
            return await _set.ToListAsync ();
        }

        public async Task<IEnumerable<T>> Query (Expression<Func<T, bool>> where) {
            return await _set.AsNoTracking ().Where (where).ToListAsync ();
        }

        public async Task Remover (T entity) {
            _set.Remove (entity);
            await SaveChanges ();

        }

        public async Task<int> SaveChanges () {
            return await _context.SaveChangesAsync ();
        }
    }
}