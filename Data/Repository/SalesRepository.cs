using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ISalesRepository : IDisposable
    {
        Task<SalesEntity> Create(SalesEntity sales);
        Task<List<SalesEntity>> GetAll();
        Task<SalesEntity> GetById(int salesId);
        Task<List<SalesEntity>> GetByBookId(int bookId);
        Task<List<SalesEntity>> GetByCustomerId(int customerId);
        void Update(SalesEntity sales);
        void Delete(SalesEntity sales);
    }
   public class SalesRepository : ISalesRepository
    {
        private readonly LibraryContext _library;
        public SalesRepository(LibraryContext library)
        {
            _library = library;
        }
        public void Dispose()
        {
            _library.Dispose();
        }
        public async Task<List<SalesEntity>> GetAll()
        {          
            var sales= _library.Sales.Include(e => e.Book)
                                      .Include(e => e.Customer)
                                      .ToList();
            return sales;

        }
        public async Task<SalesEntity> GetById(int salesId)
        {
            return   _library.Sales.Include(e => e.Book).Include(e => e.Customer).FirstOrDefault(sales => sales.Id == salesId);
        }
        public async Task<List<SalesEntity>> GetByBookId(int bookId)
        {
            return _library.Sales.Where(sales => sales.BookId == bookId).Include(e => e.Book)
                                                                        .Include(e => e.Customer)
                                                                        .ToList(); 
        }
        public async Task<List<SalesEntity>> GetByCustomerId(int customerId)
        {
            return _library.Sales.Where(sales => sales.CustomerId == customerId).Include(e => e.Book)
                                                                                .Include(e => e.Customer)
                                                                                .ToList(); 
        }
        public async Task<SalesEntity> Create(SalesEntity sales)
        {
           _library.Sales.Add(sales);
           _library.SaveChanges();
            return sales;
        }
        public void Update(SalesEntity sales)
        {
            _library.Sales.Update(sales);
            _library.SaveChanges();
        }

        public void Delete(SalesEntity sales)
        {
            _library.Sales.Remove(sales);
            _library.SaveChanges();
        }
    }
}
