using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public  class ReturnData
    {
        public int returncode { get; set; }
        public string returnMsg { get; set; }
    }
    public class ReturnProductReturnData: ReturnData
    {
        public List<Product> products { get; set; }
    }
}
