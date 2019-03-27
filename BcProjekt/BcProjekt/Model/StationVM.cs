using System;
using System.Collections.Generic;

namespace BcProjekt.Model
{
    public class StationVM
    {
        public StationVM(int number)
        {
            Name = "sta" + number;
            Wlans = 1;
            Wpans = 0;
            SignalRange = 1;
            Type = "station";
            Rad = new Radius();
            VlanInterfaces = new List<VlanInterface>();
            ExternalInterfaces = new List<ExternalInterface>();
            PrivateDiroctories = new List<PrivateDiroctory>();
        }


        public string Name { get; set; }

        public int Wlans { get; set; }

        public int Wpans { get; set; }

        public int SignalRange { get; set; }

        public int PreviousSignalRange { get; set; }

        public string IpAddress { get; set; }

        public string DefaultRoute { get; set; }

        public string AmountCPU { get; set; }

        public string Type { get; set; }

        public string Cores { get; set; }

        public string StartCommand { get; set; }

        public string StopCommad { get; set; }

        public Radius Rad {get; set;}

        public bool IsDeleted { get; set; } = false;

        public List<VlanInterface> VlanInterfaces { get; set; }

        public List<ExternalInterface> ExternalInterfaces { get; set; }

        public List<PrivateDiroctory> PrivateDiroctories { get; set; }

    }

    public class Radius
    {
        public Radius() { }

        public string Username { get; set; }

        public string Password { get; set; }
    }

}
