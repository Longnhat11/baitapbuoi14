using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IStoreServices
{
    public interface IProductServices
    {
        ReturnData AddProduct(Product product);
        ReturnProductReturnData GetProduct(GetProductRequestData requestData);
        ReturnData AddProductWithKeybroad();
    }
}
