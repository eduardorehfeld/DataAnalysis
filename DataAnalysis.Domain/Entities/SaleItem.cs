namespace DataAnalysis.Domain.Entities
{
    public class SaleItem
    {
        public SaleItem(int id, long quantity, double price)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}
