using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Model.Response;

namespace API_Motel_Luxor.Services.Colaboradores
{
    public interface IColaboradoresRepository
    {
        Task<Response<ColaboradorResponseDTO>> AdicaoColaborador(ColaboradoresCreateDTO colaborador);

        Task<Response<ColaboradorResponseDTO>> BuscarColaborador(int id);

        Task<ResponseList<List<ColaboradoresResponseListDTO>>> ListarColaboradores();
    }
}
