using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Services.Colaboradores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Motel_Luxor.Controllers
{
    
    [ApiController]
    [Route("ApiLuxor/")]
    public class ColaboradoresController : ControllerBase
    {
        // chamando interface com os metodos de requisicao
        private readonly IColaboradoresRepository _repository;
        public ColaboradoresController(IColaboradoresRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        [Route("adicionarColaborador/")]
        public async Task<IActionResult> AdicaoColaboradores([FromForm]ColaboradoresCreateDTO colaborador)
        {
            var respostaRequisicao = await _repository.AdicaoColaborador(colaborador);

            if(respostaRequisicao.Mensagem != "Dados Adicionandos com Sucesso")
            {
                return BadRequest(respostaRequisicao.Mensagem);
            }

            return Ok(respostaRequisicao);
        }



        [HttpGet]
        [Route("listagemColaboradores/")]
        public IActionResult ListagemColaboradores()
        {
            return Ok();
        }
    }
}
