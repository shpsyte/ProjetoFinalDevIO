using System;
using System.ComponentModel.DataAnnotations;
using App.AttributeValidations;
using Microsoft.AspNetCore.Http;

namespace App.ViewModels {
    public class ProdutoViewModel {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength (100, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType (DataType.Currency)]
        [Moeda]
        public decimal Price { get; set; }

        public IFormFile ImageUpload { get; set; }
        public string Image { get; set; }

        public Guid FornecedorId { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }

    }
}