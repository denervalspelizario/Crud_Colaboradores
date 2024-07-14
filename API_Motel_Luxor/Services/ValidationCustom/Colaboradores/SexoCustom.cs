using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Services.ValidationCustom.Colaboradores
{
    public static class SexoCustom
    {
        public static ValidationResult ValidateSexo( SexoEnum sexo, ValidationContext context)
        {
            if (SexoEnum.IsDefined(typeof(SexoEnum), sexo))
            {
                return new ValidationResult("O cargo selecionado é inválido.");
            }
            return ValidationResult.Success;
        }
    }
}
