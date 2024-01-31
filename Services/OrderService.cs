using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService : IOrderService
	{

		private readonly IPaymentProcessor _paymentProcessor;

        public OrderService(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{

            await _paymentProcessor.PayOrder(paymentMethod, paymentValue, customerId);

			return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}
