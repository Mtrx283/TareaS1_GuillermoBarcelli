using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class HarvestedItem
    {
        public string Name { get; }
        public float SellPrice { get; }
        public int SellDays { get; }

        public HarvestedItem(string name, float sellPrice, int sellDays)
        {
            Name = name;
            SellPrice = sellPrice;
            SellDays = sellDays;
        }

        public override string ToString()
        {
            return $"{Name} - Precio: ${SellPrice} (tardara {SellDays} dia(s) en venderse)";
        }
    }
}
