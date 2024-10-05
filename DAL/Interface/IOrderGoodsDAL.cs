using DTO;

namespace DAL
{
    public interface IOrderGoodsDAL
    {
        public int Add(OrderedGoods order);
        public bool DeleteById(int id);
        public List<OrderedGoods> GetAll();
        public OrderedGoods GetById(int id);
        public bool Update(OrderedGoods order, int id);
        public List<OrderedGoods> GetAllSortedBy(int column);
    }
}
