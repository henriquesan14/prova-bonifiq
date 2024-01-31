using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly Dictionary<string, IPaymentService> paymentProcessors = new Dictionary<string, IPaymentService>
{
            { "pix", new PixPaymentService() },
            { "creditcard", new CreditCardPaymentService() },
            { "paypal", new PaypalPaymentService() }
        };

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            if (!paymentProcessors.TryGetValue(paymentMethod.ToLower(), out var paymentProcessor))
            {
                throw new ArgumentException("Método de pagamento não suportado", nameof(paymentMethod));
            }

            await paymentProcessor.ProcessPaymentAsync(paymentValue, customerId);

            return new Order
            {
                Value = paymentValue
            };
        }
    }
}
