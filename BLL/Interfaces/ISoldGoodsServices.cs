using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISoldGoodsServices
    {
        public List<SoldGoods> GetAll();
        public int Add(SoldGoods soldGoods);
        public SoldGoods GetById(int id);
        public bool Update(SoldGoods soldGoods, int id);
        public bool DeleteById(int id); 
    }
}
