using FluentValidation;

namespace Business.Models.Validations {
    public class FornecedorValidations : AbstractValidator<Fornecedor> {
        public FornecedorValidations () {

            RuleFor (f => f.Name)
                .NotEmpty ().WithMessage ("O campo {PropertyName} precisa ser fornecido")
                .Length (2, 100).WithMessage ("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght}");

            When (f => f.Document.Length <= 11, () => {
                RuleFor (f => f.Document.Length)
                    .Equal (11)
                    .WithMessage ("O campo precisa ter 11 caracteres");
            });

            When (f => f.Document.Length > 11 && f.Document.Length <= 14, () => {
                RuleFor (f => f.Document.Length)
                    .Equal (14)
                    .WithMessage ("O capo precisa ter 14 caracteres");
            });

        }
    }
}