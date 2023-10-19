using FirstDecisionDesafioMoises.Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstDecisionDesafioMoises.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<PessoaModel>> BuscarTodasPessoas();
        Task<PessoaModel> BuscarPorId(int id);
        Task<PessoaModel> Adicionar(PessoaModel pessoa);
        Task<PessoaModel> Atualizar(PessoaModel pessoa, int id);
        Task<bool> Apagar(int id);
    }
}