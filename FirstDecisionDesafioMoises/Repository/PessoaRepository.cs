using FirstDecisionDesafioMoises.Data;
using FirstDecisionDesafioMoises.Models.Classes;
using FirstDecisionDesafioMoises.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstDecisionDesafioMoises.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        #region Fields
        private readonly TasksDBContext dbContext;
        #endregion

        #region Constructors
        public PessoaRepository(TasksDBContext tasksCBContext)
        {
            this.dbContext = tasksCBContext;
        }
        #endregion

        #region PublicMethods
        public async Task<PessoaModel> Adicionar(PessoaModel pessoa)
        {
            await this.dbContext.AddAsync(pessoa);
            await this.dbContext.SaveChangesAsync();

            return pessoa;
        }

        public async Task<bool> Apagar(int id)
        {
            PessoaModel pessoaPorId = await BuscarPorId(id);

            if (pessoaPorId == null)
                throw new ApplicationException($"Pessoa para o ID: { id } não foi encontrada na base de dados.");

            this.dbContext.Pessoas.Remove(pessoaPorId);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<PessoaModel> Atualizar(PessoaModel pessoa, int id)
        {
            PessoaModel pessoaPorId = await BuscarPorId(id);

            if(pessoaPorId == null)
                throw new ApplicationException($"Pessoa para o ID: { id } não foi encontrada na base de dados.");

            pessoaPorId.Nome = pessoa.Nome;
            pessoaPorId.Sobrenome = pessoa.Sobrenome;
            pessoaPorId.Telefone = pessoa.Telefone;
            pessoaPorId.CEP = pessoa.CEP;
            pessoaPorId.Cidade = pessoa.Cidade;
            pessoaPorId.CpfCnpj = pessoa.CpfCnpj;
            pessoaPorId.DataNascimento = pessoa.DataNascimento;
            pessoaPorId.Email = pessoa.Email;
            pessoaPorId.Endereco = pessoa.Endereco;
            pessoaPorId.Estado = pessoa.Estado;

            this.dbContext.Pessoas.Update(pessoaPorId);
            this.dbContext.SaveChanges();

            return pessoaPorId;
        }

        public async Task<PessoaModel> BuscarPorId(int id)
            => await this.dbContext.Pessoas.FirstOrDefaultAsync(x => x.ID == id);

        public async Task<IEnumerable<PessoaModel>> BuscarTodasPessoas()
            => await this.dbContext.Pessoas.ToListAsync();
        #endregion
    }
}