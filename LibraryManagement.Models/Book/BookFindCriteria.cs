using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookFindCriteria
    {

        public BookFindCriteria()
        {
            BookID = null;
            Title = null;
            IncludeIsAvaliable = false;
        }

        public int? BookID { get; set; }

        public string Title { get; set; }

        public bool IncludeIsAvaliable { get; set; }

        public bool? IsOverdue { get; set; }
    }

    
}
