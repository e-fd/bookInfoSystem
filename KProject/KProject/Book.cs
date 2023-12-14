using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KProject
{
    internal class Book
    {
        private int listIndex;      // 
        private static int bookCounter; // статический счетчик книг
        public int BookId { get; set; }          // свойства объектов класса Book
        public string? Title { get; set; }       //
        public string? Author { get; set; }      //
        public string? Genre { get; set; }       //
        public string? Description { get; set; } //
        public Book(string? title)               // (1) конструкторы для объектов класса Book
        {
            Title = title;
            BookId = bookCounter++; // присваивание ID книге при создании объекта
        }
        public Book(string? title, string? author) : this (title) // (1)
        {                                        // вызов предыдущего
            Author = author;                     // конструктора
        }
        public Book(string? title, string? author, string? genre) : 
            this(title, author) // (1)
        {
            Genre = genre;
        }
        public Book(string? title, string? author, string? genre, string? description) : 
            this(title, author, genre) // (1)
        {
            Description = description;
        }
        public int GetListIndex()
        {
            return listIndex;
        }
        public void SetListIndex(int value)
        {
            listIndex = value;
        }
        public int GetBookCounter()
        {
            return bookCounter;
        }
        public void SetBookCounter(int value)
        {
            bookCounter = value;
        }
        public void BookInfo(out int bookId, out string? title, out string? author, 
                                out string? description, out string? genre) // ?  .
        {
            bookId = BookId; 
            title = Title;
            author = Author;
            description = Description;
            genre = Genre;
        }
    }
}
