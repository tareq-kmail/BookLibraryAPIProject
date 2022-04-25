using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Repository 
{
    public interface IBookRepository  :IDisposable
    {
        void Create(BookEntity book);
        Task<BookEntity> GetById(int bookId);
        Task<BookEntity> GetByIsbn(string isbn);
        //Task<List<BookEntity>> GetAllWithFilter(string isbn, string name, double? price, double? quantity);
        Task<List<BookEntity>> GetAll();
        void Update(BookEntity book);
        void Delete(BookEntity book);
    }
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _library; 
        public BookRepository(LibraryContext library)
        {
            _library = library;
        }

        public void Dispose()
        {
            _library.Dispose();
        }
        //public async Task<List<BookEntity>> GetAllWithFilter(string? isbn, string? name, double? price, double? quantity)
        //{
        //    var books =  _library.Books.Where(e =>
        //                                          (string.IsNullOrEmpty(isbn) || e.Isbn.Equals(isbn))
        //                                        ||( string.IsNullOrEmpty(name) || e.Name.Contains(name))
        //                                        || ( price == null || e.Price == price)
        //                                        || ( quantity == null || e.Quantity == quantity)
        //                                     ).ToList();
        //    return  books;
            
        //}
        public async Task<List<BookEntity>> GetAll()
        {
            var books = _library.Books.ToList();
            return books;
        }
        public async Task<BookEntity> GetById(int bookId)
        {
            return _library.Books.FirstOrDefault(book => book.Id == bookId);
        }
        public async Task<BookEntity> GetByIsbn(string isbn)
        {
            return _library.Books.FirstOrDefault(book => book.Isbn == isbn);
        }

        public void Create(BookEntity book)
        {
             _library.Books.Add(book);
             _library.SaveChanges();
            
        }
        public void Update(BookEntity book)
        {          
            _library.Books.Update(book);          
            _library.SaveChanges();
           

        }

        public void Delete(BookEntity book)
        {
            _library.Books.Remove(book);
            _library.SaveChanges();
        }

       
    }
}
