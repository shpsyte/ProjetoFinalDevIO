using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository {
        public EnderecoRepository (MeuDbContext context) : base (context) { }

        public async Task<Endereco> PegarEnderecoFornecedor (Guid fornecedorId) {
            return await _set.Where (a => a.FornecedorId == fornecedorId).FirstOrDefaultAsync ();
        }
    }
}