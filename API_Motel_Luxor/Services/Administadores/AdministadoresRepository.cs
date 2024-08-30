using API_Motel_Luxor.Db;
using API_Motel_Luxor.Dto.Administradores;
using API_Motel_Luxor.Model.Administradores;
using API_Motel_Luxor.Model.Response;
using API_Motel_Luxor.Services.Senha;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Motel_Luxor.Services.Administadores
{
    public class AdministadoresRepository(
        AppDbContext context,
        ISenhaRepository senhaRepository ) : IAdminstradoresRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ISenhaRepository _senhaRepository = senhaRepository;
        public async Task<Response<AdministradorResponseDTO>> AdicaoAdministador(AdministradoresCreateDTO request)
        {
            // criando objeto com resposta padrão 
            Response<AdministradorResponseDTO> resposta = new Response<AdministradorResponseDTO>();

            try
            {
                // validando se usuario já existe
                if (!VerificaSeEmailEUsuarioJaExistem(request))
                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Email/Usuario já cadastrados";
                    return resposta;
                }

                // depois de validar criando a criptografia atravez do metodo CriarSenhaHash
                _senhaRepository.CriarSenhaHash(request.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var administrador = new AdministradoresModel
                (
                    request.Email,
                    request.Usuario,
                    request.Cargo,
                    senhaSalt,
                    senhaHash
                );


                // adicionando os dados do usuario no banco
                _context.Add(administrador);

                // salvando no banco
                await _context.SaveChangesAsync();

                // buscando dado do banco
                var administradorEncontrado = await _context.Administradores.FirstOrDefaultAsync(x => x.Email == request.Email);

                var administradorResponse = new AdministradorResponseDTO
                (
                    administradorEncontrado.Id,
                    administradorEncontrado.Email,
                    administradorEncontrado.Usuario,
                    administradorEncontrado.Cargo
                   
                );
                resposta.Dados = administradorResponse;
                resposta.Mensagem = "Administrador criado com sucesso!";
            }
            catch (Exception erro)
            {
                resposta.Dados = null;
                resposta.Mensagem = erro.Message;
            }

            return resposta;
        }




        public bool VerificaSeEmailEUsuarioJaExistem(AdministradoresCreateDTO request)
        {
            // verificando se existe ja no banco email e name 
            var admEncontrado = _context.Administradores.FirstOrDefault(
                x => x.Email == request.Email || x.Usuario == request.Usuario);

            if (admEncontrado != null)
            {
                return false;
            }
            return true;
        }
    }
}
