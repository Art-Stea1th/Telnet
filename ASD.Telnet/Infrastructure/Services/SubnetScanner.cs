using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ASD.Telnet.Infrastructure.Services {

    using Extensions;

    internal static class SubnetScanner {

        //public static event Action<IPAddress> AvailableFound;

        public static async Task<IEnumerable<IPAddress>> ScanRangeAsync(IPAddress startIP, IPAddress endIP, int timeout = 1000) {

            var tasks = CreateIPRange(startIP, endIP)
                .Select(address => new Ping().SendPingAsync(address, timeout));

            var results = await Task.WhenAll(tasks);

            var availables = results
                .Where(reply => reply.Status == IPStatus.Success)?
                .Select(reply => reply.Address);

            return availables;
        }

        private static IEnumerable<IPAddress> CreateIPRange(IPAddress startIP, IPAddress endIP) {

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