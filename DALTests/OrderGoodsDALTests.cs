using DAL.Concrete;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DALTests
{
    public class OrderGoodsDALTests
    {/*
        private readonly OrderGoodsDAL dal;
        private List<OrderedGoods> orders = new List<OrderedGoods>();
        string connectionString;
        private DateTime date;
        public OrderGoodsDALTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            connectionString = configuration.GetConnectionString("TradingCompany") ?? "";
            dal = new OrderGoodsDAL(connectionString);
        }

        [SetUp]
        public void Setup()
        {
            date = new DateTime(2024, 10, 8);
        }

        [Test]
        public void AddOrder_ReturnId()
        {
            OrderedGoods og = new OrderedGoods()
            {
                Goods_Name = "Music Box",
                Cost_Lost = 31,
                Amount_Get = 12,
                Date = date
            };
            int id = dal.Add(og);
            int count;
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT COUNT(*) FROM tblOrderedGoods WHERE " +
                    "order_id = @id;", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            Assert.That(1, Is.EqualTo(count));
        }
        [Test]
        public void GetAllOrders_ReturnAll()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("DELETE FROM tblOrderedGoods;", con))
                {
                    command.ExecuteNonQuery();
                }
            }
            List<OrderedGoods> ogs = new List<OrderedGoods>()
            {
                new OrderedGoods()
                {
                    Goods_Name = "Papers",
                    Cost_Lost = 31,
                    Amount_Get = 12,
                    Date = date
                },
                new OrderedGoods()
                {
                    Goods_Name = "Window",
                    Cost_Lost = 20,
                    Amount_Get = 8,
                    Date = date
                },
                new OrderedGoods()
                {
                    Goods_Name = "Doors",
                    Cost_Lost = 15,
                    Amount_Get = 4,
                    Date = date
                },
            };
            //int id = dal.Add(og);
            foreach (var og in ogs)
                dal.Add(og);

            List<OrderedGoods> orders = dal.GetAll();

            Assert.IsNotNull(orders);
            Assert.AreEqual(3, orders.Count());

            Assert.AreEqual("Papers", orders[0].Goods_Name);
            Assert.AreEqual("Window", orders[1].Goods_Name);
            Assert.AreEqual("Doors", orders[2].Goods_Name);
        }
        [Test]
        public void GetByIdOrder_ReturnOrder()
        {
            OrderedGoods og = new OrderedGoods()
            {
                Goods_Name = "Bed",
                Cost_Lost = 13,
                Amount_Get = 5,
                Date = date
            };
            int id = dal.Add(og);
            OrderedGoods gd = dal.GetById(id);
           
            Assert.That("Bed", Is.EqualTo(gd.Goods_Name));
            Assert.That(13, Is.EqualTo(gd.Cost_Lost));
            Assert.That(5, Is.EqualTo(gd.Amount_Get));
        }
        [Test]
        public void UpdateOrder_ReturnUpdated()
        {
            OrderedGoods og = new OrderedGoods()
            {
                Goods_Name = "Refrifirator",
                Cost_Lost = 10,
                Amount_Get = 2,
                Date = date
            };
            int id = dal.Add(og);

            og.Goods_Name = "TV";
            og.Cost_Lost = 11;
            og.Amount_Get = 3;
            dal.Update(og, id);
            OrderedGoods gd = dal.GetById(id);
            Assert.IsNotNull(gd);
            Assert.AreEqual("TV",gd.Goods_Name);
            Assert.AreEqual(11, gd.Cost_Lost);
            Assert.AreEqual(3, gd.Amount_Get);
        }
        [Test]
        public void DeleteOrder_ReturnUpdated()
        {
            OrderedGoods og = new OrderedGoods()
            {
                Goods_Name = "Lipstick",
                Cost_Lost = 14,
                Amount_Get = 7,
                Date = date
            };
            int id = dal.Add(og);
            dal.DeleteById(id);
            OrderedGoods gd = dal.GetById(id);
            
            Assert.IsNull(gd);
        }
        [Test]
        public void SortOrders_ReturnAllSorted()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = new SqlCommand("DELETE FROM tblOrderedGoods;", con))
                {
                    command.ExecuteNonQuery();
                }
            }
            List<OrderedGoods> ogs = new List<OrderedGoods>()
            {
                new OrderedGoods()
                {
                    Goods_Name = "Rock",
                    Cost_Lost = 28,
                    Amount_Get = 16,
                    Date = date
                },
                new OrderedGoods()
                {
                    Goods_Name = "Bag",
                    Cost_Lost = 23,
                    Amount_Get = 4,
                    Date = date
                },
                new OrderedGoods()
                {
                    Goods_Name = "Photo",
                    Cost_Lost = 21,
                    Amount_Get = 7,
                    Date = date
                },
            };
            foreach (var og in ogs)
                dal.Add(og);

            List<OrderedGoods> orders = dal.GetAllSortedBy(2);

            Assert.IsNotNull(orders);
            Assert.AreEqual(3, orders.Count());

            Assert.AreEqual("Rock", orders[0].Goods_Name);
            Assert.AreEqual("Bag", orders[1].Goods_Name);
            Assert.AreEqual("Photo", orders[2].Goods_Name);
        }
    }
        */
    }
}