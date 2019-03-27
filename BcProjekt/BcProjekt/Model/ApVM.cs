using System;
using System.Collections.Generic;

namespace BcProjekt.Model
{
    public class ApVM
    {
        public ApVM(int number)
        {
            Name = "ap" + number;
            SSID = "ap" + number + "-ssid";
            Channel = 1;
            Mode = "g";
            SignalRange = 100;
            Authentication = "none";
            Type = "Default";
            NetFlow = false;
            sFlow = false;
            ExternalInterfaces = new List<ExternalInterface>();
        }

        public string Name { get; set; }

        public string SSID { get; set; }

        public int Channel { get; set; }

        public string Mode { get; set; }

        public int SignalRange { get; set; }

        public int PreviousSignalRange { get; set; }

        public string Authentication { get; set; }

        public string Type { get; set; }

        public string DPID { get; set; }

        public bool NetFlow { get; set; }

        public bool sFlow { get; set; }

        public string IpAddress { get; set; }

        public string DPCTLport { get; set; }

        public string StartCommand { get; set; }

        public string StopCommad { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<ExternalInterface> ExternalInterfaces { get; set; }
    }
}
