using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Services.Colaboradores;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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
        [SwaggerRequestExample(typeof(ColaboradoresCreateDTO), typeof(ColaboradoresExampleDTO))]
        public async Task<IActionResult> AdicaoColaboradores(ColaboradoresCreateDTO colaborador)
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
