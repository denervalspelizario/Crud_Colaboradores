using API_Motel_Luxor.Model.Administradores;

namespace API_Motel_Luxor.Services.Senha
{
    public interface ISenhaRepository
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);

        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);

        string CriarToken(AdministradoresModel administradores);
    }
}
