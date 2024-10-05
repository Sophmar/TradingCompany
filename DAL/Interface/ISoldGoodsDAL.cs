using DTO;

namespace DAL
{
    public interface ISoldGoodsDAL
    {
        public int Add(SoldGoods oreder);
        public bool DeleteById(int id);
        public List<SoldGoods> GetAll();
        public SoldGoods GetById(int id);
        public bool Update(SoldGoods order, int id);
        public List<SoldGoods> GetAllSortedBy(int column);
    }
}
