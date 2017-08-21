using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(int ID);
        void Update(TEntity entity);
    }
}
