using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai3_DNS_Server
{
    class Program
    {
        private static DNSServer named;
        static void Main(string[] args)
        {
            named = new DNSServer();
            while (!false)
            {
                named.startListening();
            }
        }
    }
}
