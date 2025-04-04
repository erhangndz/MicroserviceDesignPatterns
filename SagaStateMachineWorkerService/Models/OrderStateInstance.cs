using System.Text;
using MassTransit;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string CustomerId { get; set; }
        public int OrderId { get; set; }
        public string CardholdersName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; }

        public override string ToString()
        {
            var properties = GetType().GetProperties();

            var stringBuilder = new StringBuilder();

            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this);
                stringBuilder.AppendLine($"{propertyInfo.Name}: {value}");
            }
            stringBuilder.AppendLine("---------------------");
            return stringBuilder.ToString();
        }
    }
}
