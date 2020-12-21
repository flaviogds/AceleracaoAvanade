using System.Threading.Tasks;

namespace ControleVendas.Repository
{
    public interface IUnityOfWork
    {
        public Task<int> SubmitChangesAsync();

        public Task RollBackChangesAsync();

    }
}
