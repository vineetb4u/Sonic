using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Contracts;
using LibraryManagement.Data.Entities;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private ConcurrentBag<Book> _inMemoryDb;
        private static BookRepository _instance;
        private static readonly object lockObj = new object();

        public BookRepository()
        {
            _inMemoryDb = new ConcurrentBag<Book>();
        }

        public static BookRepository Database
        {
            get
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new BookRepository();
                    }
                }

                return _instance;
            }
        }

        private IQueryable<Book> Query()
        {
            return _inMemoryDb.AsQueryable();
        }

        public void Add(Book book)
        {
            _inMemoryDb.Add(book);
        }

        public void Update(Book book)
        {
            var rec = Query().Where(b => b.BookID == book.BookID).FirstOrDefault();

            rec.BookID = book.BookID;
            rec.Title = book.Title;
        }

        public void Delete(Book book)
        {
            _inMemoryDb.TryTake(out book);
        }

        public IEnumerable<Book> FindByCriteria(BookFindCriteria criteria)
        {
            var query = Query();

            if (criteria.BookID.HasValue)
                query = query.Where(book => book.BookID == criteria.BookID.Value);

            if (criteria.Title != null)
                query = query.Where(book => book.Title == criteria.Title);

            return query.ToList();
        }

        public Book Find(int bookID)
        {
            var query = Query();
            return query.Where(book => book.BookID == bookID).FirstOrDefault();
        }
    }
}
