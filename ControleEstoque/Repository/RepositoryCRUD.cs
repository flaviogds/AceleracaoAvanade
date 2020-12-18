using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Repository
{
    public class RepositoryCRUD : IRepositoryCRUD
    {
        private readonly RepositoryContext _context;

        private readonly DbSet<Product> _DbSet;

        public RepositoryCRUD(RepositoryContext context)
        {
            _context = context;
            _DbSet = _context.Set<Product>();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetAsync(int? id)
        {
            return await _DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Product> AddAsync( Product item )
        {
            if (await _DbSet.AnyAsync(e => 
                e.Id == item.Id 
               || e.Nome == item.Nome 
               || e.Quantidade == item.Quantidade)) return null;

            await _DbSet.AddAsync(item);

            return item;
        }

        public async Task<Product> UpdateAsync( int id, Product item )
        {
            if (!await _DbSet.AnyAsync(e => e.Id == id)) return null;

            _context.Entry(item).State = EntityState.Modified;

            return item;
        }

        public async Task<Product> DeleteAsync( int? id )
        {
            Product product = await _DbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (product == null) return null;

            _DbSet.Remove(product);

            return product;
        }
    }
}
