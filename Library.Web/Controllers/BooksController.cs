using Library.Models;
using Library.Repositories;
//using Library.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class BooksController : Controller
    {
        private BooksRepository _booksRepository = new BooksRepository();
        private GenresRepository _genresRepository = new GenresRepository();

        public IActionResult Index()
        {
            InitializeViewBagGenresDropdown();
            var booksList = _booksRepository.GetBooks();
            return View(booksList);
        }

        public IActionResult Details(int id)
        {
            var book = _booksRepository.GetBook(id);
            return View(book);
        }

        public IActionResult Create()
        {

            InitializeViewBagGenresDropdown();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            { 
                var newBookId = _booksRepository.AddBook(book);
                return RedirectToAction("Details", new { id = newBookId });
            }
            else
            {

                InitializeViewBagGenresDropdown();
                return View(book);
            }
            
        }

        public IActionResult Update(int id)
        {
            var book = _booksRepository.GetBook(id);

            InitializeViewBagGenresDropdown();

            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            _booksRepository.UpdateBook(book);

            return RedirectToAction("Details", new { id = book.Id });
        }

        public IActionResult Delete(int id)
        {
            var book = _booksRepository.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _booksRepository.DeleteBook(book.Id);
            return RedirectToAction("index");
        }

        public IActionResult BooksList(int genreId)
        {
            var books = _booksRepository.GetBooksByGenreId(genreId);
            return PartialView("_booksList", books);
        }
            
        private void InitializeViewBagGenresDropdown()
        {
            ViewBag.Genres = _genresRepository.GetAll()
                   .Select(x =>
                       new SelectListItem
                       {
                           Text = x.Name,
                           Value = x.Id.ToString()
                       }
                   );
        }
    }
}
