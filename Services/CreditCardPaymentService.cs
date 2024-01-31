
namespace ProvaPub.Services
{
    public class CreditCardPaymentService : IPaymentService
    {
        public Task ProcessPaymentAsync(decimal paymentValue, int customerId)
        {
            //Faz pagamento via credit card...
            return Task.CompletedTask;
        }
    }
}
