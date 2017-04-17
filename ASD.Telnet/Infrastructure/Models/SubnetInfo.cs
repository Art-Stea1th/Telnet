using System.Net;

namespace ASD.Telnet.Infrastructure.Models {

    using Services;

    internal sealed class SubnetInfo {

        public IPAddress Address { get; private set; }
        public IPAddress SubnetMask { get; private set; }

        public IPAddress StartIP { get; private set; }
        public IPAddress EndIP { get; private set; }

        public SubnetInfo() {
            Address = SubnetHelper.GetLocalIPAddress();
            SubnetMask = SubnetHelper.GetSubnetMaskFor(Address);
            StartIP = SubnetHelper.GetStartIP(Address, SubnetMask);
            EndIP = SubnetHelper.GetEndIP(Address, SubnetMask);
        }
    }
}