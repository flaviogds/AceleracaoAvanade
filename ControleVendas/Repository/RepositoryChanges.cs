using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ControleVendas.Repository
{
    public class RepositoryChanges : IRepositoryChanges
    {
        private readonly RepositoryContext _context;

        public RepositoryChanges(RepositoryContext context)
        {
            _context = context;
        }

        public Task RollBackChangesAsync()
        {
            _context.ChangeTracker.Entries()
                .Where(e => e.Entity != null)
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);

            return Task.CompletedTask;
        }

        public async Task<int> SubmitChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
