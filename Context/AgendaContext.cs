using Dio.AgendaDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace Dio.AgendaDeContatos.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }
    }
}
