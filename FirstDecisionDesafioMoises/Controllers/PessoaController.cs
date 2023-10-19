using FirstDecisionDesafioMoises.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FirstDecisionDesafioMoises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PessoaModel>> BuscarTodasPessoas()
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<PessoaModel> BuscarPessoaPorId(int id)
        {
            return Ok(null);
        }
    }
}
