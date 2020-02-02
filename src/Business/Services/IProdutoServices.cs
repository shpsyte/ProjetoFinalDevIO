using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services {
    public interface IProdutoServices : IDisposable {
        Task adicionar (Produto produto);
        Task atualizar (Produto produto);
        Task remover (Produto produto);
    }
}