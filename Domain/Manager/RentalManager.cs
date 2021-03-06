using AutoMapper;
using Data.Entity;
using Data.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Manager
{
    public interface IRentalManager
    {
        Task<RentalModel> Create(RentalModel rental);
        Task<List<RentalModel>> GetAll();
        Task<RentalModel> GetById(int rentalId);
        Task<List<RentalModel>> GetByBookId(int bookId);
        Task<List<RentalModel>> GetByCustomerId(int customerId);
        Task<RentalModel> Update(RentalModel rental);
        bool Delete(int rentalId);
    }
    public class RentalManager : IRentalManager
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookManager _bookManager;
        private readonly IMapper _mapper;
        public RentalManager(IRentalRepository rentalRepository, IBookManager bookManager, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _bookManager = bookManager;
            _mapper = mapper;
        }
        public async Task<List<RentalModel>> GetAll()
        {
            return _mapper.Map<List<RentalModel>>(await _rentalRepository.GetAll());
        }
        public async Task<RentalModel> GetById(int id)
        {
            return _mapper.Map<RentalModel>(await _rentalRepository.GetById(id));//await same as Result
        }
        public async Task<List<RentalModel>> GetByBookId(int id)
        {
            return _mapper.Map<List<RentalModel>>(_rentalRepository.GetByBookId(id).Result);
        }
        public async Task<List<RentalModel>> GetByCustomerId(int id)
        {
            return _mapper.Map<List<RentalModel>>(_rentalRepository.GetByCustomerId(id).Result);
        }
        public async Task<RentalModel> Create(RentalModel rental)
        {
            var rentalEn = _mapper.Map<RentalEntity>(rental);
            await _rentalRepository.Create(rentalEn);
            var sale = _rentalRepository.GetById(rentalEn.Id).Result;
            var book = _bookManager.GetById(rental.BookId).Result;
            book.Quantity = book.Quantity - 1;
            var bookUp = _bookManager.Update(book).Result;
            return _mapper.Map<RentalModel>(sale);
        }
        public async Task<RentalModel> Update(RentalModel rental)
        {
            var toUPdate = _rentalRepository.GetById(rental.Id).Result;
            toUPdate.Price = rental.Price;
            toUPdate.BookingExpiryDate = rental.BookingExpiryDate;
            _rentalRepository.Update(toUPdate);
            return _mapper.Map<RentalModel>(_rentalRepository.GetById(toUPdate.Id).Result);


        }

        public bool Delete(int rentalId)
        {
            var rental = _rentalRepository.GetById(rentalId).Result;
            if (rental != null)
            {
                _rentalRepository.Delete(rental);
                return true;
            }
            return false;
        }
    }
}
