using Swashbuckle.AspNetCore.Filters;

namespace API_Motel_Luxor.Dto.Colaboradores
{
    public class ColaboradoresExampleDTO: IExamplesProvider<ColaboradoresCreateDTO>
    {
        public ColaboradoresCreateDTO GetExamples()
        {
            return new ColaboradoresCreateDTO
            {
                Nome = "João Silva",
                Data_nascimento = "DD/MM/YYYY",
                Cpf = "12345678901",
                Endereco = "Rua Exemplo, 123, Centro, São Paulo",
                Sexo = Enum.SexoEnum.Masculino,
                Telefone = "(11)98765-4321",
                Email = "exemplo@dominio.com",
                Cargo = Enum.CargoEnum.Administrador,
                Salario = 3000.50M,
                Departamento = Enum.DepartamentoEnum.Administrativo,
            };
        }
    }
}
