using System;
using System.Collections.Generic;

namespace BcProjekt.Model
{
    public class SwitchVM
    {
        public SwitchVM(int number)
        {
            Name = "s" + number;
            NetFlow = false;
            sFlow = false;
            Type = "default";
            ExternalInterfaces = new List<ExternalInterface>();
        }

        public string Name { get; set; }

        public string DPID { get; set; }

        public bool NetFlow { get; set; }

        public bool sFlow { get; set; }

        public string Type { get; set; }

        public string IpAddress { get; set; }

        public string DPCTLport { get; set; }

        public string StartCommand { get; set; }

        public string StopCommad { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<ExternalInterface> ExternalInterfaces { get; set; }
    }
}
