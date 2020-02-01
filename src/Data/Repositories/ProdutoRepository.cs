using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository {
        public ProdutoRepository (MeuDbContext context, DbSet<Produto> set) : base (context, set) { }

        public async Task<Produto> PegarProdutoPorIdValido (Guid id) {
            return await _set.Where (a => a.Price > 0 && a.Id == id).FirstOrDefaultAsync ();
        }

        public async Task<IEnumerable<Produto>> PegarTodosProdutos () {
            return await _set.AsNoTracking ().Include (a => a.Fornecedor).ToListAsync ();
        }

        public async Task<IEnumerable<Produto>> PegarTodosProdutosPorFornecedor (Guid fornecedorId) {
            return await _set.Where (a => a.FornecedorId == fornecedorId).ToListAsync ();
        }
    }
}