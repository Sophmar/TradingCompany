using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public int Add(User user)
        {
            int newId;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("INSERT INTO tblUsers " +
                    "(login, password) " +
                    "OUTPUT INSERTED.user_id " +
                    "Values (@Login, @Password);", con))
                {
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@Password", user.Password);
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
                                Login = dataReader["login"].ToString(),
                                Password = dataReader["password"].ToString()
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
                            ur.Password = Convert.ToString(dataReader["password"]);
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
                    "SET user_id = @(user.Login), password = @(user.Password) " +
                    "WHERE user_id = @(user.UserId);", con))
                {
                    command.Parameters.AddWithValue("@user.Login", user.Login);
                    command.Parameters.AddWithValue("@user.Password", user.Password);
                    command.Parameters.AddWithValue("@user.UserId", user.UserId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }
    }
}
