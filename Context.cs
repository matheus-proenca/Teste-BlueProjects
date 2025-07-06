using Microsoft.EntityFrameworkCore;
using TechStoreApi.models;

namespace TechStoreApi
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; } = null!;
    }
}