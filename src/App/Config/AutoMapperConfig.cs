using App.ViewModels;
using AutoMapper;
using Business.Models;

namespace App.Config {
    public class AutoMapperConfig : Profile {
        public AutoMapperConfig () {
            CreateMap<Fornecedor, FornecedorViewModel> ().ReverseMap ();
            CreateMap<Endereco, EnderecoViewModel> ().ReverseMap ();
            CreateMap<Produto, ProdutoViewModel> ().ReverseMap ();
        }

    }
}