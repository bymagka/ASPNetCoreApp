
using System.Collections.Generic;

namespace ASPNetCoreApp.Interfaces.TestApi
{
    public interface IValuesService
    {
        IEnumerable<string> GetAll();

        int Count();

        string GetById(int id);

        void Add(string Value);

        void Edit(int id, string Value);

        bool Delete(int id);
    }
}
