using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Carrot : Plant
    {
        public Carrot() : base("Zanahoria", growthDays: 3, sellDays: 2, seedPrice: 15f, harvestSellPrice: 40f) { }

        public override string GetDescription()
        {
            return $"Zanahoria  | Semilla: ${SeedPrice} | Crece en {GrowthDays} dias | Venta: ${HarvestSellPrice} (tarda {SellDays} dias)";
        }
    }
}
