using API_Motel_Luxor.Db;
using API_Motel_Luxor.Dto.Colaboradores;
using API_Motel_Luxor.Model.Colaboradores;
using API_Motel_Luxor.Model.Response;
using Microsoft.EntityFrameworkCore;

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

               // data de nascimento para formato aceito no banco
                DateTime dataNascimento = DateTime.Parse(colaborador.Data_nascimento);


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
                    dataNascimento,
                    colaborador.Cpf,
                    colaborador.Endereco,
                    colaborador.Sexo,
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
                

                

                // formatando dados do novo colaborador para resposta
                var colaboradorResposta = new ColaboradorResponseDTO(
                    colaboradorEncontrado.colaborador_id,
                    colaboradorEncontrado.nome,
                    colaboradorEncontrado.data_nascimento.ToString("dd-MM-yyyy"),
                    colaboradorEncontrado.cpf,
                    colaboradorEncontrado.endereco,
                    colaboradorEncontrado.sexo.ToString(),
                    colaboradorEncontrado.telefone,
                    colaboradorEncontrado.email,
                    colaboradorEncontrado.data_admissao.ToString("dd-MM-yyyy"),
                    colaboradorEncontrado.cargo.ToString(),
                    colaboradorEncontrado.salario,
                    colaboradorEncontrado.departamento.ToString()
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
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);
            }
        }

        public async Task<Response<ColaboradorResponseDTO>> BuscarColaborador(int id)
        {
            // formatação de resposta
            var resposta = new Response<ColaboradorResponseDTO>();

            try
            {
                var colaboradorEncontrado = await _context.Colaboradores.FindAsync(id);

                // validando se user foi encontrado
                if (colaboradorEncontrado is null || colaboradorEncontrado.status == "Inativo")
                {
                    resposta.Mensagem = "Colaborador não encontrado";
                    return resposta;
                }

                // Formatando msg de resposta
                var colaboradorResponse = new ColaboradorResponseDTO(
                    colaboradorEncontrado.colaborador_id,
                    colaboradorEncontrado.nome,
                    colaboradorEncontrado.data_nascimento.ToString("dd-MM-yyyy"),
                    colaboradorEncontrado.cpf,
                    colaboradorEncontrado.endereco,
                    colaboradorEncontrado.sexo.ToString(),
                    colaboradorEncontrado.telefone,
                    colaboradorEncontrado.email,
                    colaboradorEncontrado.data_admissao.ToString("dd-MM-yyyy"),
                    colaboradorEncontrado.cargo.ToString(),
                    colaboradorEncontrado.salario,
                    colaboradorEncontrado.departamento.ToString()
                    ); 

                //resposta estrutura
                resposta.Dados = colaboradorResponse;
                resposta.Status = colaboradorEncontrado.status;
                resposta.Mensagem = "Dados de Colaborador listado com Sucesso";


                // retornando resposta
                return resposta;
            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);
            }

        }

        public async Task<ResponseList<List<ColaboradoresResponseListDTO>>> ListarColaboradores()
        {
            // formatação de resposta
            var resposta = new ResponseList<List<ColaboradoresResponseListDTO>>();

            try
            {
                // objeto com colaboradores do banco
                var listaColaboradores = await _context.Colaboradores.ToListAsync();



                // objeto com Lista de colaboradores tipo ColaboradoresResponseListDTO
                var listaColaboradoresResposta = new List<ColaboradoresResponseListDTO>();

                // adicionando todos os adms na lista de objetos respostaFormatada
                foreach (var colaborador in listaColaboradores)
                {
                    if (colaborador.status == "Ativo")
                    {
                        var colaboradorFormatado = new ColaboradoresResponseListDTO
                        (
                            colaborador.colaborador_id,
                            colaborador.nome,
                            colaborador.email,
                            colaborador.cargo.ToString(),
                            colaborador.departamento.ToString()
                        );

                        listaColaboradoresResposta.Add(colaboradorFormatado);
                    }
                }

                // resposta estrutura
                resposta.Dados = listaColaboradoresResposta;
                resposta.Mensagem = "Requisição efetuado com sucesso";

                return resposta;

            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);
            }
        }

        public async Task<ResponseMessagem> DesabilitarColaborador(int id)
        {
            // resposta formatada
            var resposta = new ResponseMessagem();
            try
            {
                // obtendo a entidade pelo id
                var colaboradorEncontrado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.colaborador_id == id);

                // validando se user foi encontrado
                if (colaboradorEncontrado is null)
                {
                    resposta.Mensagem = "Colaborador não encontrado";
                    return resposta;
                }

                // validando se colaborador já esta ativado
                if (colaboradorEncontrado.status == "Inativo")
                {
                    resposta.Mensagem = "cadastro do colaborador já está inabilitado";
                    return resposta;
                }

                // alteração do status da entidade                   
                colaboradorEncontrado.status = "Inativo";


                // salvando os dados
                _context.SaveChanges();

                resposta.Mensagem = "Cadastro do colaborador desativado com sucesso";
                return resposta;

            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);
            }    
        }

        public async Task<ResponseMessagem> HabilitarColaborador(int id)
        {
            // resposta formatada
            var resposta = new ResponseMessagem();
            try
            {
                // obtendo a entidade pelo id
                var colaboradorEncontrado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.colaborador_id == id);

                // validando se user foi encontrado
                if (colaboradorEncontrado is null)
                {
                    resposta.Mensagem = "Colaborador não encontrado";
                    return resposta;
                }

                // validando se colaborador já esta ativado
                if (colaboradorEncontrado.status == "Ativo")
                {
                    resposta.Mensagem = "cadastro do colaborador já está ativado";
                    return resposta;
                }


                // alteração do status da entidade                   
                colaboradorEncontrado.status = "Ativo";


                // salvando os dados
                _context.SaveChanges();

                resposta.Mensagem = "Cadastro do colaborador ativado com sucesso";
                return resposta;

            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);
            }
        }

        public async Task<Response<ColaboradorResponseDTO>> AtualizarColaborador(ColaboradorUpdateDTO colaborador)
        {
            // formatação de resposta
            var resposta = new Response<ColaboradorResponseDTO>();

            try
            {
                // obtendo o colaborador pelo id
                var colaboradorEcontrado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.colaborador_id == colaborador.Id);

                // validando se user foi encontrado
                if (colaboradorEcontrado is null)
                {
                    resposta.Mensagem = "Colaborador não encontrado";
                    return resposta;
                }

                // email ja existe?
                var emailDuplicado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.email == colaborador.Email);

                // o email existe e ele esta em um cadastro diferente
                if (emailDuplicado != null && emailDuplicado.colaborador_id != colaborador.Id)
                {
                    resposta.Mensagem = "Email já cadastrado por outro colaborador";
                    return resposta;
                }


                // cpf ja existe?
                var cpfDuplicado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.cpf == colaborador.Cpf);

                // o cpf existe e ele esta em um cadastro diferente
                if (cpfDuplicado != null && cpfDuplicado.colaborador_id != colaborador.Id)
                {
                    resposta.Mensagem = "Cpf já cadastrado por outro colaborador";
                    return resposta;
                }


                // telefone ja existe?
                var telefoneDuplicado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.telefone == colaborador.Telefone);

                // o cpf existe e ele esta em um cadastro diferente
                if (telefoneDuplicado != null && telefoneDuplicado.colaborador_id != colaborador.Id)
                {
                    resposta.Mensagem = "Telefone já cadastrado por outro colaborador";
                    return resposta;
                }

                // data de nascimento para formato aceito no banco
                DateTime dataNascimento = DateTime.Parse(colaborador.Data_nascimento);



                var colaboradorAtualizado = new ColaboradoresModel(
                    colaborador.Id,
                    colaborador.Nome,
                    dataNascimento,
                    colaborador.Cpf,
                    colaborador.Endereco,
                    colaborador.Sexo,
                    colaborador.Telefone,
                    colaborador.Email,
                    colaboradorEcontrado.data_admissao,
                    colaborador.Cargo,
                    colaborador.Salario,
                    colaborador.Departamento,
                    colaboradorEcontrado.status
                    );

                // alterando tabela com dados adm por dados admAtualizado
                _context.Colaboradores.Entry(colaboradorEcontrado).CurrentValues.SetValues(colaboradorAtualizado);



                // salvando os dados no banco
                _context.SaveChanges();

                // Formatando msg de resposta
                var colaboradorResponse = new ColaboradorResponseDTO(

                    colaborador.Id,
                    colaborador.Nome,
                    dataNascimento.ToString("dd-MM-yyyy"),
                    colaborador.Cpf,
                    colaborador.Endereco,
                    colaborador.Sexo.ToString(),
                    colaborador.Telefone,
                    colaborador.Email,
                    colaboradorEcontrado.data_admissao.ToString("dd-MM-yyyy"),
                    colaborador.Cargo.ToString(),
                    colaborador.Salario,
                    colaborador.Departamento.ToString()
                 ); 

                //resposta estrutura
                resposta.Dados = colaboradorResponse;
                resposta.Status = colaboradorEcontrado.status;
                resposta.Mensagem = "Dados do colaborador atualizado com Sucesso";


                // retornando resposta
                return resposta;
            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);

            }
        }

        public async Task<ResponseMessagem> DeletarColaborador(int id)
        {
            // resposta formatada
            var resposta = new ResponseMessagem();

            try
            {

                // obtendo a entidade pelo id
                var colaboradorEncontrado = await _context.Colaboradores.FirstOrDefaultAsync(x => x.colaborador_id == id);

                // validando se user foi encontrado
                if (colaboradorEncontrado is null)
                {
                    resposta.Mensagem = "Colaborador não encontrado";
                    return resposta;
                }


                // removendo entidade do bd
                _context.Colaboradores.Remove(colaboradorEncontrado);

                // salvando os dados
                _context.SaveChanges();

                resposta.Mensagem = "Dados do colaborador deletado com sucesso";
                return resposta;

            }
            catch (Exception erro)
            {
                string mensagemErro = erro.Message;
                resposta.Mensagem = "Erro interno na solicitação";
                throw new Exception(mensagemErro);
            }
        }
    }
}
