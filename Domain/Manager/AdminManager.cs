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
    public interface IAdminManager
    {
        bool Create(AdminModel admin);
        List<AdminModel> GetAll();
        bool Update(AdminModel admin);
        bool Delete(int adminId);
        AdminModel login(LoginModel loginModel);
        AdminModel GetById(int adminId);
    }
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        public AdminManager(IAdminRepository adminRepository , IMapper mapper )
        {
             _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public AdminModel login(LoginModel loginModel)
        {
            var admin = _adminRepository.GetAdminByUSerName(loginModel.UserName);
            if (admin != null &&  admin.Password.Equals(loginModel.Password))
            {
               var adminToReturn =  _mapper.Map<AdminModel>(admin);
                adminToReturn.Password = "";
                return adminToReturn;
            }
            return null;
        }

        public List<AdminModel> GetAll()
        {
            return _mapper.Map<List<AdminModel>>(_adminRepository.GetAll());
        }
        public AdminModel GetById(int adminId)
        {
            return _mapper.Map<AdminModel>(_adminRepository.GetById(adminId));
        }

        public bool Create(AdminModel admin)
        {
            var adminEn = _mapper.Map<AdminEntity>(admin);
            _adminRepository.Create(adminEn);
            return true;
        }
        public bool Update(AdminModel admin)
        {
            var adminEn = _mapper.Map<AdminEntity>(admin);
            _adminRepository.Update(adminEn);
            return true;
        }

        public bool Delete(int adminId)
        {
            var admin = _adminRepository.GetById(adminId);
            if (admin != null)
            {
                _adminRepository.Delete(admin);
                return true;
            }
            return false;
        }
    }
}
