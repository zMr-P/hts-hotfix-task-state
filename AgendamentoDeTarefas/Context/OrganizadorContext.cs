using Microsoft.EntityFrameworkCore;
using AgendamentoDeTarefas.Models.Entites;

namespace AgendamentoDeTarefas.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }
         public DbSet<Tarefa> Tarefas { get; set; }
    }
}
