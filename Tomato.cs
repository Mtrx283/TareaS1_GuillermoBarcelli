using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Tomato : Plant
    {
        public Tomato() : base("Tomate", growthDays: 4, sellDays: 1, seedPrice: 20f, harvestSellPrice: 60f) { }

        public override string GetDescription()
        {
            return $"Tomate     | Semilla: ${SeedPrice} | Crece en {GrowthDays} dias | Venta: ${HarvestSellPrice} (tarda {SellDays} dia)";
        }
    }
}
