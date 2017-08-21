using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Services
{
    public static class BookMapper
    {
        public static BookDTO Map(Book source, BookDTO target = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                target = new BookDTO();
            }

            target.BookID = source.BookID;
            target.Title = source.Title;

            return target;
        }


        public static Book Map(BookDTO source, Book target = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                target = new Book();
            }

            target.BookID = source.BookID;
            target.BookID = source.BookID;
            target.Title = source.Title;

            return target;
        }

    }
}
