using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Services.ValidationCustom.Colaboradores
{
    public static class DepartamentoCustom
    {
        public static ValidationResult ValidateDepartamento(DepartamentoEnum departamento, ValidationContext context)
        {
            if (DepartamentoEnum.IsDefined(typeof(DepartamentoEnum), departamento))
            {
                return new ValidationResult("O cargo selecionado é inválido.");
            }
            return ValidationResult.Success;
        }
    }
}
