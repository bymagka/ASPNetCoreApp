using ASPNetCoreApp.Domain.Entities;


namespace ASPNetCoreApp.Interfaces.Services
{
    public interface ICartStore
    {
        public Cart Cart { get; set; }
    }
}
