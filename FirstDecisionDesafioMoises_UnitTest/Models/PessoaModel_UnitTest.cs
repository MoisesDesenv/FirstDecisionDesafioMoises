using FirstDecisionDesafioMoises.Infraestructure.Validators.Interfaces;
using FirstDecisionDesafioMoises.Models.Classes;
using System.Text.RegularExpressions;

namespace FirstDecisionDesafioMoises_UnitTest.Models
{
    [TestFixture]
    public class PessoaModel_UnitTest
    {

        private PessoaModel pessoa_dummy = new PessoaModel()
        {
            ID = 1,
            Nome = "Moisés Messias",
            Sobrenome = "Siqueira da Silva",
            Telefone = "67984055290",
            CEP = "79645122",
            Cidade = "Três Lagoas",
            CPFCNPJ = "028.753.451-09",
            DataNascimento = new DateTime(1988, 06, 18, 09, 00, 00),
            Email = "moises_3L@hotmail.com",
            Endereco = "R: Maria Cândida Costa Lopes",
            Estado = "Mato Grosso do Sul",
        };

        #region Teste de Email
        [TestCase]
        public void TestarEmailNull()
        {
            //Arrange
            pessoa_dummy.Email = null;

            //Act
            pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field E-mail is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarEmailVazio()
        {
            //Arrange
            pessoa_dummy.Email = string.Empty;

            //Act
            pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field E-mail is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarEmailIncorreto_PalavraAleatoria()
        {
            //Arrange
            pessoa_dummy.Email = "emailIncorreto";

            //Act
            pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field E-mail is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarEmailIncorreto_SemPonto()
        {
            //Arrange
            pessoa_dummy.Email = "email@incorreto";

            //Act
            pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field E-mail is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarEmailIncorreto_CaracterEspecial()
        {
            //Arrange
            pessoa_dummy.Email = "texto¨$#!¨%@caracterEspecial.com";

            //Act
            pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field E-mail is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarEmailCORRETO()
        {
            //Arrange
            pessoa_dummy.Email = "moises_3L@hotmail.com";

            //Act
            pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field E-mail is invalid.") ?? false;

            //Assert
            //Por não ser um campo obrigatório, o correto é não encontrar erro
            Assert.IsFalse(erroEncontrado);
        }
        #endregion

        #region Teste de Cpf Cnpj
        [TestCase]
        public void TestarCpfCnpjNull()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = null;

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            //Por não ser um campo obrigatório, o correto é não encontrar erro
            Assert.IsFalse(erroEncontrado);
        }

        [TestCase]
        public void TestarCpfCnpjVazio()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = string.Empty;

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            Assert.IsFalse(erroEncontrado);
        }

        [TestCase]
        public void TestarCpfCnpjValorIncorreto()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = "123";

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarCpfCnpj_CpfInvalido()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = "47159836457";

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            Assert.IsFalse(erroEncontrado);
        }

        [TestCase]
        public void TestarCpfCnpj_CnpjInvalido()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = "47815369478152";

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            Assert.IsTrue(erroEncontrado);
        }

        [TestCase]
        public void TestarCpfCnpj_CpfValido()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = "02875345109";

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            Assert.IsFalse(erroEncontrado);
        }

        [TestCase]
        public void TestarCpfCnpj_CnpjValido()
        {
            //Arrange
            pessoa_dummy.CPFCNPJ = "23479562000165";

            //Act
            var isValid = pessoa_dummy.IsValid(out ICollection<IModelValidationResult> result);
            var erroEncontrado = result?.Any(x => x.ErrorMessage == "The field Cpf/Cnpj is invalid.") ?? false;

            //Assert
            Assert.IsFalse(erroEncontrado);
        }
        #endregion

        //TODO: Testes das outras propriedades da class (PessoaModel) -> Ler README
    }
}