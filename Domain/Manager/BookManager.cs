using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using AutoMapper;
using Data.Entity;

namespace Domain.Manager
{
    public interface IBookManager
    {
        Task<BookModel> Create(BookModel book);
        Task<BookModel> GetById(int bookId);
        Task<BookModel> GetByIsbn(string isbn);
        //Task<List<BookModel>> GetAll(BookFilterModel filter);
        Task<List<BookModel>> GetAll();
        Task<BookModel> Update(BookModel book);  
        bool Delete(int bookId);  
    }
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper; 
        public BookManager(IBookRepository bookRepository , IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<BookModel> GetById(int bookId)
        {
          return  _mapper.Map<BookModel>(await _bookRepository.GetById(bookId));
        }
        public async Task<BookModel> GetByIsbn(string isbn)
        {
            return _mapper.Map<BookModel>(await _bookRepository.GetByIsbn(isbn));
        }

        //public async Task<List<BookModel>> GetAll(BookFilterModel filter)
        //{
        //    return   _mapper.Map<List<BookModel>>(await _bookRepository.GetAllWithFilter(filter.Isbn,filter.Name,filter.Price,filter.Quantity));   
        //}
        public async Task<List<BookModel>> GetAll()
        {
            return _mapper.Map<List<BookModel>>(await _bookRepository.GetAll());
        }
        public async Task<BookModel> Create(BookModel book)
        {
            var bookEn = _mapper.Map<BookEntity>(book);
            _bookRepository.Create(bookEn);
            var books = _bookRepository.GetById(bookEn.Id).Result;
            return   _mapper.Map<BookModel>(books); 
        }
        public async Task<BookModel> Update(BookModel book)
        {
            var toUPdate = _bookRepository.GetById(book.Id).Result;
            toUPdate.Name = book.Name;
            toUPdate.Price = book.Price;
            toUPdate.Quantity = book.Quantity;
            _bookRepository.Update(toUPdate);
            return _mapper.Map<BookModel>(_bookRepository.GetById(toUPdate.Id).Result);
        }

        public bool Delete(int bookId)
        {
            var book = _bookRepository.GetById(bookId).Result;
            if (book != null)
            {
                book.Quantity = 0;
                _bookRepository.Update(book);
                return true; 
            }
            return false;
        }
    }
}
