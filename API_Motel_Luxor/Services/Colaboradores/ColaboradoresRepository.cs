using API_Motel_Luxor.Db;
using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Model.Colaboradores;
using API_Motel_Luxor.Model.Response;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Motel_Luxor.Services.Colaboradores
{
    public class ColaboradoresRepository : IColaboradoresRepository
    {
       
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<Response<ColaboradorResponseDTO>> AdicaoColaborador(ColaboradoresCreateDTO colaborador)
        {
            // formatação de resposta
            var resposta = new Response<ColaboradorResponseDTO>();
            
            try
            {
                // status dos colaboradores se iniciam como ativo
                var status = "Ativo";

                // data de cadastro do usuario
                DateTime dataAdmissao = DateTime.Today.Date;
                

                // email ja cadastrado?
                var emailDuplicado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.email == colaborador.Email);

                if (emailDuplicado != null)
                {
                    resposta.Mensagem = "Email já cadastrado";
                    return resposta;
                }

                // telefone ja cadastrado?
                var telefoneDuplicado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.telefone == colaborador.Telefone);

                if (telefoneDuplicado != null)
                {
                    resposta.Mensagem = "Telefone já cadastrado";
                    return resposta;
                }

                // telefone ja cadastrado?
                var cpfDuplicado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.cpf == colaborador.Cpf);

                if (cpfDuplicado != null)
                {
                    resposta.Mensagem = "Cpf já cadastrado";
                    return resposta;
                }


                // criando obj para adicionar no db
                var colaboradorAdicionado = new ColaboradoresModel(
                    colaborador.Nome,
                    colaborador.Data_nascimento,
                    colaborador.Cpf,
                    colaborador.Endereco,
                    colaborador.Telefone,
                    colaborador.Email,
                    dataAdmissao,
                    colaborador.Cargo,
                    colaborador.Salario,
                    colaborador.Departamento,
                    status
                     );


                // adicionando colaborador ao db
                _context.Colaboradores.Add(colaboradorAdicionado);


                // salvando no db
                _context.SaveChanges();


                // buscando novo colaborador no db
                var colaboradorEncontrado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.email == colaboradorAdicionado.email);
                

                // Formatacao das datas
                string dataNascimentoFormatada =  colaboradorEncontrado.data_nascimento.ToString("yyyy-MM-dd");
                string dataAdmissaoFormatada =  colaboradorEncontrado.data_admissao.ToString("yyyy-MM-dd");


                // formatando dados do novo colaborador para resposta
                var colaboradorResposta = new ColaboradorResponseDTO(
                    colaboradorEncontrado.colaborador_id,
                    colaboradorEncontrado.nome,
                    dataNascimentoFormatada,
                    colaboradorEncontrado.cpf,
                    colaboradorEncontrado.endereco,
                    colaboradorEncontrado.telefone,
                    colaboradorEncontrado.email,
                    dataAdmissaoFormatada,
                    colaboradorEncontrado.cargo,
                    colaboradorEncontrado.salario,
                    colaboradorEncontrado.departamento
                    );

                // adicionando respostas de sucesso 
                resposta.Dados = colaboradorResposta;
                resposta.Status = colaboradorEncontrado.status;
                resposta.Mensagem = "Dados Adicionandos com Sucesso";

                // retornando o objeto de resposta
                return resposta;
            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                throw new Exception(mensagemErro);
            }
        }
    }
}
