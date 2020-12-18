using System.Threading.Tasks;

namespace ControleEstoque.Repository
{
    public interface IRepositoryChanges
    {
        public Task<int> SubmitChangesAsync();

        public Task RollBackChangesAsync();

    }
}
