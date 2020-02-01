using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels {
    public class FornecedorViewModel {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength (100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength (100, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength (20, MinimumLength = 1)]
        public string Document { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public EnderecoViewModel Endereco { get; set; }
    }
}