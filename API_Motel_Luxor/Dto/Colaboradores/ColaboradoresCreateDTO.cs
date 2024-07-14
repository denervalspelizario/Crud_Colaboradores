using API_Motel_Luxor.Enum;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Dto.Colaboradores
{
    public class ColaboradoresCreateDTO
    {
        [Required(ErrorMessage = "O Nome completo do colaborador é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tamanho do nome e sobrenome invalidos.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*)*$", ErrorMessage = "Cada palavra do nome ou sobrenome devem começar com uma letra maiúscula e conter apenas letras.")]
        
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "A data de nascimento do colaborador é obrigatório.")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/[0-9]{4}$", ErrorMessage = "A data deve estar no formato dd/MM/yyyy.")]
        [Display(Name = "Data de nascimento", Description = "data de nascimento no formato dd/MM/yyyy")]
        public string Data_nascimento { get; set; }


        
        [Required(ErrorMessage = "O cpf do colaborador é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
        [Display(Name = "Cpf", Description ="Apenas numeros")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O endereço do colaborador é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço não pode ter mais de 200 caracteres.")]
        [Display(Name = "Endereço completo", Description ="Rua Luz do sol numero 21")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O sexo do colaborador é obrigatório.")]
        [Display(Name = "Sexo")]
        public SexoEnum Sexo { get; set; }

        [Required(ErrorMessage = "O telefone do colaborador é obrigatório.")]
        [RegularExpression(@"^\(\d{2}\)\d{4,5}-\d{4}$", ErrorMessage = "O telefone deve estar no formato (XX)XXXXX-XXXX ou (XX)XXXX-XXXX.")]
        [Display(Name = "Celular", Description = "(XX)XXXXX-XXXX ou (XX)XXXX-XXXX")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O email do colaborador é obrigatório.")]
        [EmailAddress(ErrorMessage = "O endereço de e-mail não é válido.")]
        [Display(Name = "Email", Description = "exemplo@email.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O cargo do colaborador é obrigatório.")]
        [Display(Name = "Cargo")]
        public CargoEnum Cargo { get; set; }

        [Required(ErrorMessage = "O salario do colaborador é obrigatório.")]
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "O departamento do colaborador é obrigatório.")]
        [Display(Name = "Departamento")]
        public DepartamentoEnum Departamento { get; set; }
    }
}
