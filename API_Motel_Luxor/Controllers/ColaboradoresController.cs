using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Services.Colaboradores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;

namespace API_Motel_Luxor.Controllers
{
    
    [ApiController]
    [Route("Colaboradores")]
    public class ColaboradoresController : ControllerBase
    {
        // chamando interface com os metodos de requisicao
        private readonly IColaboradoresRepository _repository;

        // loger
        private readonly ILogger<ColaboradoresController> _logger;

        public ColaboradoresController(IColaboradoresRepository repository, ILogger<ColaboradoresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("adicionarColaborador/")]
        [SwaggerRequestExample(typeof(ColaboradoresCreateDTO), typeof(ColaboradoresExampleDTO))]
        public async Task<IActionResult> AdicaoColaboradores(ColaboradoresCreateDTO colaborador)
        {
            _logger.LogInformation("Recebida requisição para criar cadastro de colaborador {Nome}", colaborador.Nome);
            var respostaRequisicao = await _repository.AdicaoColaborador(colaborador);


            if (respostaRequisicao.Mensagem == "Email já cadastrado por outro colaborador" ||
                respostaRequisicao.Mensagem == "Cpf já cadastrado por outro colaborador" ||
                respostaRequisicao.Mensagem == "Telefone já cadastrado por outro colaborador")
            {
                _logger.LogWarning("Dados Duplicados: {Mensagem}", respostaRequisicao.Mensagem);
                return Conflict(respostaRequisicao.Mensagem);
            }


            if (respostaRequisicao.Mensagem == "Erro interno na solicitação de cadastro de colaborador")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }


            _logger.LogInformation("Colaborador  {Nome} cadastrado com sucesso", respostaRequisicao.Dados.Nome);
            return Created(string.Empty,respostaRequisicao);
        }



        [HttpGet]
        [Route("buscarColaborador/{id:int}")]
        public async Task<IActionResult> BuscarColaborador(int id)
        {
            _logger.LogInformation("Recebida requisição para buscar colaborador com ID {id}", id);
            var respostaRequisicao = await _repository.BuscarColaborador(id);

            // colaborador nao encontrado
            if (respostaRequisicao.Mensagem == "Colaborador não encontrado")
            {
                _logger.LogWarning("Colaborador com ID {id} não encontrado", id);
                return NotFound(respostaRequisicao.Mensagem);
            }

            if (respostaRequisicao.Mensagem == "Erro interno na solicitação")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }

            _logger.LogInformation("Colaborador com ID {id} encontrado com sucesso", id);
            return Ok(respostaRequisicao);
        }


        [HttpGet]
        [Route("listarColaboradores/")]
        public async Task<IActionResult> listarColaboradores()
        {
            _logger.LogInformation("Recebida requisição para listar colaboradores");
            var respostaRequisicao = await _repository.ListarColaboradores();

            
            if(respostaRequisicao.Mensagem == "Nenhum colaborador cadastrado")
            {
                _logger.LogWarning("Nenhum colaborador cadastrado");
                return NotFound(respostaRequisicao.Mensagem);
            }


            if (respostaRequisicao.Mensagem == "Erro na solicitação de listagem de colaboradores")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }

            _logger.LogInformation("Listagem de colaboradores concluida com sucesso");
            return Ok(respostaRequisicao);
        }


        [HttpPatch]
        [Route("desabilitarColaborador/{id:int}")]
        public async Task<IActionResult> DesabilitarColaborador(int id)
        {
            _logger.LogInformation("Recebida requisição para desabilitar colaborador com ID {id}", id);
            var respostaRequisicao = await _repository.DesabilitarColaborador(id);
         

            if (respostaRequisicao.Mensagem == "Colaborador não encontrado")
            {
                _logger.LogWarning("Erro ao desabilitar colaborador: {Mensagem}", respostaRequisicao.Mensagem);
                return NotFound(respostaRequisicao.Mensagem);
            }

            if(respostaRequisicao.Mensagem == "cadastro do colaborador já está inabilitado")
            {
                _logger.LogWarning("Erro ao desabilitar colaborador: {Mensagem}", respostaRequisicao.Mensagem);
                return BadRequest(respostaRequisicao.Mensagem);
            }


            if (respostaRequisicao.Mensagem == "Erro interno na solicitação")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }

            _logger.LogInformation("Colaborador com ID {id} desabilitado com sucesso", id);
            return Ok(respostaRequisicao);
        }



        [HttpPatch]
        [Route("habilitarColaborador/{id:int}")]
        public async Task<IActionResult> HabilitarColaborador(int id)
        {
            _logger.LogInformation("Recebida requisição para habilitar colaborador com ID {id}", id);
            var respostaRequisicao = await _repository.HabilitarColaborador(id);

            if (respostaRequisicao.Mensagem == "Colaborador não encontrado")
            {
                _logger.LogWarning("Erro ao habilitar colaborador: {Mensagem}", respostaRequisicao.Mensagem);
                return NotFound(respostaRequisicao.Mensagem);
            }

            if (respostaRequisicao.Mensagem == "cadastro do colaborador já está habilitado")
            {
                _logger.LogWarning("Erro ao habilitar colaborador: {Mensagem}", respostaRequisicao.Mensagem);
                return BadRequest(respostaRequisicao.Mensagem);
            }

            if (respostaRequisicao.Mensagem == "Erro interno na solicitação")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }

            _logger.LogInformation("Colaborador com ID {id} habilitado com sucesso", id);
            return Ok(respostaRequisicao);
        }



        [HttpPut]
        [Route("atualizarColaborador/")]
        [SwaggerRequestExample(typeof(ColaboradorUpdateDTO), typeof(ColaboradoresExampleUpdateDTO))]
        public async Task<IActionResult> AtualizarColaborador(ColaboradorUpdateDTO colaborador)
        {
            var respostaRequisicao = await _repository.AtualizarColaborador(colaborador);

            // colaborador nao encontrado
            if (respostaRequisicao.Mensagem == "Colaborador não encontrado")
            {
                return NotFound(respostaRequisicao.Mensagem);
            }

            // dados duplicados
            if (respostaRequisicao.Mensagem == "Email já cadastrado por outro colaborador" ||
                respostaRequisicao.Mensagem == "Cpf já cadastrado por outro colaborador" ||
                respostaRequisicao.Mensagem == "Telefone já cadastrado por outro colaborador")
            {
                return Conflict(respostaRequisicao.Mensagem);
            }


            // erro interno na requisição
            if (respostaRequisicao.Mensagem == "Erro interno na solicitação de autalizar dados do colaborador")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }

            return Ok(respostaRequisicao);
        }



        
        [HttpDelete]
        [Route("deletarColaborador/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Recebida requisição para deletar dados de um colaborador com ID {id}", id);
            var respostaRequisicao = await _repository.DeletarColaborador(id);

            // colaborador nao encontrado
            if (respostaRequisicao.Mensagem == "Colaborador não encontrado")
            {
                _logger.LogWarning("Erro ao deletar dados do colaborador: {id}", id);
                return NotFound(respostaRequisicao.Mensagem);
            }

            // erro interno na requisição
            if (respostaRequisicao.Mensagem == "Erro interno na solicitação para deletar dados do colaborador")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, respostaRequisicao.Mensagem);
            }

            _logger.LogInformation("{Mensagem}", respostaRequisicao.Mensagem);
            return NoContent();
        }
    }
}
