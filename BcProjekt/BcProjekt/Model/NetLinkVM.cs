using System;
namespace BcProjekt.Model
{
    public class NetLinkVM
    {
        public NetLinkVM(int number)
        {
            Name = "nl" + number;
            Connection = "wired";
            SSID = "new-ssid";
            Cahnnel = 1;
            Mode = "g";
        }

        public string Name { get; set; }

        public string Connection { get; set; }

        public string SSID { get; set; }

        public int Cahnnel { get; set; }

        public string Mode { get; set; }

        public string Bandwith { get; set; }

        public string Delay { get; set; }

        public string Loss { get; set; }

        public string MaxQueueSize { get; set; }

        public string Jitter { get; set; }

        public string Speedup { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
