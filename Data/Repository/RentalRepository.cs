using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRentalRepository : IDisposable
    {
        Task<RentalEntity> Create(RentalEntity rental);
        Task<List<RentalEntity>> GetAll();
        Task<RentalEntity> GetById(int rentalId);
        Task<List<RentalEntity>> GetByBookId(int bookId);
        Task<List<RentalEntity>> GetByCustomerId(int customerId);
        void Update(RentalEntity rental);
        void Delete(RentalEntity rental);
    }
    public class RentalRepository : IRentalRepository
    {
        private readonly LibraryContext _library;
        public RentalRepository(LibraryContext library)
        {
            _library = library;
        }
        public void Dispose()
        {
            _library.Dispose();
        }
        public async Task<List<RentalEntity>> GetAll()
        {
            var rental = _library.Rental.Include(e => e.Book)
                                      .Include(e => e.Customer)
                                      .ToList();
            return rental;

        }
        public async Task<RentalEntity> GetById(int rentalId)
        {
            return _library.Rental.Include(e => e.Book).Include(e => e.Customer).FirstOrDefault(rental => rental.Id == rentalId);
        }
        public async Task<List<RentalEntity>> GetByBookId(int bookId)
        {
            return _library.Rental.Where(rental => rental.BookId == bookId).Include(e => e.Book)
                                                                        .Include(e => e.Customer)
                                                                        .ToList();
        }
        public async Task<List<RentalEntity>> GetByCustomerId(int customerId)
        {
            return _library.Rental.Where(rental => rental.CustomerId == customerId).Include(e => e.Book)
                                                                                .Include(e => e.Customer)
                                                                                .ToList();
        }
        public async Task<RentalEntity> Create(RentalEntity rental)
        {
            _library.Rental.Add(rental);
            _library.SaveChanges();
            return rental;
        }
        public void Update(RentalEntity rental)
        {
            _library.Rental.Update(rental);
            _library.SaveChanges();
        }

        public void Delete(RentalEntity rental)
        {
            _library.Rental.Remove(rental);
            _library.SaveChanges();
        }
    }
}
