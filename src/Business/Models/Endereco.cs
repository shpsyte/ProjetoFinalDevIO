using System;

namespace Business.Models {
    public class Endereco : Entity {
        public string Street { get; set; }
        public string PostCode { get; set; }

        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

    }
}