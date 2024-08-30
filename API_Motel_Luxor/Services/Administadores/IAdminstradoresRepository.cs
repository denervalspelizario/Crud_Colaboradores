using API_Motel_Luxor.Dto.Administradores;
using API_Motel_Luxor.Model.Response;

namespace API_Motel_Luxor.Services.Administadores
{
    public interface IAdminstradoresRepository
    {
        Task<Response<AdministradorResponseDTO>> AdicaoAdministador(AdministradoresCreateDTO administrador);
    }
}
