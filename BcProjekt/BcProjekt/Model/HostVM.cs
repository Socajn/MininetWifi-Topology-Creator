using System;
using System.Collections.Generic;

namespace BcProjekt.Model
{
    public class HostVM
    {
        public HostVM(int number)
        {
            Hostname = "h" + number;
            Type = "host";
            VlanInterfaces = new List<VlanInterface>();
            ExternalInterfaces = new List<ExternalInterface>();
            PrivateDiroctories = new List<PrivateDiroctory>();
        }


        public string Hostname { get; set; }

        public string IpAddress { get; set; }

        public string DefaultRoute { get; set;}

        public string AmountCPU { get; set; }

        public string Type { get; set; }

        public string Cores { get; set; }
        
        public string StartCommand { get; set; }

        public string StopCommad { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<VlanInterface> VlanInterfaces { get; set; }

        public List<ExternalInterface> ExternalInterfaces { get; set; }

        public List<PrivateDiroctory> PrivateDiroctories { get; set; }
    }





}

