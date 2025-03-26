namespace Shared
{
    public class PaymentMessage
    {
        public string CardholdersName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
