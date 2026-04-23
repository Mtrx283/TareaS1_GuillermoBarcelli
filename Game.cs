using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Game
    {
        private Farm farm;

        public void Execute()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("   Bienvenido al Juego de la Granja  ");
            Console.WriteLine("======================================");
            Console.WriteLine("Empiezas con $500, 3 parcelas y 2 corrales.");
            Console.WriteLine();

            farm = new Farm(startingMoney: 500f, initialPlots: 3, initialPens: 2);

            bool playing = true;
            while (playing)
            {
                farm.DisplayStatus();
                farm.DisplayPlots();
                farm.DisplayPens();

                Console.WriteLine("\n-- ACCIONES DEL DIA --");
                Console.WriteLine("  1. Comprar semilla y plantar");
                Console.WriteLine("  2. Cosechar planta lista");
                Console.WriteLine("  3. Comprar animal");
                Console.WriteLine("  4. Recolectar producto de animal");
                Console.WriteLine("  5. Vender item del inventario");
                Console.WriteLine("  6. Ver inventario y ventas pendientes");
                Console.WriteLine("  7. Ampliar granja");
                Console.WriteLine("  8. Pasar al siguiente dia");
                Console.WriteLine("  9. Salir");
                Console.Write("\nElige una opcion: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": MenuSowSeed(); break;
                    case "2": MenuHarvest(); break;
                    case "3": MenuBuyAnimal(); break;
                    case "4": MenuCollectProduct(); break;
                    case "5": MenuSellItem(); break;
                    case "6":
                        farm.DisplayInventory();
                        farm.DisplayPendingSales();
                        break;
                    case "7": MenuExpand(); break;
                    case "8":
                        Console.WriteLine("Avanzando al dia siguiente...");
                        farm.AdvanceDay();
                        Console.WriteLine($"Ahora es el dia {farm.CurrentDay}.");
                        break;
                    case "9":
                        playing = false;
                        Console.WriteLine("Hasta luego! Tu granja quedo en $" + (int)farm.Money);
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }
            }
        }

        private void MenuSowSeed()
        {
            List<PlotSlot> plots = farm.GetPlots();
            List<int> emptyIndices = new List<int>();
            for (int i = 0; i < plots.Count; i++)
                if (plots[i].IsEmpty) emptyIndices.Add(i);

            if (emptyIndices.Count == 0)
            {
                Console.WriteLine("No tienes parcelas vacias. Cosecha o amplia tu granja.");
                return;
            }

            Console.WriteLine("Parcelas disponibles:");
            for (int i = 0; i < emptyIndices.Count; i++)
                Console.WriteLine($"  {i + 1}. Parcela {emptyIndices[i] + 1}");
            Console.Write("Elige parcela: ");

            if (!int.TryParse(Console.ReadLine(), out int plotChoice)
                || plotChoice < 1 || plotChoice > emptyIndices.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }
            int selectedIndex = emptyIndices[plotChoice - 1];

            List<Plant> seeds = new List<Plant> { new Wheat(), new Carrot(), new Tomato() };
            Console.WriteLine("\nSemillas disponibles:");
            for (int i = 0; i < seeds.Count; i++)
                Console.WriteLine($"  {i + 1}. {seeds[i].GetDescription()}");
            Console.Write("Elige semilla: ");

            if (!int.TryParse(Console.ReadLine(), out int seedChoice)
                || seedChoice < 1 || seedChoice > seeds.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }

            farm.SowPlant(selectedIndex, seeds[seedChoice - 1]);
        }

        private void MenuHarvest()
        {
            List<PlotSlot> plots = farm.GetPlots();
            List<int> readyIndices = new List<int>();
            for (int i = 0; i < plots.Count; i++)
                if (plots[i].IsReady) readyIndices.Add(i);

            if (readyIndices.Count == 0)
            {
                Console.WriteLine("No hay plantas listas para cosechar.");
                return;
            }

            Console.WriteLine("Plantas listas:");
            for (int i = 0; i < readyIndices.Count; i++)
                Console.WriteLine($"  {i + 1}. Parcela {readyIndices[i] + 1} - {plots[readyIndices[i]].CurrentPlant.Name}");
            Console.Write("Elige cual cosechar: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)
                || choice < 1 || choice > readyIndices.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }

            farm.HarvestPlant(readyIndices[choice - 1]);
        }

        private void MenuBuyAnimal()
        {
            bool hasEmpty = false;
            foreach (AnimalPen pen in farm.GetPens())
                if (pen.IsEmpty) { hasEmpty = true; break; }

            if (!hasEmpty)
            {
                Console.WriteLine("No tienes corrales vacios. Amplia tu granja primero.");
                return;
            }

            List<Animal> animals = new List<Animal> { new Chicken(), new Cow() };
            Console.WriteLine("Animales disponibles:");
            for (int i = 0; i < animals.Count; i++)
                Console.WriteLine($"  {i + 1}. {animals[i].GetDescription()}");
            Console.Write("Elige animal: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)
                || choice < 1 || choice > animals.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }

            farm.BuyAnimal(animals[choice - 1]);
        }

        private void MenuCollectProduct()
        {
            List<AnimalPen> pens = farm.GetPens();
            List<int> readyIndices = new List<int>();
            for (int i = 0; i < pens.Count; i++)
                if (pens[i].HasProduct) readyIndices.Add(i);

            if (readyIndices.Count == 0)
            {
                Console.WriteLine("No hay productos listos para recolectar.");
                return;
            }

            Console.WriteLine("Productos listos:");
            for (int i = 0; i < readyIndices.Count; i++)
            {
                AnimalPen pen = pens[readyIndices[i]];
                Console.WriteLine($"  {i + 1}. Corral {readyIndices[i] + 1} - {pen.CurrentAnimal.Name} ({pen.CurrentAnimal.GetProductName()})");
            }
            Console.Write("Elige cual recolectar: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)
                || choice < 1 || choice > readyIndices.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }

            farm.CollectAnimalProduct(readyIndices[choice - 1]);
        }

        private void MenuSellItem()
        {
            farm.DisplayInventory();
            List<HarvestedItem> inventory = farm.GetInventory();

            if (inventory.Count == 0)
            {
                Console.WriteLine("Tu inventario esta vacio.");
                return;
            }

            Console.Write("Que item vender? (numero, o 0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int choice)
                || choice < 0 || choice > inventory.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }
            if (choice == 0) return;

            farm.SellItem(choice - 1);
        }

        private void MenuExpand()
        {
            Console.WriteLine("Que quieres ampliar?");
            Console.WriteLine($"  1. Nueva parcela de cultivo  - Costo: $100  (tienes {farm.PlotCount})");
            Console.WriteLine($"  2. Nuevo corral de animales  - Costo: $150  (tienes {farm.PenCount})");
            Console.WriteLine("  3. Cancelar");
            Console.Write("Elige: ");

            switch (Console.ReadLine())
            {
                case "1": farm.ExpandPlots(); break;
                case "2": farm.ExpandPens(); break;
                case "3": break;
                default: Console.WriteLine("Opcion invalida."); break;
            }
        }
    }
}
