using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Telnet.Services {

    internal sealed class IPScanner {



        public IPScanner() {
            Test();
        }

        void Test() {
            //NetworkInterface[] ani = NetworkInterface.GetAllNetworkInterfaces();
            //foreach (var ni in ani) {
            //    Console.WriteLine(ni.Name);
            //    //Console.WriteLine(ni);
            //}

            var ip = GetLocalIPAddress();
            var subnet = GetSubnetMask(ip);

            Console.WriteLine(ip);
            Console.WriteLine(subnet);
        }


        public void GetAllIp() {

        }

        public IPAddress GetLocalIPAddress() {
            return Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public IPAddress GetSubnetMask(IPAddress address) {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
                .Where(uiai => uiai.Address.AddressFamily == AddressFamily.InterNetwork && uiai.Address.Equals(address))
                .FirstOrDefault()
                .IPv4Mask;
        }

    }
}