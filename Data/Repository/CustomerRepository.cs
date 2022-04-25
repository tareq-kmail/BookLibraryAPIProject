using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        void Create(CustomerEntity customer);
        Task<CustomerEntity> GetById(int customerId);
        Task<CustomerEntity> GetByNationalId(string nationalId);
        //Task<List<BookEntity>> GetAllWithFilter(string isbn, string name, double? price, double? quantity);
        Task<List<CustomerEntity>> GetAll();
        void Update(CustomerEntity customer);
        void Delete(CustomerEntity customer);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LibraryContext _library;
        public CustomerRepository(LibraryContext library)
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
        public async Task<List<CustomerEntity>> GetAll()
        {
            var customers = _library.Customers.ToList();
            return customers;
        }
        public async Task<CustomerEntity> GetById(int customerId)
        {
            return _library.Customers.FirstOrDefault(customer => customer.Id == customerId);
        }
        public async Task<CustomerEntity> GetByNationalId(string nationalId)
        {
            return _library.Customers.FirstOrDefault(customer => customer.NationalId == nationalId);
        }

        public void Create(CustomerEntity customer)
        {
            _library.Customers.Add(customer);
            _library.SaveChanges();

        }
        public void Update(CustomerEntity customer)
        {
            _library.Customers.Update(customer);
            _library.SaveChanges();


        }

        public void Delete(CustomerEntity customer)
        {
            _library.Customers.Remove(customer);
            _library.SaveChanges();
        }


    }
}
