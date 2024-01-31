namespace ProvaPub.Services
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(decimal paymentValue, int customerId);
    }
}
