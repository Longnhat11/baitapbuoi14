using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.DTO
{
    public class Product
    {
       public int ProductID {  get; set; }
       public string ProductName {  get; set; }
       public int CategoryID { get; set; }
       public decimal Price {  get; set; }
       public int Stock {  get; set; }
       public DateTime ExpiryDate {  get; set; }
    }
    public class GetProductRequestData
    {
        public string ProductName { get; set; }
        public int ProductID {get; set; }
    }
}
