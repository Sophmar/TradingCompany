using BLL.Interfaces;
using DAL;
using DAL.Concrete;
using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class UserServices : IUserServices
    {
        private readonly IUserDAL _userDal;
        public UserServices(IUserDAL userDal)
        {
            //_userDal = userDal;
            _userDal = userDal ?? throw new ArgumentNullException(nameof(UserDAL));
        }

        public User GetUserById(int id)
        {
            return _userDal.GetById(id);
        }
        public User GetUserByLogin(string login)
        {
            return _userDal.GetByLogin(login);
        }

        public int Authentication(string login, string password)
        {
            return _userDal.Authentication(login, password);
        }
        public int Add(User user, string password)
        {
            return _userDal.Add(user, password);
        }
    }
}