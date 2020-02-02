using FluentValidation;

namespace Business.Models.Validations {
    public class ProdutosValidations : AbstractValidator<Produto> {
        public ProdutosValidations () {
            RuleFor (a => a.Name)
                .NotEmpty ().WithMessage ("O campo {PropertyName} precisa ser fornecido")
                .Length (2, 100).WithMessage ("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght}");

            RuleFor (a => a.Price)
                .GreaterThan (0).WithMessage ("O preco deve ser mairo que 0");

        }
    }
}