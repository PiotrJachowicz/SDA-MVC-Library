using Library.Database;
//using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BooksRepository
    {
        // Tytuły książek podane przez kursantów, czytaj na własną odpowiedzialność
        private static List<Book> _allBooks = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Myśli współczesnego człowieka",
                Author = "Roman Dmowski",
                ProductionYear = 1920,
                Genre = new Genre
                {
                    Id = 1,
                    Name = "Political fiction"
                }
            },
            new Book
            {
                Id = 2,
                Title = "Czerwona książeczka",
                Author = "Mao Ze Dung",
                ProductionYear = 1925,
                Genre = new Genre
                {
                    Id = 1,
                    Name = "Political fiction"
                }
            }
        };

        public IEnumerable<Book> GetBooks()
        {
            using(var context = new LibraryContext())
            {
                return context.Book.Include(x => x.Genre).ToList();
            }
        }

        public Book GetBook(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Book.Include(x => x.Genre).Single(x => x.Id == id);
            }
        }

        public int AddBook(Book book)
        {
            using(var context = new LibraryContext())
            {
                context.Book.Add(book);
                context.SaveChanges();
                return book.Id;
            }
        }

        public void UpdateBook(Book book)
        {
            using (var context = new LibraryContext())
            {
                var originalBook = context.Book.Find(book.Id);
                context.Entry(originalBook).CurrentValues.SetValues(book);
                context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var existingBook = _allBooks.FirstOrDefault(x => x.Id == id);
            _allBooks.Remove(existingBook);
        }

        public IEnumerable<Book> GetBooksByGenreId(int genreId)
        {
            return GetBooks().Where(x => x.GenreId == genreId);
        }
    }
}
