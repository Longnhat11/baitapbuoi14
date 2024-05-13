using baitapbuoi14;
using DataAccess.DTO;
using DataAccess.IStoreServices;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.StoreServices
{
    public class ProductService : IProductServices
    {
        public ReturnData AddProduct(Product product)
        {
            var result = new ReturnData();
            try
            {
                //kiểm tra dữ liệu vào 
                if(product == null
                   ||product.ProductName==null
                   ||product.ExpiryDate==null
                   ||product.Price<=0
                   ||product.Stock<=0)
                {
                    result.returncode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ";
                    return result;
                }
                var connect = Commonlib.DbHelper.GetSqlConnection();

                var cmd = new SqlCommand("AddProduct", connect);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName",product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID ", product.CategoryID);
                cmd.Parameters.AddWithValue("@Price",product.Price);
                cmd.Parameters.AddWithValue("@Stock",product.Stock);
                cmd.Parameters.AddWithValue("@ExpiryDate",product.ExpiryDate);
                cmd.Parameters.AddWithValue("@ResponseCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var rs = cmd.Parameters["@ResponseCode"].Value != DBNull.Value ?
                    Convert.ToInt32(cmd.Parameters["@ResponseCode"].Value) : 0;

                if (rs < 0)
                {
                    result.returncode = -2;
                    result.returnMsg = "Thêm dữ liệu thất bại";
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.returncode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
            result.returncode = 1;
            result.returnMsg = "Đã thêm Product thành công!";
            return result;
        }

        public ReturnData AddProductWithKeybroad()
        {
            ReturnData result = new ReturnData();
            Product product = new Product();            
            try
            {
                bool check;string input;int Idinput; decimal price;
                Console.WriteLine("nhap CategoryId");
                do
                {
                    input = Console.ReadLine();
                    if (checkInput.CheckNumber(input, out Idinput) == true)
                    {
                        product.CategoryID = Idinput;
                        check = true;
                    }
                    else { check = false; }
                } while (!check);
                Console.WriteLine("nhap ProductName");
                do
                {
                    input = Console.ReadLine();
                    if (checkInput.CheckIsNullOrWhiteSpace(input)==true
                        ||checkInput.ContainsNumber(input)==true)
                    {
                        check = false;
                    }
                    else { check = true;product.ProductName = input; }
                } while (!check);
                Console.WriteLine("nhap price");
                do
                {
                    input = Console.ReadLine();
                    if (checkInput.CheckPrice(input, out price) == true)
                    {
                        product.Price = price;
                        check = true;
                    }
                    else { check = false; }
                } while (!check);
                Console.WriteLine("nhap ExpiryDate");
                do
                {
                    input = Console.ReadLine();
                    if (checkInput.CheckDateTime(input)==true)
                    {
                        product.ExpiryDate= Convert.ToDateTime(input);
                        check = true;
                    }
                    else { check = false; }
                } while (!check);
                Console.WriteLine("nhap stock");
                do
                {
                    input = Console.ReadLine();
                    if (checkInput.CheckNumber(input, out Idinput) == true)
                    {
                        product.Stock = Idinput;
                        check = true;
                    }
                    else { check = false; }
                } while (!check);
            }
            catch (Exception ex)
            {
                result.returncode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
            result=AddProduct(product);
            return result;
        }

        public ReturnProductReturnData GetProduct(GetProductRequestData requestData)
        {
            ReturnProductReturnData result = new ReturnProductReturnData();
            var list = new List<Product>();
            try
            {
                //kiểm tra dữ liệu vào
                if(requestData.ProductName == null 
                    &&requestData.ProductID <=0)
                {
                    result.returncode=-1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                //nếu nhập vào là tên
                if (requestData.ProductName != null)
                {
                    var connect = Commonlib.DbHelper.GetSqlConnection();

                    var cmd = new SqlCommand("GetProductName", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductName", requestData.ProductName);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductID = reader["ProductID"] != null ? Convert.ToInt32(reader["ProductID"]) : 0,
                            CategoryID = reader["CategoryID"] != null ? Convert.ToInt32(reader["CategoryID"]) : 0,
                            ProductName = reader["ProductName"] != null ? Convert.ToString(reader["ProductName"]) : String.Empty,
                            Price = reader["Price"] != null ? Convert.ToDecimal(reader["Price"]) : 0,
                            Stock = reader["Stock"] != null ? Convert.ToInt32(reader["Stock"]) : 0,
                            ExpiryDate = reader["ExpiryDate"] != null ? Convert.ToDateTime(reader["ExpiryDate"]) : DateTime.MinValue,
                        };
                        TimeSpan Time = product.ExpiryDate-DateTime.Now;
                        int TongSoNgay = Time.Days;
                        if (TongSoNgay <= 30)
                        {
                            list.Add(product);
                        }
                    }
                }
                //nếu nhập vào là số
                if (requestData.ProductName == null)
                {
                    var connect = Commonlib.DbHelper.GetSqlConnection();

                    var cmd = new SqlCommand("GetProductID", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductID", requestData.ProductID);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductID = reader["ProductID"] != null ? Convert.ToInt32(reader["ProductID"]) : 0,
                            CategoryID = reader["CategoryID"] != null ? Convert.ToInt32(reader["CategoryID"]) : 0,
                            ProductName = reader["ProductName"] != null ? Convert.ToString(reader["ProductName"]) : String.Empty,
                            Price = reader["Price"] != null ? Convert.ToDecimal(reader["Price"]) : 0,
                            Stock = reader["Stock"] != null ? Convert.ToInt32(reader["Stock"]) : 0,
                            ExpiryDate = reader["ExpiryDate"] != null ? Convert.ToDateTime(reader["ExpiryDate"]) : DateTime.MinValue,
                        };
                        TimeSpan Time = product.ExpiryDate - DateTime.Now;
                        int TongSoNgay = Time.Days;
                        if (TongSoNgay <= 30)
                        {
                            list.Add(product);
                        }
                    }
                }
                result.products = list;
                result.returncode = 1;
                result.returnMsg = "lấy dữ liệu thành công!";
                return result;
                
            }
            catch (Exception ex)
            {
                result.returncode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }
    }
}
    