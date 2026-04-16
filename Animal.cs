using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal abstract class Animal : Entity
    {
        public float BuyPrice { get; protected set; }
        public float ProductSellPrice { get; protected set; }
        public int ProductionDays { get; protected set; }
        public int ProductSellDays { get; protected set; }

        public Animal(string name, float buyPrice, float productSellPrice, int productionDays, int productSellDays)
            : base(name)
        {
            BuyPrice = buyPrice;
            ProductSellPrice = productSellPrice;
            ProductionDays = productionDays;
            ProductSellDays = productSellDays;
        }

        public abstract string GetProductName();
        public abstract string GetDescription();
    }
}
