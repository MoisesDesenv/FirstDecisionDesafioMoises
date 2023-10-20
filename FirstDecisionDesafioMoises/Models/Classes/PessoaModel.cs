using FirstDecisionDesafioMoises.Infraestructure.Attributes;
using FirstDecisionDesafioMoises.Infraestructure.Extensions;
using FirstDecisionDesafioMoises.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDecisionDesafioMoises.Models.Classes
{
    [Table("Pessoa")]
    public class PessoaModel : ModelBase
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Nome { get; set; }
        
        [MaxLength(255)]
        public string Sobrenome { get; set; }
        
        public DateTime? DataNascimento { get; set; }
        
        [MaxLength(255)]
        [EmailValido]
        public string Email { get; set; }
        
        [MaxLength(20)]
        public string Telefone
        {
            get
            {
                return this.telefone;
            }
            set
            {
                this.telefone = value.SomenteNumeros();
            }
        } private string telefone { get; set; }
        
        [MaxLength(255)]
        public string Endereco { get; set; }
        
        [MaxLength(100)]
        public string Cidade { get; set; }
        
        [MaxLength(50)]
        public string Estado { get; set; }
        
        [MaxLength(10)]
        public string CEP { get; set; }
        
        [MaxLength(14)]
        [CpfCnpjValido]
        public string CPFCNPJ { get; set; }
    }
}