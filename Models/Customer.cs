namespace ProvaPub.Models
{
	public class Customer : Entity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
