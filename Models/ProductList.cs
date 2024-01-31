namespace ProvaPub.Models
{
	public class ProductList
	{
        public ProductList(List<Product> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Products = items;
        }

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public List<Product> Products { get; set; }
		public int TotalCount { get; set; }
        public bool HasNext => CurrentPage < TotalPages;
    }
}
