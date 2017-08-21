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
    public class BorrowRepository : IBorrowRepository
    {
        private ConcurrentBag<Borrow> _inMemoryDb;
        private static BorrowRepository _instance;
        private static readonly object lockObj = new object();

        public BorrowRepository()
        {
            _inMemoryDb = new ConcurrentBag<Borrow>();
        }

        public static BorrowRepository Database
        {
            get
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new BorrowRepository();
                    }
                }

                return _instance;
            }
        }

        private IQueryable<Borrow> Query()
        {
            return _inMemoryDb.AsQueryable();
        }

        public void Add(Borrow borrow)
        {
            _inMemoryDb.Add(borrow);
        }

        public void Update(Borrow borrow)
        {
            var rec = Query().Where(b => b.BookID == borrow.BookID && b.StudentID == b.StudentID).FirstOrDefault();

            rec.BookID = borrow.BookID;
            rec.StudentID = borrow.StudentID;
            rec.DueDate = borrow.DueDate;

            if (borrow.ReturnDate.HasValue)
                rec.ReturnDate = borrow.ReturnDate;
            else
                rec.ReturnDate = null;

        }

        public void Delete(Borrow borrow)
        {
            _inMemoryDb.TryTake(out borrow);
        }

        public IEnumerable<Borrow> FindByCriteria(BorrowFindCriteria criteria)
        {
            var query = Query();

            if (criteria.BookID.HasValue)
                query = query.Where(borrow => borrow.BookID == criteria.BookID.Value);

            if (criteria.StudentID.HasValue)
                query = query.Where(borrow => borrow.StudentID == criteria.StudentID.Value);

            if (criteria.ReturnDate.HasValue)
                query = query.Where(borrow => borrow.DueDate == criteria.ReturnDate.Value);

            return query.ToList();
        }

        public Borrow Find(int bookID, int studentID)
        {
            var query = Query();
            return query.Where(borrow => borrow.BookID == bookID && borrow.StudentID == studentID).FirstOrDefault();
        }

        public bool isBookAvaliable(int bookID)
        {
            var query = Query();
            query = query.Where(borrow => borrow.BookID == bookID && borrow.ReturnDate == null);

            return query.ToList().Count < 1;
        }

        public bool isBookOverdue(int bookID)
        {
            var query = Query();
            query = query.Where(borrow => borrow.BookID == bookID && borrow.DueDate < DateTime.Now && borrow.ReturnDate == null);

            return query.ToList().Count > 0;
        }
    }
}
