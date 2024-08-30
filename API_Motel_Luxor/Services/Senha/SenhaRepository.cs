using API_Motel_Luxor.Model.Administradores;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_Motel_Luxor.Services.Senha
{
    public class SenhaRepository(IConfiguration config) : ISenhaRepository
    {
        public IConfiguration _config = config;

        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                // senha salt(chave para criar e descriptografar a senha hash) e hash(senha criptografada)
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

            }
        }

        public bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA256(senhaSalt))
            {
                // senha hash 
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

                // retorna computedHash(senha passa por login hasheada)
                return computedHash.SequenceEqual(senhaHash);
            }
        }

        public string CriarToken(AdministradoresModel administradores)
        {
            List<Claim> clainsCriada = new List<Claim>();
            {
                new Claim("Cargo", administradores.Cargo.ToString());
                new Claim("Email", administradores.Email);
                new Claim("Username", administradores.Usuario);
            }

            // criando o token baseado na chave adicionada em appsettings.json
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            // criando a credencial baseado na key acima que foi baseado no token com o método de criptografia
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // agora de fato criando nosso token
            var token = new JwtSecurityToken(
                claims: clainsCriada, // claim criada
                expires: DateTime.Now.AddDays(1), // token expira com 1 ano
                signingCredentials: cred // credencial criada acima
             );

            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
