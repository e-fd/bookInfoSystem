using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KProject
{
    internal class Book
    {
        private static int bookCounter;
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public Book(string? title)
        {
            Title = title;
            BookId = bookCounter++;
        }
        public Book(string? title, string? author) : this (title)
        {
            Author = author;
        }
        public Book(string? title, string? author, string? description) : this(title, author)
        {
            Description = description;
        }
        public Book(string? title, string? author, string? description, string? genre) : this(title, author, description)
        {
            Genre = genre;
        }
        public void BookInfo(out int bookId, out string? title, out string? author, 
                                out string? description, out string? genre)
        {
            bookId = BookId;
            title = Title;
            author = Author;
            description = Description;
            genre = Genre;
        }
    }
}
