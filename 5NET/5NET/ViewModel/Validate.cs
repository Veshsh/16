using System.Text.RegularExpressions;
using System.Windows;

namespace _5NET.ViewModel
{
    public class Validate
    {
        public static bool EnterValidate(string Ip, string Name)
        {
            if (Name is null || Name is "")
                MessageBox.Show("Невведено имя");
            if (Ip is null || Ip is "")
                MessageBox.Show("Невведеный ip");
            else if (Regex.IsMatch(Ip, "\\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?):\\d{1,5}\\b", RegexOptions.IgnoreCase))
                return true;
            else
                MessageBox.Show("Неправельно введён ip");
            return false;
        }
        public static bool MessageValidate(string Message)
        {
            if (Message is null)
                MessageBox.Show("Введите сообщение");
            else
                return true;
            return false;
        }
    }
}
