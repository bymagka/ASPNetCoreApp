

using ASPNetCoreApp.Domain.ViewModels;

namespace ASPNetCoreApp.Interfaces.Services
{
    public interface ICartService
    {
        void Add(int id);

        void Decrement(int id);

        void Remove(int id);

        void Clear();

        CartViewModel GetViewModel(); 


    }
}
