using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Motel_Luxor.Model.Colaboradores
{
    [Table("colaboradores")]
    public class ColaboradoresModel
    {
        [Key]
        public int? colaborador_id { get;  set; }
        public string nome { get;  set; }

        [Column(TypeName = "date")]
        public DateTime data_nascimento { get;  set; }
        public string cpf { get;  set; }
        public string endereco { get;  set; }
        public SexoEnum sexo { get;  set; }
        public string telefone { get;  set; }
        public string email { get;  set; }

        [Column(TypeName = "date")]
        public DateTime data_admissao { get;  set; }
        public CargoEnum cargo { get;  set; }
        public decimal salario { get;  set; }
        public DepartamentoEnum departamento { get;  set; }
        public string status { get;  set; }

        public ColaboradoresModel(
            string nome, DateTime data_nascimento, string cpf, string endereco
            ,SexoEnum sexo, string telefone, string email, DateTime data_admissao, CargoEnum cargo,
            decimal salario, DepartamentoEnum departamento, string status) 
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
