using System;
namespace BcProjekt.Model
{
    public class LegacySwitchVM
    {
        public LegacySwitchVM(int number)
        {
            Name = "ls" + number;
        }

        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
