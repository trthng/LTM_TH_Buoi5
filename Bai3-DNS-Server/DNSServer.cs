using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bai3_DNS_Server
{
    class DNSServer
    {
        private TcpListener server;
        private TcpClient client;
        private NetworkStream netStream;
        private byte[] dataSend, dataReceive;
        private string data;

        private Dictionary<string, string> resolve; 

        public DNSServer()
        {
            Console.WriteLine("Server init components...");
            dataReceive = new byte[1024];
            dataSend = new byte[1024];

            resolve = new Dictionary<string, string>();     
            resolve.Add("https://google.com", "111.222.333.444");
            resolve.Add("https://bing.com", "1.2.3.4");

            server = new TcpListener(IPAddress.Any, 1724);
        }

        public void startListening()
        {
            Console.WriteLine("Server listening...");
            server.Start();
            client = server.AcceptTcpClient();
            netStream = client.GetStream();
            Console.WriteLine("Server accepted a client...");

            int dataSize = netStream.Read(dataReceive, 0, 1024);
            data = Encoding.ASCII.GetString(dataReceive, 0, dataSize);
            if (data == "QUERY")            
            {
                data = "ACCEPT";
                dataSend = Encoding.ASCII.GetBytes(data);
                netStream.Write(dataSend, 0, data.Length);
            }

            dataSize = netStream.Read(dataReceive, 0, 1024);        
            string key = Encoding.ASCII.GetString(dataReceive, 0, dataSize);       
            Console.WriteLine("Server received a query: " + key);
            string value = resolve[key];
            dataSend = Encoding.ASCII.GetBytes(value);     
            netStream.Write(dataSend, 0, value.Length);
            Console.WriteLine("Server responed with: " + value);

            dataSize = netStream.Read(dataReceive, 0, 1024);
            data = Encoding.ASCII.GetString(dataReceive, 0, dataSize);
            if (data == "ACK")
            {
                client.Close();
                netStream.Close();
                Console.WriteLine("Server disconnected a client...");
            }
        }
    }
}
