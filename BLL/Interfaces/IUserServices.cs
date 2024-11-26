using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserServices
    {
        User GetUserById(int id);
        User GetUserByLogin(string login);
        int Authentication(string login, string password);
        public int Add(User user, string password);
    }
}
