using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Chicken : Animal
    {
        public Chicken() : base("Gallina", buyPrice: 50f, productSellPrice: 30f, productionDays: 2, productSellDays: 1) { }

        public override string GetProductName() => "Huevos";

        public override string GetDescription()
        {
            return $"Gallina | Precio: ${BuyPrice} | Produce {GetProductName()} cada {ProductionDays} dias | Venta: ${ProductSellPrice} (tarda {ProductSellDays} dia)";
        }
    }
}
