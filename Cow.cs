using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Cow : Animal
    {
        public Cow() : base("Vaca", buyPrice: 150f, productSellPrice: 80f, productionDays: 3, productSellDays: 2) { }

        public override string GetProductName() => "Leche";

        public override string GetDescription()
        {
            return $"Vaca    | Precio: ${BuyPrice} | Produce {GetProductName()} cada {ProductionDays} dias | Venta: ${ProductSellPrice} (tarda {ProductSellDays} dias)";
        }
    }
}
