using FirstDecisionDesafioMoises.Infraestructure.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FirstDecisionDesafioMoises.Infraestructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CpfCnpjValidoAttribute : ValidationAttribute
    {
        #region Constants
        private const int NUMERO_CARACTERE_CPF = 11;
        #endregion

        #region Properties
        public bool RequiredField { get; set; }
        public override bool RequiresValidationContext => true;
        #endregion

        #region Constructors

        public CpfCnpjValidoAttribute(bool requiredField = false)
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
                return new ValidationResult(this.FormatErrorMessage("Cpf/Cnpj"), new[] { validationContext.DisplayName });

            string cpfCnpj = value as string;

            if (string.IsNullOrWhiteSpace(cpfCnpj))
            {
                if (this.RequiredField)
                    return new ValidationResult(this.FormatErrorMessage("Cpf/Cnpj"), new[] { validationContext.DisplayName });

                return ValidationResult.Success;
            }

            string cpf = this.ObterCpf(cpfCnpj);

            string cnpj = this.ObterCnpj(cpfCnpj);

            bool cpfValido = this.ValidarCpf(cpf);
            bool cnpjValido = this.ValidarCnpj(cnpj);

            if (!cpfValido && !cnpjValido)
                return new ValidationResult(this.FormatErrorMessage("Cpf/Cnpj"), new[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }
        #endregion

        #region PrivateMethods
        public bool ValidarCpf(string cpf)
        {
            if (cpf == null)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.SomenteNumeros();

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        public bool ValidarCnpj(string cnpj)
        {
            if (cnpj == null)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.SomenteNumeros();

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private string ObterCpf(string cpfCnpj)
        {
            string cpfCnpj_SemMascara = cpfCnpj?.SomenteNumeros();

            return cpfCnpj_SemMascara.Length <= NUMERO_CARACTERE_CPF ? cpfCnpj_SemMascara : null;
        }

        private string ObterCnpj(string cpfCnpj)
        {
            string cpfCnpj_SemMascara = cpfCnpj?.SomenteNumeros();

            return cpfCnpj_SemMascara.Length > NUMERO_CARACTERE_CPF ? cpfCnpj_SemMascara : null;
        }
        #endregion
    }
}
