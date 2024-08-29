using API_Motel_Luxor.Model.Colaboradores;
using Microsoft.EntityFrameworkCore;

namespace API_Motel_Luxor.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
        
        }
        public DbSet<ColaboradoresModel> Colaboradores { get; set; }
       
    }
}
