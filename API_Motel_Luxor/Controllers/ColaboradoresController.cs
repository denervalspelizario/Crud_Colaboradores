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
        [Route("buscarColaborador/")]
        public async Task<IActionResult> BuscarColaborador(int id)
        {
            var respostaRequisicao = await _repository.BuscarColaborador(id);
            return Ok(respostaRequisicao);
        }


        [HttpGet]
        [Route("listarColaboradores/")]
        public async Task<IActionResult> listarColaboradores()
        {
            var respostaRequisicao = await _repository.ListarColaboradores();
            return Ok(respostaRequisicao);
        }

        [HttpPatch]
        [Route("desabilitarColaborador/{id:int}")]
        public async Task<IActionResult> DesabilitarColaborador(int id)
        {

            var respostaRequisicao = await _repository.DesabilitarColaborador(id);

            if (respostaRequisicao.Mensagem != "Cadastro do colaborador desativado com sucesso")
            {
                return BadRequest(respostaRequisicao.Mensagem);
            }

            return Ok(respostaRequisicao);
        }


        [HttpPatch]
        [Route("habilitarColaborador/{id:int}")]
        public async Task<IActionResult> HabilitarColaborador(int id)
        {
            var respostaRequisicao = await _repository.HabilitarColaborador(id);

            if (respostaRequisicao.Mensagem != "Cadastro do colaborador ativado com sucesso")
            {
                return BadRequest(respostaRequisicao.Mensagem);
            }

            return Ok(respostaRequisicao);
        }

    }
}
