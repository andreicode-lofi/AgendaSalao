using Microsoft.EntityFrameworkCore;

namespace VagaSalão.Contexto
{
    public class appContexto : DbContext
    {
        public appContexto(DbContextOptions<appContexto> options) : base(options)
        {

        }

        public DbSet<VagaSalão.Models.Cadastro> Cadastro { get; set;}
        public DbSet<VagaSalão.Models.Login> Login { get; set;} 
    }
}
