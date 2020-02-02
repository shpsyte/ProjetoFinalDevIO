using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services {
    public interface IFornecedorServices : IDisposable {
        Task adicionar (Fornecedor fornecedor);
        Task atualizar (Fornecedor fornecedor);
        Task remover (Fornecedor fornecedor);

    }
}