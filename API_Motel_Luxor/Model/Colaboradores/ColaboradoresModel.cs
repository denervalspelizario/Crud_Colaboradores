using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Motel_Luxor.Model.Colaboradores
{
    [Table("colaboradores")]
    public class ColaboradoresModel
    {
        [Key]
        public int? colaborador_id { get; private set; }
        public string nome { get; private set; }

        [Column(TypeName = "date")]
        public DateTime data_nascimento { get; private set; }
        public string cpf { get; private set; }
        public string endereco { get; private set; }
        public string sexo { get; private set; }
        public string telefone { get; private set; }
        public string email { get; private set; }

        [Column(TypeName = "date")]
        public DateTime data_admissao { get; private set; }
        public string cargo { get; private set; }
        public decimal salario { get; private set; }
        public string departamento { get; private set; }
        public string status { get; private set; }

        public ColaboradoresModel(
            string nome, DateTime data_nascimento, string cpf, string endereco
            ,string sexo, string telefone, string email, DateTime data_admissao, string cargo,
            decimal salario, string departamento, string status) 
        {
            this.nome = nome;
            this.data_nascimento = data_nascimento;
            this.cpf = cpf;
            this.endereco = endereco;
            this.sexo = sexo;
            this.telefone = telefone;
            this.email = email;
            this.data_admissao = data_admissao;
            this.cargo = cargo;
            this.salario = salario;
            this.departamento = departamento;
            this.status = status;
        }
    }
}
