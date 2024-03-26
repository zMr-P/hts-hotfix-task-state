using Microsoft.EntityFrameworkCore;
using AgendamentoDeTarefas.Entites;

namespace AgendamentoDeTarefas.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }
        public DbSet<MeuUsuario> MeusUsuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
