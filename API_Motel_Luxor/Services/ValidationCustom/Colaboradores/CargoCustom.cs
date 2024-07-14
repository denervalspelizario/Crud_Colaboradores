using API_Motel_Luxor.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Motel_Luxor.Services.ValidationCustom.Colaboradores
{
    public static class CargoCustom
    {
        public static ValidationResult ValidateCargo(CargoEnum cargo, ValidationContext context)
        {
            if (CargoEnum.IsDefined(typeof(CargoEnum), cargo))
            {
                return new ValidationResult("O cargo selecionado é inválido.");
            }
            return ValidationResult.Success;
        }
    }
}
