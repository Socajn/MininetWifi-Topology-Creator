using System;
using System.Collections.Generic;

namespace BcProjekt.Model
{
    public class MainVM
    {
        public MainVM()
        {
            Hosts = new List<HostVM>();
            Stations = new List<StationVM>();
            Switches = new List<SwitchVM>();
            Aps = new List<ApVM>();
            LegacySwitches = new List<LegacySwitchVM>();
            LegacyRouters = new List<LegacyRouterVM>();
            NetLinks = new List<NetLinkVM>();
            Controllers = new List<ControllerVM>();
        }

        public List<HostVM> Hosts { get; set; }
        public List<StationVM> Stations { get; set; }
        public List<SwitchVM> Switches { get; set; }
        public List<ApVM> Aps { get; set; }
        public List<LegacySwitchVM> LegacySwitches { get; set; }
        public List<LegacyRouterVM> LegacyRouters { get; set; }
        public List<NetLinkVM> NetLinks { get; set; }
        public List<ControllerVM> Controllers { get; set; }
    }
}
