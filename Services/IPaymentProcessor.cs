using ProvaPub.Models;

namespace ProvaPub.Services
{
    public interface IPaymentProcessor
    {
        Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId);
    }
}
