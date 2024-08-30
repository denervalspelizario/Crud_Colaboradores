using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Model.Administradores
{
    public class AdministradoresModel
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public CargoEnum Cargo { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public DateTime TokenDataCriacao { get; set; } = DateTime.Now;


        public AdministradoresModel(int id, string email, string usuario, CargoEnum cargo, byte[] senhaHash, byte[] senhaSalt, DateTime tokenDataCriacao)
        {
            Id = id;
            Email = email;
            Usuario = usuario;
            Cargo = cargo;
            SenhaHash = senhaHash;
            SenhaSalt = senhaSalt;
            TokenDataCriacao = tokenDataCriacao;
        }

        public AdministradoresModel( string email, string usuario, CargoEnum cargo, byte[] senhaHash, byte[] senhaSalt)
        {
            Email = email;
            Usuario = usuario;
            Cargo = cargo;
            SenhaHash = senhaHash;
            SenhaSalt = senhaSalt;
        }
    }
}
