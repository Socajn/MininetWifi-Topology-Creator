using System;
namespace BcProjekt.Model
{
    public class LegacyRouterVM
    {
        public LegacyRouterVM(int number)
        {
            Name = "lr" + number;
        }

        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
