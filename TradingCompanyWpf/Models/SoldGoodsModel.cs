using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompanyWpf.Models
{
    public class SoldGoodsModel
    {
        public int Id { get; set; }
        public string GoodsName { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }

    }

    public static class SellMapper
    {
        public static DTO.SoldGoods MapToSell(SoldGoodsModel sellModel)
        {
            return new DTO.SoldGoods
            {
                Sell_Id = sellModel.Id,
                Goods_Name = sellModel.GoodsName,
                Cost_Get = sellModel.Cost,
                Amount_Lost = sellModel.Amount,
                Date = sellModel.Date
            };

        }

        public static SoldGoodsModel MapToSellModel(DTO.SoldGoods sell)
        {
            return new SoldGoodsModel
            {
                Id = sell.Sell_Id,
                GoodsName = sell.Goods_Name,
                Cost = sell.Cost_Get,
                Amount = sell.Amount_Lost,
                Date = sell.Date
            };
        }
    }
}
