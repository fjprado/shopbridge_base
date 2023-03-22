namespace Shopbridge_base.Domain.Models
{
    public class ProductRequestModel
    {
        public decimal PriceStart { get; set; } = decimal.Zero;
        public decimal PriceEnd { get; set; } = decimal.Zero;
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
    }
}
