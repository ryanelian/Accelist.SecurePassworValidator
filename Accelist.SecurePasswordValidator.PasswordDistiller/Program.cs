using System;
using System.IO;

namespace Accelist.SecurePasswordValidator.PasswordDistiller
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 0;

            using (var dump = new FileStream("rockyou-3min8.txt", FileMode.Create))
            using (var file = new FileStream("rockyou-withcount.txt", FileMode.Open))
            using (var sw = new StreamWriter(dump))
            using (var sr = new StreamReader(file))
            {
                string s = sr.ReadLine();
                while (s != null)
                {
                    var countString = s.Substring(0, 8);
                    var password = s.Substring(8);
                    var count = int.Parse(countString.Trim());
                    if (count < 3)
                    {
                        break;
                    }

                    //Console.WriteLine($"{count}:\"{password}\"");
                    //Console.ReadLine();
                    if (password.Length >= 8)
                    {
                        sw.WriteLine(password);
                        n++;
                    }

                    s = sr.ReadLine();
                }
            }

        }
    }
}
