using FirstDecisionDesafioMoises.Models.Classes;
using FirstDecisionDesafioMoises.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstDecisionDesafioMoises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            this.pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaModel>>> BuscarTodasPessoas()
        {
            IEnumerable<PessoaModel> pessoas = await pessoaRepository.BuscarTodasPessoas();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaModel>> BuscarPessoaPorId(int id)
        {
            PessoaModel pessoa = await pessoaRepository.BuscarPorId(id);
            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<PessoaModel>> Adicionar([FromBody] PessoaModel pessoaModel)
        {
            PessoaModel pessoa = await pessoaRepository.Adicionar(pessoaModel);
            return Ok(pessoa);
        }

        [HttpPut]
        public async Task<ActionResult<PessoaModel>> Atualizar([FromBody] PessoaModel pessoaModel, int id)
        {
            pessoaModel.ID = id;
            PessoaModel pessoa = await pessoaRepository.Atualizar(pessoaModel, id);
            return Ok(pessoa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PessoaModel>> Deletar(int id)
        {
            bool apagado = await pessoaRepository.Deletar(id);
            return Ok(apagado);
        }
    }
}