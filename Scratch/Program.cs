using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(YahooFantasySports.AuthManager.Instance.GetAuthTokenAsync().Result);
            Console.WriteLine(YahooFantasySports.AuthManager.Instance.GetAuthTokenAsync().Result);
        }
    }
}
