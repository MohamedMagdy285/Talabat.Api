namespace Talab.Api.Dtos
{
    public class OrderDto
    {
        public string BasketId{ get; set; }
        public int DeliverMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
