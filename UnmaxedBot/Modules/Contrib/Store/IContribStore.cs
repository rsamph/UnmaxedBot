using System.Collections.Generic;
using System.Threading.Tasks;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public interface IContribStore<T> where T : IContrib
    {
        IEnumerable<int> Keys { get; }

        Task Add(T contrib);
        Task Remove(int contribKey);
        bool Exists(T contrib);
        bool KeyExists(int contribKey);
        T FindByContribKey(int contribKey);
        T FindByNaturalKey(T contrib);
        IEnumerable<Contributor> GetContributors();
    }
}
