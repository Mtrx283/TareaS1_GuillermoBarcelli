using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Wheat : Plant
    {
        public Wheat() : base("Trigo", growthDays: 2, sellDays: 1, seedPrice: 10f, harvestSellPrice: 25f) { }

        public override string GetDescription()
        {
            return $"Trigo      | Semilla: ${SeedPrice} | Crece en {GrowthDays} dias | Venta: ${HarvestSellPrice} (tarda {SellDays} dia)";
        }
    }
}
