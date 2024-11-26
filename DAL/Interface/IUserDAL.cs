using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserDAL
    {
        public string HashPasswordWithSalt(string password, string salt);
        public string GenerateSalt(int size = 32);
        public int Add(User user, string password);
        public bool DeleteById(int id);
        public List<User> GetAll();
        public User GetById(int id);
        public bool Update(User user);
        public User GetByLogin(string login);
        public int Authentication(string login, string password);

    }
}
