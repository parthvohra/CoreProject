using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject_Models.Models.ViewModels
{
    public class BookAuthorVM
    {
        public Book Book { get; set; }
        public BookAuthor BookAuthor { get; set; }
        public List<BookAuthor> BookAuthorsList { get; set; }
        public IEnumerable<SelectListItem> AuthorsList { get; set; }
    }
}
