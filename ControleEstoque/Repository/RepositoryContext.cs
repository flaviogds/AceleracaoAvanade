using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Produtos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("Data Source=DBStorage.db");
        }
    }
}