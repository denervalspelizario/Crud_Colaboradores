using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Dto.Administradores
{
    public class AdministradorResponseDTO
    {
       
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public CargoEnum Cargo { get; set; }

        public AdministradorResponseDTO(int id, string usuario, string email, CargoEnum cargo)
        {
            Id = id;
            Usuario = usuario;
            Email = email;
            Cargo = cargo;
        }

    }
}
