using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal abstract class Plant : Entity
    {
        public int GrowthDays { get; protected set; }
        public int SellDays { get; protected set; }
        public float SeedPrice { get; protected set; }
        public float HarvestSellPrice { get; protected set; }

        public Plant(string name, int growthDays, int sellDays, float seedPrice, float harvestSellPrice)
            : base(name)
        {
            GrowthDays = growthDays;
            SellDays = sellDays;
            SeedPrice = seedPrice;
            HarvestSellPrice = harvestSellPrice;
        }

        public abstract string GetDescription();
    }
}
