using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class SoldGoodsDAL : ISoldGoodsDAL
    {
        string connectionString;
        public SoldGoodsDAL(string conString)
        {
            connectionString = conString;
        }
        public int Add(SoldGoods sale)
        {
            int newId = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("INSERT INTO tblSoldGoods " +
                    "(goods_name, cost_get, amount_lost, date) " +
                    "OUTPUT INSERTED.sell_id " +
                    "Values (@goodsName, @costGet, " +
                    "@amountLost, @date);", con))
                {
                    command.Parameters.AddWithValue("@goodsName", sale.Goods_Name);
                    command.Parameters.AddWithValue("@costGet", sale.Cost_Get);
                    command.Parameters.AddWithValue("@amountLost", sale.Amount_Lost);
                    command.Parameters.AddWithValue("@date", sale.Date);
                    newId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return newId;

        }

        public List<SoldGoods> GetAll()
        {
            List<SoldGoods> sales = new List<SoldGoods>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblSoldGoods;", con))
                 {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                SoldGoods sd = new SoldGoods
                                {
                                    Sell_Id = Convert.ToInt32(dataReader["sell_id"]),
                                    Goods_Name = dataReader["goods_name"].ToString(),
                                    Cost_Get = Convert.ToDecimal(dataReader["cost_get"]),
                                    Amount_Lost = Convert.ToInt32(dataReader["amount_lost"]),
                                    Date = Convert.ToDateTime(dataReader["date"])
                                };
                                sales.Add(sd);
                            }
                        }
                    }
            }

            return sales;
        }


        public SoldGoods GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblSoldGoods WHERE sell_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        SoldGoods sd = new SoldGoods();
                        if (dataReader.Read())
                        {
                            sd.Sell_Id = Convert.ToInt32(dataReader["sell_id"]);
                            sd.Goods_Name = dataReader["goods_name"].ToString();
                            sd.Cost_Get = Convert.ToDecimal(dataReader["cost_get"]);
                            sd.Amount_Lost = Convert.ToInt32(dataReader["amount_lost"]);
                            sd.Date = Convert.ToDateTime(dataReader["date"]);
                            return sd;
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
                using (var command = new SqlCommand("DELETE FROM tblSoldGoods WHERE sell_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }

        public bool Update(SoldGoods sale, int id)
        {
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("UPDATE tblSoldGoods " +
                    "SET goods_name = @goodsName, cost_get = @costGet, " +
                    "amount_lost = @amountLost, date = @date " +
                    "WHERE sell_id = @Id", con))
                {
                    command.Parameters.AddWithValue("@goodsName", sale.Goods_Name);
                    command.Parameters.AddWithValue("@costGet", sale.Cost_Get);
                    command.Parameters.AddWithValue("@amountLost", sale.Amount_Lost);
                    command.Parameters.AddWithValue("@date", sale.Date);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }

        public List<SoldGoods> GetAllSortedBy(int column)
        {
            string col = "";
            List<SoldGoods> sales = new List<SoldGoods>();
            SoldGoods sd = null;
            switch (column)
            {
                case 1:
                    col = "sell_id";
                    break;
                case 2:
                    col = "goods_name";
                    break;
                case 3:
                    col = "cost_get";
                    break;
                case 4:
                    col = "amount_lost";
                    break;
                case 5:
                    col = "date";
                    break;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand($"SELECT * FROM tblSoldGoods ORDER BY {col};", con))
                {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            sd = new SoldGoods();
                            sd.Sell_Id = Convert.ToInt32(dataReader["sell_id"]);
                            sd.Goods_Name = dataReader["goods_name"].ToString();
                            sd.Cost_Get = Convert.ToDecimal(dataReader["cost_get"]);
                            sd.Amount_Lost = Convert.ToInt32(dataReader["amount_lost"]);
                            sd.Date = Convert.ToDateTime(dataReader["date"]);
                            sales.Add(sd);
                        }
                    }
                }
            }
            return sales;
        }
    }
}
