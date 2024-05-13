using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace baitapbuoi14
{
    public class checkInput
    {
        public static bool ContainsNumber(string input)
        {
            if (input.Any(char.IsDigit))
            {
                Console.WriteLine("Không được chứa chữ số!");
                return true;
            }
            else
                return false;
        }
        public static bool CheckContainSpecialChar(string input)
        {
            Regex specialCharRegex = new Regex(@"[!@#$%&*()+=|\\{}':;,<>?/""-]");
            if (specialCharRegex.IsMatch(input))
            {
                Console.WriteLine("Không được chứa kí tự đặc biệt!");
                return true;
            }
            else
                return false;
        }
        public static bool CheckIsNullOrWhiteSpace(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Không được để trống hay chỉ chứa khoảng trắng!");
                return true;
            }
            else
                return false;
        }
        public static bool CheckDateTime(string input)
        {
            if (DateTime.TryParseExact(input, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Định dạng ngày không hợp lệ!");
                return false;
            }
        }
        public static bool CheckNumber(string input, out int number)
        {
            if (int.TryParse(input, out number))
            {
                if (number <= 0)
                {
                    Console.WriteLine("Số không được bằng 0 hoặc là số âm!");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                Console.WriteLine("Số không được quá lớn, không được là kí tự, không để trống hay chứa khoảng trắng!");
                return false;
            }
        }
        public static bool CheckPrice(string input, out decimal number)
        {
            if (decimal.TryParse(input, out number))
            {
                if (number <= 0)
                {
                    Console.WriteLine("Số tien không được bằng 0 hoặc là số âm!");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                Console.WriteLine("Số tien không được quá lớn, không được là kí tự, không để trống hay chứa khoảng trắng!");
                return false;
            }
        }
    }
}   
