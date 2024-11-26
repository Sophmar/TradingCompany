using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UserDAL : IUserDAL
    {
        string connectionString;
        public UserDAL(string conString)
        {
            connectionString = conString;
        }
        public int Add(User user, string password)
        {
            int newId = 0;
            var salt = GenerateSalt();
            var passwd = HashPasswordWithSalt(password, salt);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("INSERT INTO tblUsers " +
                    "(login, password, salt, access_id) " +
                    "OUTPUT INSERTED.user_id " +
                    "Values (@Login, @passwd, @salt, 0);", con))
                {
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@passwd", passwd);
                    command.Parameters.AddWithValue("@salt", salt);
                    newId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return newId;
        }
        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM tblUsers;", con))
                {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            User ur = new User
                            {
                                UserId = Convert.ToInt32(dataReader["user_id"]),
                                Login = dataReader["login"].ToString()

                            };
                            users.Add(ur);
                        }
                    }
                }
            }

            return users;
        }

        public User GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblUsers WHERE user_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        User ur = new User();
                        if (dataReader.Read())
                        {
                            ur.UserId = Convert.ToInt32(dataReader["user_id"]);
                            ur.Login = Convert.ToString(dataReader["login"]);
                            return ur;
                        }
                    }
                }
                return null;
            }
        }

        public bool DeleteById(int id)
        {
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("DELETE FROM tblUsers WHERE user_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }

        public bool Update(User user)
        {
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("UPDATE tblUsers " +
                    "SET login = @(user.Login) " +
                    "WHERE user_id = @(user.UserId);", con))
                {
                    command.Parameters.AddWithValue("@user.Login", user.Login);
                    command.Parameters.AddWithValue("@user.UserId", user.UserId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }
        public User GetByLogin(string login)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblUsers WHERE login = @login;", con))
                {
                    command.Parameters.AddWithValue("@login", login);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        User ur = new User();
                        if (dataReader.Read())
                        {
                            ur.UserId = Convert.ToInt32(dataReader["user_id"]);
                            ur.Login = Convert.ToString(dataReader["login"]);
                            return ur;
                        }
                    }
                }
                return null;
            }
        }
        public int Authentication(string login, string password)
        {
            string storedHashedPassword = null;
            string storedSalt = null;
            int accessId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT password, salt, access_id FROM \"tblUsers\" WHERE login = @Login", connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHashedPassword = reader["password"].ToString();
                            storedSalt = reader["salt"].ToString();
                            accessId = Convert.ToInt32(reader["access_id"]);
                        }
                    }
                }
            }
            if (storedHashedPassword == null || storedSalt == null)
            {
                return -1;
            }
            string hashedInputPassword = HashPasswordWithSalt(password, storedSalt);

            if (hashedInputPassword == storedHashedPassword)
                return accessId;
            return -1;
        }
        public string HashPasswordWithSalt(string password, string salt)
        {
            var passwdWithSalt = password + salt;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(passwdWithSalt);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string GenerateSalt(int size = 32)
        {
            byte[] saltBytes = new byte[size];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}
