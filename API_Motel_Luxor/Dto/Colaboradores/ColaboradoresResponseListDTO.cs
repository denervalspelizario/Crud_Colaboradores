using API_Motel_Luxor.Enum;

namespace API_Motel_Luxor.Dto.Colaboradores
{
    public class ColaboradoresResponseListDTO
    {
        public int? Colaborador_id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CargoEnum Cargo { get; set; }
        public DepartamentoEnum Departamento { get; set; }
        public string Status { get; set; }
    }
}
