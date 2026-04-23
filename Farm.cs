using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Farm
    {
        public float Money { get; private set; }
        public int CurrentDay { get; private set; }
        public int PlotCount => plantPlots.Count;
        public int PenCount => animalPens.Count;

        private const float PlotExpansionCost = 100f;
        private const float PenExpansionCost = 150f;

        private List<PlotSlot> plantPlots;
        private List<AnimalPen> animalPens;
        private List<HarvestedItem> inventory;
        private List<PendingSale> pendingSales;

        public Farm(float startingMoney, int initialPlots, int initialPens)
        {
            Money = startingMoney;
            CurrentDay = 1;
            plantPlots = new List<PlotSlot>();
            animalPens = new List<AnimalPen>();
            inventory = new List<HarvestedItem>();
            pendingSales = new List<PendingSale>();

            for (int i = 0; i < initialPlots; i++) plantPlots.Add(new PlotSlot());
            for (int i = 0; i < initialPens; i++) animalPens.Add(new AnimalPen());
        }

        public bool SowPlant(int slotIndex, Plant plant)
        {
            PlotSlot slot = GetPlot(slotIndex);
            if (slot == null || !slot.IsEmpty)
            {
                Console.WriteLine("Parcela no valida o ya ocupada.");
                return false;
            }
            if (Money < plant.SeedPrice)
            {
                Console.WriteLine($"No tienes suficiente dinero. Necesitas ${plant.SeedPrice}.");
                return false;
            }
            Money -= plant.SeedPrice;
            slot.Sow(plant);
            Console.WriteLine($"Sembraste {plant.Name} en la parcela {slotIndex + 1}. (-${plant.SeedPrice})");
            return true;
        }

        public bool HarvestPlant(int slotIndex)
        {
            PlotSlot slot = GetPlot(slotIndex);
            if (slot == null || !slot.IsReady)
            {
                Console.WriteLine("Esa parcela no tiene plantas listas.");
                return false;
            }
            Plant harvested = slot.Harvest();
            inventory.Add(new HarvestedItem(harvested.Name, harvested.HarvestSellPrice, harvested.SellDays));
            Console.WriteLine($"Cosechaste {harvested.Name}! Revisa tu inventario para venderlo.");
            return true;
        }

        public bool BuyAnimal(Animal animal)
        {
            AnimalPen emptyPen = animalPens.Find(p => p.IsEmpty);
            if (emptyPen == null)
            {
                Console.WriteLine("No tienes corrales vacios. Amplia tu granja primero.");
                return false;
            }
            if (Money < animal.BuyPrice)
            {
                Console.WriteLine($"No tienes suficiente dinero. Necesitas ${animal.BuyPrice}.");
                return false;
            }
            Money -= animal.BuyPrice;
            emptyPen.AddAnimal(animal);
            Console.WriteLine($"Compraste una {animal.Name}. (-${animal.BuyPrice})");
            return true;
        }

        public bool CollectAnimalProduct(int penIndex)
        {
            AnimalPen pen = GetPen(penIndex);
            if (pen == null || !pen.HasProduct)
            {
                Console.WriteLine("Ese corral no tiene productos listos.");
                return false;
            }
            Animal animal = pen.CollectProduct();
            inventory.Add(new HarvestedItem(animal.GetProductName(), animal.ProductSellPrice, animal.ProductSellDays));
            Console.WriteLine($"Recolectaste {animal.GetProductName()} de tu {animal.Name}!");
            return true;
        }

        public bool SellItem(int inventoryIndex)
        {
            if (inventoryIndex < 0 || inventoryIndex >= inventory.Count) return false;
            HarvestedItem item = inventory[inventoryIndex];
            pendingSales.Add(new PendingSale(item.Name, item.SellPrice, item.SellDays));
            inventory.RemoveAt(inventoryIndex);
            Console.WriteLine($"Pusiste {item.Name} a la venta. Recibiras ${item.SellPrice} en {item.SellDays} dia(s).");
            return true;
        }

        public bool ExpandPlots()
        {
            if (Money < PlotExpansionCost)
            {
                Console.WriteLine($"Necesitas ${PlotExpansionCost} para ampliar parcelas.");
                return false;
            }
            Money -= PlotExpansionCost;
            plantPlots.Add(new PlotSlot());
            Console.WriteLine($"Nueva parcela agregada! Ahora tienes {plantPlots.Count}. (-${PlotExpansionCost})");
            return true;
        }

        public bool ExpandPens()
        {
            if (Money < PenExpansionCost)
            {
                Console.WriteLine($"Necesitas ${PenExpansionCost} para ampliar corrales.");
                return false;
            }
            Money -= PenExpansionCost;
            animalPens.Add(new AnimalPen());
            Console.WriteLine($"Nuevo corral agregado! Ahora tienes {animalPens.Count}. (-${PenExpansionCost})");
            return true;
        }

        public void AdvanceDay()
        {
            CurrentDay++;

            foreach (PlotSlot slot in plantPlots) slot.AdvanceDay();
            foreach (AnimalPen pen in animalPens) pen.AdvanceDay();

            List<PendingSale> completed = new List<PendingSale>();
            foreach (PendingSale sale in pendingSales)
            {
                sale.AdvanceDay();
                if (sale.IsCompleted) completed.Add(sale);
            }
            foreach (PendingSale sale in completed)
            {
                Money += sale.SalePrice;
                pendingSales.Remove(sale);
                Console.WriteLine($"  >> Venta completada: {sale.ItemName} por ${sale.SalePrice}! (Dinero total: ${Money:F0})");
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine("\n" + new string('=', 55));
            Console.WriteLine($"  GRANJA - Dia {CurrentDay}  |  Dinero: ${Money:F0}");
            Console.WriteLine(new string('=', 55));
        }

        public void DisplayPlots()
        {
            Console.WriteLine($"\nPARCELAS ({plantPlots.Count} total):");
            for (int i = 0; i < plantPlots.Count; i++)
                Console.WriteLine(plantPlots[i].GetStatus(i));
        }

        public void DisplayPens()
        {
            Console.WriteLine($"\nCORRALES ({animalPens.Count} total):");
            for (int i = 0; i < animalPens.Count; i++)
                Console.WriteLine(animalPens[i].GetStatus(i));
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"\nINVENTARIO ({inventory.Count} items):");
            if (inventory.Count == 0) { Console.WriteLine("  (vacio)"); return; }
            for (int i = 0; i < inventory.Count; i++)
                Console.WriteLine($"  {i + 1}. {inventory[i]}");
        }

        public void DisplayPendingSales()
        {
            Console.WriteLine($"\nVENTAS EN PROCESO ({pendingSales.Count}):");
            if (pendingSales.Count == 0) { Console.WriteLine("  (ninguna)"); return; }
            foreach (PendingSale sale in pendingSales)
                Console.WriteLine($"  - {sale}");
        }

        public PlotSlot GetPlot(int index) => (index >= 0 && index < plantPlots.Count) ? plantPlots[index] : null;
        public AnimalPen GetPen(int index) => (index >= 0 && index < animalPens.Count) ? animalPens[index] : null;
        public List<PlotSlot> GetPlots() => plantPlots;
        public List<AnimalPen> GetPens() => animalPens;
        public List<HarvestedItem> GetInventory() => inventory;
    }
}
