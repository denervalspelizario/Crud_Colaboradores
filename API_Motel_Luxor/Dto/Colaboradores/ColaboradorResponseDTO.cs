
using API_Motel_Luxor.Enum;

namespace API_Motel_Luxor.Dto.Colaboradores
{
    public class ColaboradorResponseDTO
    {
        public int? Colaborador_id { get; set; }
        public string Nome { get; set; }
        public string Data_nascimento { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Data_admissao { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public string Departamento { get; set; }


        public ColaboradorResponseDTO(int? colaborador_id, string nome, string data_nascimento, string cpf, string endereco, string telefone, string email, string data_admissao, string cargo, decimal salario, string departamento)
        {
            Colaborador_id = colaborador_id;
            Nome = nome;
            Data_nascimento = data_nascimento;
            Cpf = cpf;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
            Data_admissao = data_admissao;
            Cargo = cargo;
            Salario = salario;
            Departamento = departamento;
        }
    }
}
