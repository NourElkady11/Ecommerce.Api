namespace Domain.Entities.OrderEntities
{
    public class AddressOfOrder
    {
        public AddressOfOrder()
        {
            
        }
        public AddressOfOrder(string username, string street, string city, string country)
        {
            Username = username;
            Street = street;
            City = city;
            Country = country;
         
        }

        public string Username { get; set; }//to specify the name of the user who want to order 
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
       
    }
}