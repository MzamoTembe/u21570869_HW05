using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21570869_HW05.Models
{
    public class BooksVM
    {
        public List<Book> Books { get; set; }

        public List<string> AuthorNames { get; set; }

        public List<string> TypeNames { get; set; }
    }
}