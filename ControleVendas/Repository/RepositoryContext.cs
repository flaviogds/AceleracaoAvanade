using ControleVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleVendas.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Produtos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("Data Source=DBSales.db");
        }
    }
}