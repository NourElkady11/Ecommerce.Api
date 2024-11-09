namespace Domain.Entities.OrderEntities
{
    public class OrderItems:BaseEntity<Guid>
    {
        public int ProductsId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
/*
        public Order order { get; set; }
        public Guid? Orderid { get; set; }*/

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

