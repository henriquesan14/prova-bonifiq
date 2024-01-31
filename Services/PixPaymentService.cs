
namespace ProvaPub.Services
{
    public class PixPaymentService : IPaymentService
    {
        public Task ProcessPaymentAsync(decimal paymentValue, int customerId)
        {
            //Faz pagamento via pix...
            return Task.CompletedTask;
        }
    }
}
