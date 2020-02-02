using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository {
        public FornecedorRepository (MeuDbContext context) : base (context) { }

        public async Task<Fornecedor> PegarFornecedorValido (Guid id) {
            return await _set.Include (a => a.Produtos).Include (a => a.Endereco)
                .Where (a => a.Id == id).FirstOrDefaultAsync ();
        }

        public async Task<IEnumerable<Fornecedor>> PegarTodosFornecedores () {
            return await _set.AsNoTracking ().Include (a => a.Endereco).Include (a => a.Produtos).ToListAsync ();
        }
    }
}