using BLL.Interfaces;
using DAL.Concrete;
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
        private readonly UserDAL _userDal;
        public UserServices(UserDAL userDal)
        {
            _userDal = userDal;
        }

        public User GetUserById(int id)
        {
            return _userDal.GetById(id);
        }
        public User GetUserByLogin(string login)
        {
            return _userDal.GetByLogin(login);
        }

        bool Authentication(string login, string password)
        {
            var users = _userDal.GetPasswordByLogin(login);
            string passwd = null;
            string salt = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblUsers WHERE login = @login;", con))
                {
                    command.Parameters.AddWithValue("@login", login);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            passwd = Convert.ToString(dataReader["password"]);
                            salt = Convert.ToString(dataReader["salt"]);

                        }
                    }
                }
            }
            foreach (var user in users)
            {
                if (user.Login == login && passwd == password)
                    return true;
            }
            return false;
        }
    }

        string ReadPasswordHash(string salt)
    {
        var passwd = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && passwd.Length > 0)
            {
                Console.Write("\b \b");
                passwd = passwd[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                passwd += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        string password = passwd + salt;
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }
    }
}
