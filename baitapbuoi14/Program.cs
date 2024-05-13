
// See https://aka.ms/new-console-template for more information

using DataAccess.DTO;
using DataAccess.StoreServices;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Text;
ReturnProductReturnData returnProductReturnData=new ReturnProductReturnData();
ProductService productService=new ProductService();
Console.OutputEncoding = Encoding.UTF8;
Product product = new Product()
{
    ProductName = "Quan Ao",
    CategoryID = 1,
    Price = 1000,
    Stock = 5,
    ExpiryDate = DateTime.Now
};
GetProductRequestData GetProductRequestData = new GetProductRequestData();
Console.WriteLine("nhập lựa chọn của bạn:\n1.nhập sản phẩm từ bàn phím\n2. Kiểm tra số lượng tồn kho,các sản phẩm sắp hết hạn (30 ngày tính từ ngày hiện tại) tìm kiếm theo tên hoặc ProductID");
int choice=Convert.ToInt32(Console.ReadLine());
switch (choice)
{
    case 1:
        Console.WriteLine( productService.AddProductWithKeybroad().returnMsg);
        break;
    case 2:
        Console.WriteLine("nhap 1 de tim san pham theo ten, nhap 2 de tim san pham theo id ");
        int chon= Convert.ToInt32(Console.ReadLine());
        switch (chon)
        {
            case 1:
                Console.WriteLine("Nhap ten:");
                GetProductRequestData.ProductName = Console.ReadLine();
                var list = productService.GetProduct(GetProductRequestData).products;
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Thêm một Worksheet mới
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Danh sách sản phẩm");

                    // Thêm tiêu đề cho cột
                    worksheet.Cells[1, 1].Value = "Tên sản phẩm";

                    // Thêm sản phẩm vào Worksheet
                    for (int i = 0; i < list.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = list[i];
                    }

                    // Lưu file Excel
                    package.SaveAs(new System.IO.FileInfo("C:\\Users\\LENOVO\\source\\repos\\Output.xlsx"));
                }
                if (list != null && list.Count > 0)
                {
                    Console.WriteLine("-----------------");
                    foreach (var item in list)
                    {
                        Console.WriteLine("ProductID {0}", item.ProductID);
                        Console.WriteLine("ProductName {0}", item.ProductName);
                        Console.WriteLine("price {0}", item.Price);
                        Console.WriteLine("CategoryID {0}", item.CategoryID);
                        Console.WriteLine("stock {0}", item.Stock);
                        Console.WriteLine("ExpiryDate {0}", item.ExpiryDate);
                    }
                }
                break;
            case 2:
                Console.WriteLine("nhap Id");
                GetProductRequestData.ProductID= Convert.ToInt32(Console.ReadLine());
                list = productService.GetProduct(GetProductRequestData).products;
                if (list != null && list.Count > 0)
                {
                    Console.WriteLine("-----------------");
                    foreach (var item in list)
                    {
                        Console.WriteLine("ProductID {0}", item.ProductID);
                        Console.WriteLine("ProductName {0}", item.ProductName);
                        Console.WriteLine("price {0}", item.Price);
                        Console.WriteLine("CategoryID {0}", item.CategoryID);
                        Console.WriteLine("stock {0}", item.Stock);
                        Console.WriteLine("ExpiryDate {0}", item.ExpiryDate);
                    }
                }
                break;
            default:
                Console.WriteLine("lua chon khong hop le!");
                break;
        }
        break;
    default:
        Console.WriteLine("lua chon khong hop le!");
        break;
}

