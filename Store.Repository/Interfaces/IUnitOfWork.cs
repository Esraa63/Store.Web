using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepoistory<TEntity, TKey> Repoistory<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> CompleteAsync();

    }
}
