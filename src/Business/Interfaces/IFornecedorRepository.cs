using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces {
    public interface IFornecedorRepository : IRepository<Fornecedor> {
        Task<IEnumerable<Fornecedor>> PegarTodosFornecedores ();
        Task<Fornecedor> PegarFornecedorValido (Guid id);

    }
}