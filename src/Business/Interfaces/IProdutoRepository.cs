using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces {
    public interface IProdutoRepository : IRepository<Produto> {
        Task<IEnumerable<Produto>> PegarTodosProdutos ();
        Task<Produto> PegarProdutoPorIdValido (Guid id);
        Task<IEnumerable<Produto>> PegarTodosProdutosPorFornecedor (Guid fornecedorId);

    }
}