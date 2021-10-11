

using ASPNetCoreApp.ViewModels;

namespace ASPNetCoreApp.Services.Interfaces
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
