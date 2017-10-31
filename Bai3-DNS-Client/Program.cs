using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai3_DNS_Client
{
    class Program
    {
        private static DNSClient dnsClient;
        static void Main(string[] args)
        {
            dnsClient = new DNSClient();
            dnsClient.requestResolve("https://google.com");
            System.Console.ReadKey();
        }
    }
}
