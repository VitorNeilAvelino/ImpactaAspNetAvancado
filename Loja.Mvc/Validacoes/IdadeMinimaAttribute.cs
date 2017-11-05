using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Loja.Mvc.Validacoes
{
    [AttributeUsage(AttributeTargets.Property /*| AttributeTargets.Method*/)]
    public class IdadeMinimaAttribute : ValidationAttribute, IClientValidatable
    {
        private int _idadeMinima;
        private string _mensagemErro;

        public IdadeMinimaAttribute(int idadeMinima)
        {
            _idadeMinima = idadeMinima;
            _mensagemErro = $"A idade mínima é de {_idadeMinima} anos.";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var regra = new ModelClientValidationRule
            {
                //ErrorMessage = FormatErrorMessage(metadata.),
                //ErrorMessage = base.ErrorMessage,
                //ErrorMessage = $"Idade mínima: {_idadeMinima} anos.",
                ErrorMessage = _mensagemErro,
                ValidationType = "regraidademinima"
            };

            regra.ValidationParameters.Add("valoridademinima", _idadeMinima);

            yield return regra;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataNascimento = (DateTime)value;

            if (dataNascimento.AddYears(_idadeMinima) > DateTime.Now)
            {
                //base.ErrorMessage = $"Idade mínima: {_idadeMinima} anos.";
                return new ValidationResult(_mensagemErro);
            }

            return ValidationResult.Success;
        }
    }
}