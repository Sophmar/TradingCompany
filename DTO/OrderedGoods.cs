namespace DTO
{
    public class OrderedGoods
    {
        public int Order_Id { get; set; }
        public int Goods_Id { get; set; }
        public decimal Cost_Lost { get; set; }
        public int Amount_Get { get; set; }
        public DateTime Date { get; set; }
    }
}
