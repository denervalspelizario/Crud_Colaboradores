using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Dto.Administradores
{
    public class AdministradorLoginDTO
    {
        [Required(ErrorMessage = "O campo email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Senha { get; set; }
    }
}
