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









            //using (var db = new LibraryContext())
            //{
            //    return db.Book.Include(x=>x.Genre).ToList();
            //}
            //return _allBooks.OrderBy(x => x.Id).ToList();
        }

        public Book GetBook(int id)
        {
            return GetBooks().First(x=>x.Id == id);
        }

        public int AddBook(Book book)
        {
            var maxId = _allBooks.Select(x => x.Id).Max();
            book.Id = maxId + 1;

            var genreRepository = new GenresRepository();
            book.Genre = genreRepository.Get(book.GenreId);

            _allBooks.Add(book);

            return book.Id;
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _allBooks.FirstOrDefault(x => x.Id == book.Id);
            _allBooks.Remove(existingBook);

            var genreRepository = new GenresRepository();
            book.Genre = genreRepository.Get(book.GenreId);

            _allBooks.Add(book);
        }

        public void DeleteBook(int id)
        {
            var existingBook = _allBooks.FirstOrDefault(x => x.Id == id);
            _allBooks.Remove(existingBook);
        }

        public List<Book> GetBooksByGenreId(int genreId)
        {
            return GetBooks().Where(x => x.GenreId == genreId);
        }
    }
}
