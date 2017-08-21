using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Entities;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> FindByCriteria(BookFindCriteria criteria);
        Book Find(int bookID);
    }
}
