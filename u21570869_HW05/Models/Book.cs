using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21570869_HW05.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int Point { get; set; }
        public Author Author { get; set; }
        public BookType BookType { get; set; }

        public int Borrows { get; set; }
        public string Status { get; set; }

    }
}