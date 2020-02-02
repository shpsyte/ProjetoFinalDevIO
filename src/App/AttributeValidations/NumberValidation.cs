using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace App.AttributeValidations {

    public class MoedaAttribute : ValidationAttribute {

        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            try {
                var moeda = Convert.ToDecimal (value, new CultureInfo ("pt-BR"));
            } catch (System.Exception) {

                return new ValidationResult ("Moeda não é valida");

            }

            return ValidationResult.Success;
        }
    }

    public class MoedaAtributeAdapter : AttributeAdapterBase<MoedaAttribute> {
        public MoedaAtributeAdapter (MoedaAttribute attribute, IStringLocalizer stringLocalizer) : base (attribute, stringLocalizer) { }

        public override void AddValidation (ClientModelValidationContext context) {

            MergeAttribute (context.Attributes, "data-val", "true");
            MergeAttribute (context.Attributes, "data-val-moeda", GetErrorMessage (context));
            MergeAttribute (context.Attributes, "data-val-number", GetErrorMessage (context));

        }

        public override string GetErrorMessage (ModelValidationContextBase validationContext) {
            return "Modea inválida";
        }
    }

    public class MoedaValidationAdapterProvider : IValidationAttributeAdapterProvider {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider ();
        public IAttributeAdapter GetAttributeAdapter (ValidationAttribute attribute, IStringLocalizer stringLocalizer) {

            if (attribute is MoedaAttribute moedaAttribute) {
                return new MoedaAtributeAdapter (moedaAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter (attribute, stringLocalizer);

        }
    }

}