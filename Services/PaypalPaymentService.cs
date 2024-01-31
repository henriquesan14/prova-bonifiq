
namespace ProvaPub.Services
{
    public class PaypalPaymentService : IPaymentService
    {
        public Task ProcessPaymentAsync(decimal paymentValue, int customerId)
        {
            //Faz pagamento via pay pal...
            return Task.CompletedTask;
        }
    }
}
