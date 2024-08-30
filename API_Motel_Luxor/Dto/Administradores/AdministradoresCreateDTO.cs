using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Dto.Administradores
{
    public class AdministradoresCreateDTO
    {
        [Required(ErrorMessage = "O campo usuário é obrigatorio")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não coincidem")]
        public string ConfirmeSenha { get; set; }

        [Required(ErrorMessage = "O campo cargo é obrigatorio")]
        public CargoEnum Cargo { get; set; }
    }
}
