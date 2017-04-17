using System;
using System.Net;

namespace ASD.Telnet.Infrastructure.Extensions {

    internal static class IPExtensions {

        public static uint ToUInt32(this IPAddress address) {
            var bytes = address.GetAddressBytes();
            if (BitConverter.IsLittleEndian) {
                // flip big-endian (network order) to little-endian
                Array.Reverse(bytes);
            }            
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static IPAddress ToIPv4(this uint address) {
            var bytes = BitConverter.GetBytes(address);
            if (BitConverter.IsLittleEndian) {
                // flip little-endian to big-endian (network order)
                Array.Reverse(bytes);
            }
            return new IPAddress(bytes);
        }
    }
}