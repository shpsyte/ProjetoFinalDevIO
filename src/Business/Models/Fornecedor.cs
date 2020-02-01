using System.Collections.Generic;

namespace Business.Models {
    public class Fornecedor : Entity {

        public Fornecedor () {
            this.Produtos = new HashSet<Produto> ();
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
        public Endereco Endereco { get; set; }

    }
}