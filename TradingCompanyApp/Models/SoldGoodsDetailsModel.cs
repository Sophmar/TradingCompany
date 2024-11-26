using System.ComponentModel;

namespace TradingCompanyApp.Models
{
    public class SoldGoodsDetailsModel
    {
        public SoldGoodsDetailsModel()
        {
            Goods_Name = string.Empty;
        }
        public int Sell_Id {  get; set; }
        [DisplayName("Name")]
        public string Goods_Name { get; set; }
        [DisplayName("Cost")]
        public decimal Cost_Get {  get; set; }
        [DisplayName("Amount")]
        public int Amount_Lost {  get; set; }
    }
}
