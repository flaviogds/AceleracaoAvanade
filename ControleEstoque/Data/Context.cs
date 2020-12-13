using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Models
{
    public class Context : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("Data Source=LocalStorage_Estoque.db");
        }
    }
}
