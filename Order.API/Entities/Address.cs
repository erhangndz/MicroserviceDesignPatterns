using Microsoft.EntityFrameworkCore;

namespace Order.API.Entities
{

    [Owned]
    public class Address
    {
        public string Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
