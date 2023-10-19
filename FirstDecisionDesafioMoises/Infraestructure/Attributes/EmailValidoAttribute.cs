using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FirstDecisionDesafioMoises.Infraestructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EmailValidoAttribute : ValidationAttribute
    {
        #region Constants
        private const int NUMERO_CARACTERE_CPF = 11;
        private const int NUMERO_CARACTERE_CNPJ = 14;
        #endregion

        #region Properties
        public bool RequiredField { get; set; }
        public override bool RequiresValidationContext => true;
        #endregion

        #region Constructors

        public EmailValidoAttribute(bool requiredField = false)
            : base()
        {
            this.RequiredField = requiredField;
        }

        #endregion

        #region Validations
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            if (this.RequiredField && value == null)
                return new ValidationResult(this.FormatErrorMessage("E-mail é um campo obrigatório"), new[] { validationContext.DisplayName });

            if (value is not string email)
                throw new ApplicationException("Tipo de dados do e-mail inesperado. O tipo correto é string");

            if (this.RequiredField && string.IsNullOrWhiteSpace(email))
                return new ValidationResult(this.FormatErrorMessage("Cpf/Cnpj é um campo obrigatório"), new[] { validationContext.DisplayName });

            if (!this.EmailIsValid(email))
                return new ValidationResult(this.FormatErrorMessage("E-mail inválido"), new[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }
        #endregion

        #region PrivateMethods
        private bool EmailIsValid(string email)
        {
            var regex = "/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
