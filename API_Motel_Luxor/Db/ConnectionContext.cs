using API_Motel_Luxor.Model.Colaboradores;
using Microsoft.EntityFrameworkCore;

namespace API_Motel_Luxor.Db
{
    public class ConnectionContext : DbContext
    {
        public DbSet<ColaboradoresModel> Colaboradores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql(
               "Server=localhost;" +
               "Port=5432;Database=luxor;" +
               "User Id=postgres;" +
               "Password=vaval0645;");
    }
}
