using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class PendingSale
    {
        public string ItemName { get; }
        public float SalePrice { get; }
        public int DaysRemaining { get; private set; }

        public bool IsCompleted => DaysRemaining <= 0;

        public PendingSale(string itemName, float salePrice, int daysRemaining)
        {
            ItemName = itemName;
            SalePrice = salePrice;
            DaysRemaining = daysRemaining;
        }

        public void AdvanceDay()
        {
            if (DaysRemaining > 0)
                DaysRemaining--;
        }

        public override string ToString()
        {
            return $"{ItemName} - ${SalePrice} ({DaysRemaining} dia(s) restantes)";
        }
    }
}
