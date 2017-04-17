using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ASD.Telnet.Infrastructure.Services {

    using Extensions;

    internal static class SubnetHelper {

        public static IPAddress GetLocalIPAddress() {
            return Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public static IPAddress GetSubnetMaskFor(IPAddress address) {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
                .Where(uiai => uiai.Address.AddressFamily == AddressFamily.InterNetwork && uiai.Address.Equals(address))
                .FirstOrDefault()
                .IPv4Mask;
        }

        public static IPAddress GetStartIP(IPAddress address, IPAddress subnetMask) {
            return (address.ToUInt32() & subnetMask.ToUInt32()).ToIPv4();
        }

        public static IPAddress GetEndIP(IPAddress address, IPAddress subnetMask) {
            return (address.ToUInt32() | ~subnetMask.ToUInt32()).ToIPv4();
        }

        public static IEnumerable<IPAddress> GetIPRange(IPAddress startIP, IPAddress endIP) {

            var startIPAsUint = startIP.ToUInt32();
            var endIPAsUint = endIP.ToUInt32();

            if (startIPAsUint > endIPAsUint) {
                throw new ArgumentException(
                    $"The StartIP: {startIP.ToString()} must be less than the EndIP: {endIP.ToString()}");
            }

            for (var i = startIPAsUint; i <= endIPAsUint; ++i) {
                yield return i.ToIPv4();
            }
        }
    }
}