using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TradingCompanyApp.Models
{
    public class EditSoldGoodsModel
    {
        public EditSoldGoodsModel()
        {
            Goods_Name = string.Empty;
        }
        public int Sell_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Goods_Name { get; set; }
        [Required]
        [DisplayName("Cost")]
        [Range(1, int.MaxValue, ErrorMessage = "Cost must be greater than 0.")]
        public decimal Cost_Get { get; set; }
        [Required]
        [DisplayName("Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public int Amount_Lost {  get; set; }
    }
}
