using System.Threading.Tasks;

namespace ControleVendas.Repository
{
    public interface IRepositoryChanges
    {
        public Task<int> SubmitChangesAsync();

        public Task RollBackChangesAsync();

    }
}
