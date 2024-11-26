using BLL.Interfaces;
using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class OrderedGoodsServices:IOrderedGoodsServices
    {
        private readonly OrderGoodsDAL _orederDal;
        public OrderedGoodsServices(OrderGoodsDAL orederDal)
        {
            _orederDal = orederDal;
        }
        public List<OrderedGoods> GetAll() => _orederDal.GetAll();

    }
}
