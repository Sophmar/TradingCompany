using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompanyWpf.Models
{
    public class OrderedGoodsModel
    {
        public int Id { get; set; }
        public string GoodsName { get; set; }
        public decimal Cost {  get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
    public static class OrderMapper
    {
        public static DTO.OrderedGoods MapToOrder(OrderedGoodsModel orderModel)
        {
            return new DTO.OrderedGoods
            {
                Order_Id = orderModel.Id,
                Goods_Name = orderModel.GoodsName,
                Cost_Lost = orderModel.Cost,
                Amount_Get = orderModel.Amount,
                Date = orderModel.Date
            };

        }

        public static OrderedGoodsModel MapToOrderModel(DTO.OrderedGoods order)
        {
            return new OrderedGoodsModel
            {
                Id = order.Order_Id,
                GoodsName = order.Goods_Name,
                Cost = order.Cost_Lost,
                Amount = order.Amount_Get,
                Date = order.Date
            };
        }
    }
}
