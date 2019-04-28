using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class NoteStore : ContribStore<Note>
    {
        public override string StoreKey => @"notes";

        public NoteStore(IObjectStore objectStore)
            : base(objectStore)
        {
        }

        public IEnumerable<Note> FindByAssociation(int associatedContribKey)
        {
            return _cache.Where(n => n.AssociatedContribKey.Equals(associatedContribKey));
        }
    }
}
