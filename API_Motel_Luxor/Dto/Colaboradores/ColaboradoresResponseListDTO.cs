using API_Motel_Luxor.Enum;

namespace API_Motel_Luxor.Dto.Colaboradores
{
    public class ColaboradoresResponseListDTO
    {
        public int? Colaborador_id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Departamento { get; set; }

        public ColaboradoresResponseListDTO(int? colaborador_id, string nome, string email, string cargo, string departamento)
        {
            Colaborador_id = colaborador_id;
            Nome = nome;
            Email = email;
            Cargo = cargo;
            Departamento = departamento;
        }
    }
}
