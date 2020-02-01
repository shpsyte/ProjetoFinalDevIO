using System;

namespace Business.Models {
    public class Produto : Entity {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Image { get; set; }

        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

    }
}