using BLL.Interfaces;
using DAL;
using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class SoldGoodsServices:ISoldGoodsServices
    {
        private readonly ISoldGoodsDAL _sellDal;
        public SoldGoodsServices(ISoldGoodsDAL sellDal)
        {
            _sellDal = sellDal ?? throw new ArgumentNullException(nameof(SoldGoodsDAL));
            //_sellDal = sellDal;
        }
        public List<SoldGoods> GetAll() => _sellDal.GetAll();
        public int Add(SoldGoods soldGoods) => _sellDal.Add(soldGoods);
        public SoldGoods GetById(int id) => _sellDal.GetById(id);
        public bool Update(SoldGoods soldGoods, int id) => _sellDal.Update(soldGoods, id);
        public bool DeleteById(int id) => _sellDal.DeleteById(id);

    }
}
