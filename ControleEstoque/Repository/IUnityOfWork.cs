using System.Threading.Tasks;

namespace ControleEstoque.Repository
{
    public interface IUnityOfWork
    {
        public Task<int> SubmitChangesAsync();

        public Task RollBackChangesAsync();

    }
}
