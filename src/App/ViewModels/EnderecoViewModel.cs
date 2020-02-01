using System;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels {
    public class EnderecoViewModel {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength (100, MinimumLength = 1)]
        public string Street { get; set; }

        [Required]
        [StringLength (10, MinimumLength = 1)]
        public string PostCode { get; set; }

        public Guid FornecedorId { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
    }
}