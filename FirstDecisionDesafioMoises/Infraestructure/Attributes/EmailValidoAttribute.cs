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
        public override bool RequiresValidationContext => true;
        #endregion

        #region Constructors

        public EmailValidoAttribute()
            : base() { }

        #endregion

        #region Validations
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            string email = value as string;

            if (!this.EmailIsValid(email))
                return new ValidationResult(this.FormatErrorMessage("E-mail"), new[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }
        #endregion

        #region PrivateMethods
        private bool EmailIsValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email ?? string.Empty);
        }
        #endregion
    }
}
