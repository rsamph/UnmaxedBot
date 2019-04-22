using System.Collections.Generic;
using System.Threading.Tasks;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public interface IContribStore<T> where T : IContrib
    {
        Task Add(T contrib);
        Task Remove(int contribKey);
        bool Exists(T contrib);
        int NextKey();
        bool KeyExists(int contribKey);
        T FindByContribKey(int contribKey);
        T FindByNaturalKey(T contrib);
        IEnumerable<Contributor> GetContributors();
    }
}
