using System;
namespace BcProjekt.Model
{
    public class VlanInterface
    {
        public VlanInterface() { }

        public string IpAddress { get; set; }

        public string VlanId { get; set; }
    }

    public class ExternalInterface
    {
        public ExternalInterface() { }

        public string Name { get; set; }
    }

    public class PrivateDiroctory
    {
        public PrivateDiroctory() { }

        public string Mount { get; set; }

        public string Directory { get; set; }
    }
}
