using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Isbs { get; set; }
        public string Title { get; set; }
        public DateTime  DatePublished { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
