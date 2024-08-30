using API_Motel_Luxor.Dto.Administradores;
using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Services.Administadores;
using API_Motel_Luxor.Services.Colaboradores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API_Motel_Luxor.Controllers
{
    [ApiController]
    [Route("Administradores")]
    public class AdministradoresController(
        IAdminstradoresRepository repository, 
        ILogger<ColaboradoresController> logger) : ControllerBase
    {
   
        private readonly IAdminstradoresRepository _repository = repository;
        private readonly ILogger<ColaboradoresController> _logger = logger;


        [HttpPost]
        [Route("adicionarAdministador/")]
        public async Task<IActionResult> AdicaoAdministador(AdministradoresCreateDTO administrador)
        {
            var respostaRequisicao = await _repository.AdicaoAdministador(administrador);

            if (respostaRequisicao.Mensagem == "Email / Usuario já cadastrados")
            {
                _logger.LogWarning(respostaRequisicao.Mensagem);
                return Conflict(respostaRequisicao.Mensagem);
            }

            return Ok(respostaRequisicao.Dados);
        }
    }
}
