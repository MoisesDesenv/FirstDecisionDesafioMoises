﻿using FirstDecisionDesafioMoises.Infraestructure.Attributes;
using FirstDecisionDesafioMoises.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstDecisionDesafioMoises.Models.Classes
{
    public class PessoaModel : ModelBase
    {
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
        public string Telefone { get; set; }
        
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
        public string CpfCnpj { get; set; }
    }
}