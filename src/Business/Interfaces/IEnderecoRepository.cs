using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces {
    public interface IEnderecoRepository : IRepository<Endereco> {
        Task<Endereco> PegarEnderecoFornecedor (Guid fornecedorId);

    }
}