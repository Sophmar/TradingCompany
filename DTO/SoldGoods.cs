namespace DTO
{
    public class SoldGoods
    {
        public int Sell_Id { get; set; }
        public string Goods_Name { get; set; }
        public decimal Cost_Get { get; set; }
        public int Amount_Lost { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
