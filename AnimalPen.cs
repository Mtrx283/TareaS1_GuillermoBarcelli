using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class AnimalPen
    {
        public Animal CurrentAnimal { get; private set; }
        public int DaysUntilProduct { get; private set; }

        public bool IsEmpty => CurrentAnimal == null;
        public bool HasProduct => CurrentAnimal != null && DaysUntilProduct <= 0;

        public void AddAnimal(Animal animal)
        {
            CurrentAnimal = animal;
            DaysUntilProduct = animal.ProductionDays;
        }

        public void AdvanceDay()
        {
            if (CurrentAnimal != null && DaysUntilProduct > 0)
                DaysUntilProduct--;
        }

        // Resetea el contador y devuelve el animal para saber qué producto dar
        public Animal CollectProduct()
        {
            DaysUntilProduct = CurrentAnimal.ProductionDays;
            return CurrentAnimal;
        }

        public string GetStatus(int index)
        {
            if (IsEmpty) return $"  Corral {index + 1}: [Vacio]";
            if (HasProduct) return $"  Corral {index + 1}: [{CurrentAnimal.Name} - {CurrentAnimal.GetProductName()} LISTO!]";
            return $"  Corral {index + 1}: [{CurrentAnimal.Name} - {DaysUntilProduct} dia(s) para {CurrentAnimal.GetProductName()}]";
        }
    }
}
