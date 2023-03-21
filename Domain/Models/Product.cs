using System;
using System.ComponentModel.DataAnnotations;

namespace Shopbridge_base.Domain.Models
{
    public class Product
    {
        [Key]
        public Guid? Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Description { get; set; }
        public decimal Product_Price { get; set; }
        public double? Product_StockBalance { get; set; }
    }
}
