using DTO;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DAL.Concrete
{
    public class OrderGoodsDAL : IOrderGoodsDAL
    {
        string connectionString;
        public OrderGoodsDAL(string conString)
        {
            connectionString = conString;
        }
        public int Add(OrderedGoods order)
        {
            int newId;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("INSERT INTO tblOrderedGoods " +
                    "(goods_id, cost_lost, amount_get, date) " +
                    "OUTPUT INSERTED.order_id " +
                    "Values (@goodsId, @costLost, " +
                    "@amountGet, @date);", con))
                {
                    command.Parameters.AddWithValue("@goodsId", order.Goods_Id);
                    command.Parameters.AddWithValue("@costLost", order.Cost_Lost);
                    command.Parameters.AddWithValue("@amountGet", order.Amount_Get);
                    command.Parameters.AddWithValue("@date", order.Date);
                    newId = Convert.ToInt32(command.ExecuteScalar());
                    
                }
            }
            return newId;

        }

        public List<OrderedGoods> GetAll()
        {
            List<OrderedGoods> goods = new List<OrderedGoods>();
            OrderedGoods gd = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblOrderedGoods;", con))
                {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            gd = new OrderedGoods();
                            gd.Order_Id = Convert.ToInt32(dataReader["order_id"]);
                            gd.Goods_Id = Convert.ToInt32(dataReader["goods_id"]);
                            gd.Cost_Lost = Convert.ToDecimal(dataReader["cost_lost"]);
                            gd.Amount_Get = Convert.ToInt32(dataReader["amount_get"]);
                            gd.Date = Convert.ToDateTime(dataReader["date"]);
                            goods.Add(gd);
                        }
                    }
                }
                return goods;
            }
        }

        public OrderedGoods GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT * FROM tblOrderedGoods WHERE order_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    { 
                        OrderedGoods gd = new OrderedGoods();
                        if (dataReader.Read())
                        {
                            gd.Order_Id = Convert.ToInt32(dataReader["order_id"]);
                            gd.Goods_Id = Convert.ToInt32(dataReader["goods_id"]);
                            gd.Cost_Lost = Convert.ToDecimal(dataReader["cost_lost"]);
                            gd.Amount_Get = Convert.ToInt32(dataReader["amount_get"]);
                            gd.Date = Convert.ToDateTime(dataReader["date"]);

                            return gd;
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
                using (var command = new SqlCommand("DELETE FROM tblOrderedGoods WHERE order_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }

        public bool Update(OrderedGoods order, int id)
        {
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("UPDATE tblOrderedGoods " +
                    "SET goods_id = @goodsId, cost_lost = @costLost, " +
                    "amount_get = @amountGet, date = @date" +
                    "WHERE order_id = @orderId;", con))
                {
                    command.Parameters.AddWithValue("@goodsId", order.Goods_Id);
                    command.Parameters.AddWithValue("@costLost", order.Cost_Lost);
                    command.Parameters.AddWithValue("@amountGet", order.Amount_Get);
                    command.Parameters.AddWithValue("@date", order.Date);
                    command.Parameters.AddWithValue("@orderId", order.Order_Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        res = true;
                }
            }
            return res;
        }
        public List<OrderedGoods> GetAllSortedBy(int column)
        {
            string col = "";
            List<OrderedGoods> goods = new List<OrderedGoods>();
            OrderedGoods gd = null;
            switch (column)
            {
                case 1:
                    col = "order_id";
                    break;
                case 2:
                    col = "goods_id";
                    break;
                case 3:
                    col = "cost_lost";
                    break;
                case 4:
                    col = "amount_get";
                    break;
                case 5:
                    col = "date";
                    break;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = $"SELECT * FROM tblOrderedGoods ORDER BY {col};";
                using (var command = new SqlCommand(query, con))
                {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            gd = new OrderedGoods();
                            gd.Order_Id = Convert.ToInt32(dataReader["order_id"]);
                            gd.Goods_Id = Convert.ToInt32(dataReader["goods_id"]);
                            gd.Cost_Lost = Convert.ToDecimal(dataReader["cost_lost"]);
                            gd.Amount_Get = Convert.ToInt32(dataReader["amount_get"]);
                            gd.Date = Convert.ToDateTime(dataReader["date"]);
                            goods.Add(gd);
                        }
                    }
                }
                return goods;
            }
        }
    }
}
