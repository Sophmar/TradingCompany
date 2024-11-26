using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompanyWpf.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int AccessId { get; set; }
    }
    public static class UserMapper
    {
        public static User MapToUser(UserModel userModel)
        {
            return new User
            {
                UserId = userModel.Id,
                Login = userModel.Login
            };
        }

        public static UserModel MapToUserModel(User user, int accessId = 0)
        {
            return new UserModel
            {
                Id = user.UserId,        
                Login = user.Login,      
                Password = null,         
                Salt = null,              
                AccessId = accessId 
            };
        }
    }
}
