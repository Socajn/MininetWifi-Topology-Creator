using System;
namespace BcProjekt.Model
{
    public class ControllerVM
    {
        public ControllerVM(int number)
        {
            Name = "c" + number;
            ControllerPort = "6633";
            Type = "OpenFlow reference";
            Protocol = "TCP";
            IPAddress = "127.0.0.1";
        }

        public string Name { get; set; }

        public string ControllerPort { get; set; }

        public string Type { get; set; }

        public string Protocol { get; set; }

        public string IPAddress { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
