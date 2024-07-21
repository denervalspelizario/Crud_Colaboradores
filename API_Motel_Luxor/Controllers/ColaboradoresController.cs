using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Services.Colaboradores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;

namespace API_Motel_Luxor.Controllers
{
    
    [ApiController]
    [Route("ApiLuxor/")]
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


            if (respostaRequisicao.Mensagem != "Dados Adicionandos com Sucesso")
            {
                return BadRequest(respostaRequisicao.Mensagem);
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
                return BadRequest(respostaRequisicao.Mensagem);
            }


            _logger.LogInformation("Listagem de colaboradores concluida com sucesso");
            return Ok(respostaRequisicao);
        }


        [HttpPatch]
        [Route("desabilitarColaborador/{id:int}")]
        public async Task<IActionResult> DesabilitarColaborador(int id)
        {

            var respostaRequisicao = await _repository.DesabilitarColaborador(id);
            _logger.LogInformation("Recebida requisição para desabilitar colaborador com ID {id}", id);

            if (respostaRequisicao.Mensagem != "Cadastro do colaborador desativado com sucesso")
            {
                _logger.LogWarning("Colaborador com ID {id} não encontrado", id);
                return BadRequest(respostaRequisicao.Mensagem);
            }

            _logger.LogInformation("Colaborador com ID {id} desabilitado com sucesso", id);
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


            // erro desconhecido
            if (respostaRequisicao.Mensagem != "Dados do colaborador atualizado com Sucesso")
            {
                return BadRequest(respostaRequisicao.Mensagem);
            }

            return Ok(respostaRequisicao);
        }



        
        [HttpDelete]
        [Route("deletarColaborador/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            var respostaRequisicao = await _repository.DeletarColaborador(id);

            // colaborador nao encontrado
            if (respostaRequisicao.Mensagem == "Colaborador não encontrado")
            {
                return NotFound(respostaRequisicao.Mensagem);
            }

            // erro interno na requisição
            if (respostaRequisicao.Mensagem != "Dados do colaborador deletado com sucesso")
            {
                return NotFound(respostaRequisicao.Mensagem);
            }


            return NoContent();
        }
    }
}
