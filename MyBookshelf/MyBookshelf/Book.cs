using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf
{
    public class Book
    {
        public int ISBN { get; set; }
        public List<string> Categories { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public Book()
        {
            Categories = new List<string>();
        }
    }
}
