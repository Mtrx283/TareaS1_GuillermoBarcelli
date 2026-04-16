using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class PlotSlot
    {
        public Plant CurrentPlant { get; private set; }
        public int DaysRemaining { get; private set; }

        public bool IsEmpty => CurrentPlant == null;
        public bool IsReady => CurrentPlant != null && DaysRemaining <= 0;

        public void Sow(Plant plant)
        {
            CurrentPlant = plant;
            DaysRemaining = plant.GrowthDays;
        }

        public void AdvanceDay()
        {
            if (CurrentPlant != null && DaysRemaining > 0)
                DaysRemaining--;
        }

        public Plant Harvest()
        {
            Plant harvested = CurrentPlant;
            CurrentPlant = null;
            DaysRemaining = 0;
            return harvested;
        }

        public string GetStatus(int index)
        {
            if (IsEmpty) return $"  Parcela {index + 1}: [Vacia]";
            if (IsReady) return $"  Parcela {index + 1}: [LISTA - {CurrentPlant.Name}]";
            return $"  Parcela {index + 1}: [{CurrentPlant.Name} - {DaysRemaining} dia(s) restantes]";
        }
    }
}
