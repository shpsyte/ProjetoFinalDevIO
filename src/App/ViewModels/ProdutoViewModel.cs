using System;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels {
    public class ProdutoViewModel {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength (100, ErrorMessage = "", MinimumLength = 3)]
        public string Name { get; set; }

        [DataType (DataType.Currency)]
        public decimal Price { get; set; }

        public string Image { get; set; }

        public Guid FornecedorId { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }

    }
}